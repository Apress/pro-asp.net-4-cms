using System;

namespace CommonLibrary.Interfaces
{
    /// <summary>
    /// Interface that defines a piece of content in the CMS.
    /// </summary>
    public interface IContentEntity
    {
        string Author { get; set; }
        Guid ContentID { get; set; }
        Guid? CurrentRevision { get; set; }
        string FriendlyUrl { get; set; }
        Guid? ParentID { get; set; }
        int SiteID { get; set; }
        string Title { get; set; }
        string Keywords { get; set; }
        string Description { get; set; }
        bool Visible { get; set; }
        bool FollowLinks { get; set; }
    }
}
