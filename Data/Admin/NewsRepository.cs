using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CommonLibrary.Entities;

namespace Data.Admin
{
    public class NewsRepository
    {
        private readonly Factory _factory;

        public NewsRepository()
        {
            _factory = new Factory();
        }

        /// <summary>
        /// Returns a list of news entries for the admin homepage
        /// </summary>
        /// <returns>A strongly-typed list of HomepageNewsEntry objects</returns>
        public IList<HomepageNewsEntry> GetNewsEntries()
        {
            HomepageNewsEntry eRow;
            var list = new List<HomepageNewsEntry>();

            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("GetAdminNewsEntries");
                comm.CommandType = CommandType.StoredProcedure;

                try
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        eRow = new HomepageNewsEntry
                                   {
                                       entryID = reader.GetInt32(0),
                                       entryDate = reader.GetDateTime(1),
                                       entryAuthor = reader.GetString(2),
                                       entryContent = reader.GetString(3)
                                   };
                        list.Add(eRow);
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
        /// Saves a new entry for the admin homepage
        /// </summary>
        /// <param name="author">The author of the entry</param>
        /// <param name="entry">The text of the entry itself</param>
        public void Save(string author, string entry)
        {
            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("SaveNewsEntry");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@author", author);
                comm.Parameters.AddWithValue("@entry", entry);

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
        /// Deletes a news entry from the homepage
        /// </summary>
        /// <param name="entryID">The entryID to remove</param>
        public void Delete(int entryID)
        {
            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("DeleteNewsEntry");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@entryID", entryID);

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