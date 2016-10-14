using System;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class RewriteRepository
    {
        private readonly Factory _factory;

        public RewriteRepository()
        {
            _factory = new Factory();
        }

        /// <summary>
        /// Returns a content ID for a particular request
        /// </summary>
        /// <param name="request">the incoming request</param>
        /// <param name="host">the host for this request (i.e. "localhost")</param>
        /// <returns>a nullable content ID for the content if available</returns>
        public Guid? GetIDByPrimaryUrl(string request, string host)
        {
            Guid? contentID = null;

            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("GetContentIDByPrimaryURL");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@url", request);
                comm.Parameters.AddWithValue("@host", host);

                try
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        contentID = (Guid) reader[0];
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return contentID;
        }

        /// <summary>
        /// Returns a friendly URL for a content ID
        /// </summary>
        /// <param name="contentID">the content ID to check</param>
        /// <param name="host">the host for this request (i.e. "localhost")</param>
        /// <returns>a string with the friendly URL if available</returns>
        public string GetPrimaryUrlByID(Guid contentID, string host)
        {
            string friendlyURL = String.Empty;

            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("GetPrimaryURLByContentID");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@contentID", contentID);
                comm.Parameters.AddWithValue("@host", host);

                try
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        friendlyURL = reader.GetString(0);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return friendlyURL;
        }

        /// <summary>
        ///  Attempts to pull up a primary URL based on an incoming alias
        /// </summary>
        /// <param name="request">the incoming request</param>
        /// <param name="host">the host for this request (i.e. "localhost")</param>
        /// <returns>a primary URL string, if available</returns>
        public string GetPrimaryUrlByAlias(string request, string host)
        {
            string primaryURL = String.Empty;

            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("GetPrimaryURLByAlias");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@aliasUrl", request);
                comm.Parameters.AddWithValue("@host", host);

                try
                {
                    conn.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        primaryURL = reader.GetString(0);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return primaryURL;
        }
    }
}