using System;
using System.Data;
using System.Data.SqlClient;

namespace Preludio.DataAccess
{
    public class SqlConnectionManager : IConnectionManager, IDisposable
    {
        private string _connectionString;
        private IDbConnection connection;
        public SqlConnectionManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection Get()
        {
            if (connection == null)
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();
            }
            return connection;
        }

        public void Override(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public void Dispose()
        {
            connection?.Close();
            connection?.Dispose();
        }
    }
}