using System.Collections.Generic;
using CommonLibrary.Entities;
using Data.Admin;

namespace Business.Admin
{
    public class HomepageNews
    {
        /// <summary>
        /// Loads a list of entries for the admin homepage
        /// </summary>
        /// <returns>A generic list of HomepageNewsEntry objects</returns>
        public IList<HomepageNewsEntry> GetNewsEntries()
        {
            var data = new NewsRepository();
            return data.GetNewsEntries();
        }

        /// <summary>
        /// Saves a news entry for the admin homepage
        /// </summary>
        /// <param name="author">The name of the author</param>
        /// <param name="entry">The actual text of the entry</param>
        public void Save(string author, string entry)
        {
            var data = new NewsRepository();
            data.Save(author, entry);
        }

        /// <summary>
        /// Deletes a news entry for the admin homepage
        /// </summary>
        /// <param name="entryID">The entryID to remove</param>
        public void Delete(int entryID)
        {
            var data = new NewsRepository();
            data.Delete(entryID);
        }
    }
}