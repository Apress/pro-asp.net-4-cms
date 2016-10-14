using System;

namespace CommonLibrary.Entities
{
    /// <summary>
    /// Defines the properties of a homepage news entry
    /// </summary>
    public class HomepageNewsEntry
    {
        public int entryID { get; set; }
        public DateTime entryDate { get; set; }
        public string entryAuthor { get; set; }
        public string entryContent { get; set; }
    }
}