namespace CommonLibrary.Interfaces
{
    /// <summary>
    /// Interface that CMS sites are expected to implement.
    /// </summary>
    public interface ISite
    {
        int siteID { get; set; }
        string siteName { get; set; }
        string siteHost { get; set; }
    }
}
