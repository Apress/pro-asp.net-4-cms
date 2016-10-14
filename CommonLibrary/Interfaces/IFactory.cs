using System;
using System.Data;

namespace CommonLibrary.Interfaces
{
    /// <summary>
    /// Defines the methods that a data store must expose.
    /// </summary>
    public interface IDataFactory
    {
        IDbCommand GetCommand(string commandText);
        IDbConnection GetConnection(string connectionName);
    }
}
