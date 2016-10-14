using System;
using System.Collections.Generic;

namespace Business.SiteTree
{
    [Serializable]
    public class Node
    {
        // Basic settings (ID, who wrote it, etc.)
        public Node()
        {
            Pages = new List<Node>();
        }

        public Guid ContentID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string FriendlyUrl { get; set; }

        // SEO-specific settings
        public string Keywords { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
        public bool FollowLinks { get; set; }

        // Tree-specific settings
        public List<Node> Pages { get; set; }
        public Guid? ParentID { get; set; }
        public Guid? CurrentRevision { get; set; }
    }
}