using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CommonLibrary.Interfaces;

namespace Data
{
    public class Factory : IDataFactory
    {
        private SqlConnection _conn;

        /// <summary>
        /// Returns a SQL connection object using a supplied connection string, if available.
        /// </summary>
        /// <param name="connectionName">the name of the connection string to use</param>
        /// <returns>A SQL connection</returns>
        public IDbConnection GetConnection(string connectionName = "CMS")
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);
            return _conn;
        }

        /// <summary>
        /// Returns a SQL command.
        /// </summary>
        /// <param name="commandText">the SQL command or stored procedure to execute</param>
        /// <returns>A SQL command</returns>
        public IDbCommand GetCommand(string commandText)
        {
            return new SqlCommand(commandText, _conn);
        }
    }
}