using System.Collections.Generic;
using Business.SiteTree;
using CommonLibrary.Entities;
using CommonLibrary.Interfaces;
using Data;

namespace Business.Operations
{
    public class SiteOperations
    {
        /// <summary>
        /// Creates a new site in the CMS.
        /// </summary>
        /// <param name="title">the title of the site</param>
        /// <param name="domain">the domain entry for the site</param>
        public void CreateSite(string title, string domain)
        {
            var siteData = new SiteRepository();
            siteData.CreateSite(title, domain);
        }

        /// <summary>
        /// Updates an existing site in the CMS.
        /// </summary>
        /// <param name="siteID">the integer site ID</param>
        /// <param name="title">the title of the site</param>
        /// <param name="domain">the domain entry for the site</param>
        public void UpdateSite(int siteID, string title, string domain)
        {
            var siteData = new SiteRepository();
            siteData.UpdateSite(siteID, title, domain);
        }

        /// <summary>
        /// Gets a generic list of sites in the CMS
        /// </summary>
        /// <returns>A generic list of ISite objects</returns>
        public IList<ISite> GetAllSites()
        {
            var siteData = new SiteRepository();
            return siteData.GetAllSites();
        }

        /// <summary>
        /// Gets a filled site tree structure
        /// </summary>
        /// <param name="siteID">The integer site ID</param>
        /// <returns>A filled Tree object for the site</returns>
        public Tree GetSiteTree(int siteID)
        {
            var tree = new Tree();
            var data = new SiteRepository();
            IList<IContentEntity> siteContent = data.GetAllSiteContent(siteID);

            LoadRoots(siteContent, ref tree);
            LoadNodes(siteContent, ref tree);

            return tree;
        }

        /// <summary>
        /// Load the root-level nodes first to ensure proper parent relationships are established
        /// </summary>
        /// <param name="nodes">The generic list of tree nodes</param>
        /// <param name="tree">A reference to the tree object</param>
        private void LoadRoots(IList<IContentEntity> nodes, ref Tree tree)
        {
            var items = ((List<IContentEntity>)nodes).FindAll(i => i.ParentID == null);

            foreach (IContentEntity c in items)
            {
                var page = new Node
                               {
                                   ContentID = c.ContentID,
                                   Title = c.Title,
                                   Author = c.Author,
                                   FriendlyUrl = c.FriendlyUrl,
                                   ParentID = c.ParentID,
                                   CurrentRevision = c.CurrentRevision,
                                   Keywords = c.Keywords,
                                   Description = c.Description,
                                   FollowLinks = c.FollowLinks,
                                   Visible = c.Visible
                               };

                tree.Pages.Add(page);
            }
        }

        /// <summary>
        /// Loads the non-root nodes into the tree
        /// </summary>
        /// <param name="nodes">The generic list of tree nodes</param>
        /// <param name="siteContent">A reference to the tree object</param>
        private void LoadNodes(IList<IContentEntity> nodes, ref Tree tree)
        {
            var items = ((List<IContentEntity>)nodes).FindAll(i => i.ParentID != null);

            foreach (IContentEntity c in items)
            {
                var page = new Node
                {
                    ContentID = c.ContentID,
                    Title = c.Title,
                    Author = c.Author,
                    FriendlyUrl = c.FriendlyUrl,
                    ParentID = c.ParentID,
                    CurrentRevision = c.CurrentRevision,
                    Keywords = c.Keywords,
                    Description = c.Description,
                    FollowLinks = c.FollowLinks,
                    Visible = c.Visible
                };

                Node parent = tree.FindPage(c.ParentID.Value);
                tree.InsertPage(page, parent.ContentID);
            }
        }
    }
}