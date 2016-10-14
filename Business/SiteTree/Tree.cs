using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Business.SiteTree
{
    [Serializable]
    public class Tree
    {
        #region Public Methods

        /// <summary>
        /// Initializes the list of page nodes
        /// </summary>
        public Tree()
        {
            Pages = new List<Node>();
        }

        public List<Node> Pages { get; set; }

        /// <summary>
        /// Finds a page node for a given content ID
        /// </summary>
        /// <param name="contentID">the unique identifier for the content</param>
        /// <returns>The page node for the content ID provided</returns>
        public Node FindPage(Guid contentID)
        {
            var page = new Node();
            Search(Pages, contentID, ref page);

            return page;
        }

        /// <summary>
        /// Inserts a page node below a particular parent
        /// </summary>
        /// <param name="page">The page node to insert</param>
        /// <param name="parentID">The nullable parent ID; pages without a parent ID are top-level pages</param>
        public void InsertPage(Node page, Guid? parentID)
        {
            if (parentID.HasValue)
            {
                Node parent = FindPage(parentID.Value);
                page.ParentID = parentID.Value;
                parent.Pages.Add(page);
            }
            else
            {
                Pages.Add(page);
            }
        }

        /// <summary>
        /// Deletes a page node from the tree; sub-pages are converted to top-level pages
        /// </summary>
        /// <param name="page">The page node to delete</param>
        /// <param name="preserveChildren">False if child pages should be deleted</param>
        public void DeletePage(Node page, bool preserveChildren)
        {
            if (preserveChildren) MoveChildrenAboveParent(page);
            RemovalSearch(Pages, page.ContentID);
        }

        /// <summary>
        /// Moves a node to a new location in the tree
        /// </summary>
        /// <param name="page">The page node to move</param>
        /// <param name="parent">The parent node; if null, the page is added to the root</param>
        public void MovePage(Node page, Node parent)
        {
            MoveNodes(page, parent);
        }

        /// <summary>
        /// Moves a node to a new position within the current level
        /// </summary>
        /// <param name="pageToMove">The page node to move</param>
        /// <param name="anchor">The page node that will be immediately below the pageToMove node</param>
        public void ReorderPage(Node pageToMove, Node anchor)
        {
            ReorderNodes(pageToMove, anchor);
        }

        /// <summary>
        /// Serializes a node using binary serialization
        /// </summary>
        /// <param name="page">The page node to serialize</param>
        /// <returns>A MemoryStream containing the serialized information</returns>
        public MemoryStream SerializeNode(Node page)
        {
            var ms = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, page);
            return ms;
        }

        /// <summary>
        /// Deserializes a binary serialized node
        /// </summary>
        /// <param name="serializedNode">The MemoryStream object containing the node</param>
        /// <returns>A deserialized Node object</returns>
        public Node DeserializeNode(MemoryStream serializedNode)
        {
            IFormatter formatter = new BinaryFormatter();
            serializedNode.Seek(0, SeekOrigin.Begin);
            return (Node) formatter.Deserialize(serializedNode);
        }

        /// <summary>
        /// Serializes a tree using binary serialization
        /// </summary>
        /// <returns>A MemoryStream containing the serialized information</returns>
        public MemoryStream SerializeTree()
        {
            var ms = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, this);
            return ms;
        }

        /// <summary>
        /// Deserializes a binary serialized tree
        /// </summary>
        /// <param name="serializedTree">The MemoryStream object containing the tree</param>
        /// <returns>A deserialized Tree object</returns>
        public Tree DeserializeTree(MemoryStream serializedTree)
        {
            IFormatter formatter = new BinaryFormatter();
            serializedTree.Seek(0, SeekOrigin.Begin);
            return (Tree) formatter.Deserialize(serializedTree);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Recursively searches the tree structure for a page node
        /// </summary>
        /// <param name="tree">The list of page nodes</param>
        /// <param name="contentID">The page node content ID to search for</param>
        /// <param name="page">A reference to a page node that is filled by the method</param>
        private void Search(IEnumerable<Node> tree, Guid contentID, ref Node page)
        {
            foreach (Node p in tree)
            {
                if (p.ContentID == contentID)
                {
                    page = p;
                }
                else
                {
                    Search(p.Pages, contentID, ref page);
                }
            }
        }

        /// <summary>
        /// Recursively searches the tree structure for a page node to delete
        /// </summary>
        /// <param name="tree">The list of page nodes</param>
        /// <param name="contentID">The page node content ID to delete</param>
        private void RemovalSearch(List<Node> tree, Guid contentID)
        {
            foreach (Node p in tree)
            {
                if (p.ContentID == contentID)
                {
                    tree.Remove(tree.Find(n => n.ContentID == contentID));
                    return;
                }
                else
                {
                    RemovalSearch(p.Pages, contentID);
                }
            }
        }

        /// <summary>
        /// Moves all child nodes to the root after deletion of the parent node
        /// </summary>
        /// <param name="parent">The parent node that will be deleted in the next step</param>
        private void MoveChildrenAboveParent(Node parent)
        {
            if (parent.ParentID.HasValue)
            {
                Node grandfatherNode = FindPage(parent.ParentID.Value);

                foreach (Node p in parent.Pages)
                {
                    p.ParentID = parent.ParentID;
                    grandfatherNode.Pages.Add(p);
                }
            }
            else
            {
                // no parent ID specified; move to root
                foreach (Node p in parent.Pages)
                {
                    p.ParentID = null;
                    Pages.Add(p);
                }
            }

            parent.Pages.Clear();
        }

        /// <summary>
        /// Moves node and child nodes to a new, arbitrary location in the tree
        /// </summary>
        /// <param name="page">The page node that will be moved</param>
        /// <param name="parent">The parent node; if none exists, will move to root</param>
        private void MoveNodes(Node page, Node parent)
        {
            Node copy = CopyNodeObject(page);
            DeletePage(page, false);

            if (parent != null)
            {
                copy.ParentID = parent.ContentID;
                parent.Pages.Add(copy);
            }
            else
            {
                // no parent ID specified; move to root
                Pages.Add(copy);
            }
        }

        /// <summary>
        /// Serializes a node for copying
        /// </summary>
        /// <param name="page">The page node to copy</param>
        /// <returns>A new node object</returns>
        private Node CopyNodeObject(Node page)
        {
            MemoryStream node = SerializeNode(page);
            return DeserializeNode(node);
        }

        /// <summary>
        /// Moves a node to a new position within the current level
        /// </summary>
        /// <param name="pageToMove">The page node to move</param>
        /// <param name="anchor">The page node that will be immediately below the pageToMove node</param>
        private void ReorderNodes(Node pageToMove, Node anchor)
        {
            if (pageToMove.ParentID.HasValue)
            {
                Node parent = FindPage(pageToMove.ParentID.Value);

                int currentIndex = parent.Pages.FindIndex(n => n.ContentID == pageToMove.ContentID);
                int desiredIndex = parent.Pages.FindIndex(n => n.ContentID == anchor.ContentID);

                parent.Pages.RemoveAt(currentIndex);
                parent.Pages.Insert(desiredIndex, pageToMove);
            }
            else
            {
                int currentIndex = Pages.FindIndex(n => n.ContentID == pageToMove.ContentID);
                int desiredIndex = Pages.FindIndex(n => n.ContentID == anchor.ContentID);

                Pages.RemoveAt(currentIndex);
                Pages.Insert(desiredIndex, pageToMove);
            }
        }

        #endregion
    }
}