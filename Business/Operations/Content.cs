using System;
using System.Collections.Generic;
using CommonLibrary.Interfaces;
using CommonLibrary.Entities;

namespace Business.Operations
{
    public class Content
    {
        /// <summary>
        /// Loads the data for a single piece of content.
        /// </summary>
        /// <param name="contentID">the ID of the content to load</param>
        /// <returns>an object that implements IContentEntity</returns>
        public IContentEntity GetContentEntity(Guid contentID)
        {
            var data = new Data.ContentRepository();
            return data.GetContentEntity(contentID);
        }

        /// <summary>
        /// Loads content and configures buckets and embeddables.
        /// </summary>
        /// <param name="contentID">the ID of the content to load</param>
        public IList<IContentRow> LoadContent(Guid contentID)
        {
            var data = new Data.ContentRepository();
            return data.GetContentRows(contentID);
        }

        /// <summary>
        /// Loads the revisions for a particular piece of content.
        /// </summary>
        /// <param name="contentID">the ID of the content to load</param>
        /// <param name="siteID">the site ID of the content</param>
        /// <returns>a list of revision objects</returns>
        public IList<IContentRevision> LoadRevisions(Guid contentID, int siteID)
        {
            var data = new Data.ContentRepository();
            return data.GetContentRevisions(contentID, siteID);
        }

        /// <summary>
        /// Saves a content entity.
        /// </summary>
        /// <param name="e">The IContentEntity object.</param>
        public void SaveEntity(IContentEntity e)
        {
            var data = new Data.ContentRepository();
            data.SaveEntity(e);
        }

        /// <summary>
        /// Saves a content revision.
        /// </summary>
        /// <param name="r">The IContentRevision object.</param>
        public void SaveRevision(IContentRevision r)
        {
            var data = new Data.ContentRepository();
            data.SaveRevision(r);
        }

        /// <summary>
        /// Saves rows of data for 
        /// </summary>
        /// <param name="r">An IContentRevision object.</param>
        /// <param name="rows">A list of content row data.</param>
        public void SaveRows(IContentRevision r, List<ContentRow> rows)
        {
            var data = new Data.ContentRepository();
            foreach (var row in rows)
            {
                data.SaveRow(r, row);
            }
        }
    }
}