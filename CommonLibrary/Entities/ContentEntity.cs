using System;
using CommonLibrary.Interfaces;

namespace CommonLibrary.Entities
{
    /// <summary>
    /// Defines the properties of content within the CMS
    /// </summary>
    public class ContentEntity : IContentEntity
    {
        public Guid ContentID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string FriendlyUrl { get; set; }
        public Guid? ParentID { get; set; }
        public int SiteID { get; set; }
        public Guid? CurrentRevision { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
        public bool FollowLinks { get; set;}
    }
}