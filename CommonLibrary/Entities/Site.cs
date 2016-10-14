using CommonLibrary.Interfaces;

namespace CommonLibrary.Entities
{
    /// <summary>
    /// Defines the properties of a CMS site.
    /// </summary>
    public class Site : ISite
    {
        public int siteID { get; set; }
        public string siteName { get; set; }
        public string siteHost { get; set; }
    }
}