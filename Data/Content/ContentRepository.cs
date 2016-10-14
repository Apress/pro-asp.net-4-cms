using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CommonLibrary.Interfaces;
using CommonLibrary.Entities;

namespace Data
{
    public class ContentRepository
    {
        private readonly Factory _factory;

        public ContentRepository()
        {
            _factory = new Factory();
        }

        /// <summary>
        /// Loads the data for a single piece of content.
        /// </summary>
        /// <param name="contentID">the ID of the content to load</param>
        /// <returns>an object that implements IContentEntity</returns>
        public IContentEntity GetContentEntity(Guid contentID)
        {
            IContentEntity entity = new ContentEntity();

            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("GetContentEntity");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@contentID", contentID);

                try
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        entity.ContentID = contentID;
                        if (!reader.IsDBNull(1)) entity.Title = reader.GetString(1);
                        if (!reader.IsDBNull(2)) entity.ParentID = reader.GetGuid(2);
                        if (!reader.IsDBNull(3)) entity.SiteID = reader.GetInt32(3);
                        if (!reader.IsDBNull(4)) entity.Author = reader.GetString(4);
                        if (!reader.IsDBNull(5)) entity.FriendlyUrl = reader.GetString(5);
                        if (!reader.IsDBNull(6)) entity.CurrentRevision = reader.GetGuid(6);
                        if (!reader.IsDBNull(7)) entity.Keywords = reader.GetString(7);
                        if (!reader.IsDBNull(8)) entity.Description = reader.GetString(8);
                        if (!reader.IsDBNull(9)) entity.Visible = reader.GetBoolean(9);
                        if (!reader.IsDBNull(10)) entity.FollowLinks = reader.GetBoolean(10);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return entity;
        }

        /// <summary>
        /// Returns a list of content rows for use in populating buckets and embeddables
        /// </summary>
        /// <returns>a generic list of ContentRow objects</returns>
        public IList<IContentRow> GetContentRows(Guid contentID)
        {
            var list = new List<IContentRow>();

            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("GetCompletedContent");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@contentID", contentID);

                try
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        var cRow = new ContentRow
                                              {
                                                  contentID = contentID,
                                                  bucketID = reader.GetInt32(0),
                                                  embeddableID = reader.GetInt32(1),
                                              };
                        list.Add(cRow);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Returns a list of content revisions.
        /// </summary>
        /// <param name="contentID">the ID of the content to load</param>
        /// <param name="siteID">the site ID of the content</param>
        /// <returns>a list of revision objects</returns>
        public IList<IContentRevision> GetContentRevisions(Guid contentID, int siteID)
        {
            var list = new List<IContentRevision>();

            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("GetRevisionsForContent");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@contentID", contentID);
                comm.Parameters.AddWithValue("@siteID", siteID);
                try
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        var cRow = new ContentRevision
                                       {
                                           VersionID = reader.GetGuid(0),
                                           ContentID = contentID,
                                           IsLive = reader.GetBoolean(1),
                                           TimeStamp = reader.GetDateTime(2)
                                       };
                        list.Add(cRow);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Saves a content entity.
        /// </summary>
        /// <param name="e">The IContentEntity object.</param>
        public void SaveEntity(IContentEntity e)
        {
            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("UpsertContentEntity");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@contentID", e.ContentID);
                comm.Parameters.AddWithValue("@title", e.Title);
                comm.Parameters.AddWithValue("@author", e.Author);
                comm.Parameters.AddWithValue("@friendlyUrl", e.FriendlyUrl);
                comm.Parameters.AddWithValue("@description", e.Description);
                comm.Parameters.AddWithValue("@keywords", e.Keywords);
                comm.Parameters.AddWithValue("@visible", e.Visible);
                comm.Parameters.AddWithValue("@followLinks", e.FollowLinks);
                if (e.ParentID == null)
                {
                    comm.Parameters.AddWithValue("@parentID", DBNull.Value);
                }
                else
                {
                    comm.Parameters.AddWithValue("@parentID", e.ParentID);
                }
                comm.Parameters.AddWithValue("@currentRevision", e.CurrentRevision);
                comm.Parameters.AddWithValue("@siteID", e.SiteID);

                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Saves a content revision.
        /// </summary>
        /// <param name="r">The IContentRevision object.</param>
        public void SaveRevision(IContentRevision r)
        {
            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("InsertContentRevision");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@versionID", r.VersionID);
                comm.Parameters.AddWithValue("@contentID", r.ContentID);
                comm.Parameters.AddWithValue("@isLive", r.IsLive);
                comm.Parameters.AddWithValue("@timeStamp", r.TimeStamp);

                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Saves a row of content control information.
        /// </summary>
        /// <param name="r">An IContentRevision object.</param>
        /// <param name="row">A ContentRow object.</param>
        public void SaveRow(IContentRevision r, ContentRow row)
        {
            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("InsertContentRow");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@versionID", r.VersionID);
                comm.Parameters.AddWithValue("@bucketID", row.bucketID);
                comm.Parameters.AddWithValue("@embeddableID", row.embeddableID);
                
                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}