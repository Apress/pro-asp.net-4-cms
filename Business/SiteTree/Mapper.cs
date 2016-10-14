using System.Collections.Generic;
using System.Web.UI.WebControls;
using Business.Operations;

namespace Business.SiteTree
{
    /// <summary>
    /// Handles mapping the custom n-ary tree to the ASP .NET TreeView
    /// </summary>
    public class Mapper
    {
        private readonly List<TreeNode> _nodes;
        private readonly List<Node> _orphans;
        private readonly Tree _tree;

        /// <summary>
        /// Constructor; transforms a Tree into a SiteTree based on site ID.
        /// </summary>
        /// <param name="siteID">The integer site ID value.</param>
        public Mapper(int siteID)
        {
            _nodes = new List<TreeNode>();
            _orphans = new List<Node>();

            var site = new SiteOperations();
            _tree = site.GetSiteTree(siteID);

            BuildTree(_tree.Pages);
            FixOrphans();
        }

        /// <summary>
        /// Used for accessing the nodes in a Tree.
        /// </summary>
        /// <returns>A generic List of Node objects.</returns>
        public List<TreeNode> GetTreeNodes()
        {
            return _nodes;
        }

        /// <summary>
        /// Used for accessing a Tree object.
        /// </summary>
        /// <returns></returns>
        public Tree GetTree()
        {
            return _tree;
        }

        /// <summary>
        /// Handles the heavy lifting of creating an ASP .NET SiteTree via a Tree.
        /// </summary>
        /// <param name="tree">A generic List of Node objects.</param>
        private void BuildTree(List<Node> tree)
        {
            foreach (Node p in tree)
            {
                if (p.ParentID.HasValue)
                {
                    TreeNode parent = _nodes.Find(n => n.Value == p.ParentID.Value.ToString());

                    if (parent == null)
                    {
                        // parent has not been added to tree yet, so page is temporarily "orphaned"
                        _orphans.Add(p);
                    }
                    else
                    {
                        parent.ChildNodes.Add(new TreeNode {Text = p.Title, Value = p.ContentID.ToString()});
                    }
                }
                else
                {
                    _nodes.Add(new TreeNode {Text = p.Title, Value = p.ContentID.ToString()});
                }

                BuildTree(p.Pages);
            }
        }

        /// <summary>
        /// Places all the orphan pages under appropriate parents, if applicable.
        /// </summary>
        private void FixOrphans()
        {
            foreach (Node o in _orphans)
            {
                foreach (TreeNode t in _nodes)
                {
                    FindOrphanParent(t, o);
                }
            }
        }

        /// <summary>
        /// Finds the appropriate parent Node for an orphan Node.
        /// </summary>
        /// <param name="node">The SiteTree TreeNode.</param>
        /// <param name="orphan">The Tree Node orphan.</param>
        private void FindOrphanParent(TreeNode node, Node orphan)
        {
            if (node.Value == orphan.ParentID.Value.ToString())
            {
                node.ChildNodes.Add(new TreeNode {Text = orphan.Title, Value = orphan.ContentID.ToString()});
            }
            else
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    FindOrphanParent(node.ChildNodes[i], orphan);
                }
            }
        }
    }
}