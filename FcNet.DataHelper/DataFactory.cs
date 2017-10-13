using FcNet.DataHelper.Common;
using FcNet.DataHelper.Factory.Dapper;
using System.Data.Common;

namespace FcNet.DataHelper
{
    public static class DataFactory<TEntity> where TEntity : class
    {
        private static string connString = "";
        private static DbConnection _conn = null;

        public static IDataFactory<TEntity> GetInstance(string connectionString, FrameworkType type = FrameworkType.Dapper, ProviderType provider = ProviderType.SqlClient)
        {
            connString = connectionString;
            ConnectionProvider conn = new ConnectionProvider(connString, GetProvider(provider));
            _conn = conn.GetConnection();

            switch (type)
            {
                case FrameworkType.MongoDB: return null;
                case FrameworkType.Entity: return null;
                case FrameworkType.Dapper: return new DapperFactory<TEntity>(_conn);
                default: return null;
            }
        }

        private static string GetProvider(ProviderType type)
        {
            switch (type)
            {
                case ProviderType.SqlClient: return "System.Data.SqlClient";
                case ProviderType.SQLite: return "System.Data.SQLite";
                case ProviderType.MySQL: return "MySql.Data";
                case ProviderType.PostgreSQL: return "";
                case ProviderType.MongoDB: return "";
                default: return "";
            }
        }
    }
}
