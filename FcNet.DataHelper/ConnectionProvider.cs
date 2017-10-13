using System;
using System.Data.Common;

namespace FcNet.DataHelper
{
    public class ConnectionProvider : IDisposable
    {
        private DbConnection _conn = null;
        private string _connString = "";
        private DbProviderFactory _factory = null;

        public ConnectionProvider(string connectionString, string factory)
        {
            _connString = connectionString;
            _factory = DbProviderFactories.GetFactory(factory);
        }

        public DbConnection GetConnection()
        {
            _conn = _factory.CreateConnection();
            _conn.ConnectionString = _connString;
            _conn.Open();
            return _conn;
        }

        public void Dispose()
        {
            _conn.Close();
            _conn = null;
        }
    }
}
