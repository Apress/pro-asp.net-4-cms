using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CommonLibrary.Entities;
using CommonLibrary.Interfaces;

namespace Data
{
    public class SiteRepository
    {
        private readonly Factory _factory;

        public SiteRepository()
        {
            _factory = new Factory();
        }

        /// <summary>
        /// Creates a new site in the CMS.
        /// </summary>
        /// <param name="title">the title of the site</param>
        /// <param name="domain">the domain entry for the site</param>
        public void CreateSite(string title, string domain)
        {
            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("CreateSite");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@title", title);
                comm.Parameters.AddWithValue("@host", domain);

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
        /// Updates an existing site in the CMS.
        /// </summary>
        /// <param name="siteID">the integer site ID</param>
        /// <param name="title">the title of the site</param>
        /// <param name="domain">the domain entry for the site</param>
        public void UpdateSite(int siteID, string title, string domain)
        {
            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("UpdateSite");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@siteID", siteID);
                comm.Parameters.AddWithValue("@title", title);
                comm.Parameters.AddWithValue("@host", domain);

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
        /// Get a list of sites that are of type ISite
        /// </summary>
        /// <returns>A generic list of ISite objects</returns>
        public IList<ISite> GetAllSites()
        {
            var sites = new List<ISite>();

            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("GetAllSites");
                comm.CommandType = CommandType.StoredProcedure;

                try
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        ISite site = new CommonLibrary.Entities.Site();
                        site.siteID = reader.GetInt32(0);
                        site.siteName = reader.GetString(1);
                        site.siteHost = reader.GetString(2);
                        sites.Add(site);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return sites;
        }

        /// <summary>
        /// Gets a list of ContentEntity objects describing all pages in a site
        /// </summary>
        /// <param name="siteID">The integer site ID</param>
        /// <returns>A list of ContentEntity objects</returns>
        public IList<IContentEntity> GetAllSiteContent(int siteID)
        {
            var sitePages = new List<IContentEntity>();
            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                var comm = (SqlCommand)_factory.GetCommand("GetAllSiteContent");
                comm.Parameters.AddWithValue("@siteID", siteID);
                comm.CommandType = CommandType.StoredProcedure;

                try
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        var e = new ContentEntity();
                        e.ContentID = reader.GetGuid(0);
                        if (!reader.IsDBNull(1)) e.Title = reader.GetString(1);
                        if (!reader.IsDBNull(2)) e.ParentID = reader.GetGuid(2);
                        e.SiteID = reader.GetInt32(3);
                        if (!reader.IsDBNull(4)) e.Author = reader.GetString(4);
                        if (!reader.IsDBNull(5)) e.FriendlyUrl = reader.GetString(5);
                        if (!reader.IsDBNull(6)) e.CurrentRevision = reader.GetGuid(6);
                        if (!reader.IsDBNull(7)) e.Keywords = reader.GetString(7);
                        if (!reader.IsDBNull(8)) e.Description = reader.GetString(8);
                        if (!reader.IsDBNull(9))
                        {
                            e.Visible = reader.GetBoolean(9);
                        }
                        else
                        {
                            e.Visible = false;
                        }
                        if (!reader.IsDBNull(10))
                        {
                            e.FollowLinks = reader.GetBoolean(10);
                        }
                        else
                        {
                            e.FollowLinks = false;
                        }
                        sitePages.Add(e);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return sitePages;
        }
    }
}