using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Data.Admin
{
    public class PluginManager
    {
        private readonly Factory _factory;

        public PluginManager()
        {
            _factory = new Factory();
        }

        /// <summary>
        /// Clears the Embeddables table.
        /// </summary>
        public void ClearPlugins()
        {
            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("ClearEmbeddables");
                comm.CommandType = CommandType.StoredProcedure;
                
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
        /// Adds an Embeddable to the Embeddables table.
        /// </summary>
        /// <param name="embeddableID">the ID assigned to the control</param>
        /// <param name="name">the friendly name of the control</param>
        public void AddEmbeddableToDatabase(int embeddableID, string name)
        {
            using (SqlConnection conn = (SqlConnection)_factory.GetConnection())
            {
                SqlCommand comm = (SqlCommand)_factory.GetCommand("InsertEmbeddable");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@embeddableID", embeddableID);
                comm.Parameters.AddWithValue("@name", name);

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
