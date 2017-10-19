using FcNet.DataHelper.Common;
using FcNet.DataHelper.Factory.Dapper;
using FcNet.DataHelper.Factory.Mongo;
using System;
using System.Data.Common;

namespace FcNet.DataHelper
{
    public static class DataFactory<TEntity> where TEntity : class
    {
        private static string connString = "";
        private static DbConnection _conn = null;
        private static FrameworkType _type = FrameworkType.Dapper;
        private static ProviderType _provider = ProviderType.SqlClient;

        public static IDataFactory<TEntity> GetInstance(string connectionString, FrameworkType type = FrameworkType.Dapper, ProviderType provider = ProviderType.SqlClient)
        {
            _type = type;
            _provider = provider;
            connString = connectionString;

            switch (type)
            {
                case FrameworkType.MongoDB:
                    return new MongoFactory<TEntity>(connectionString);
                case FrameworkType.Entity:
                    throw new NotImplementedException();
                case FrameworkType.Dapper:
                    _conn = new ConnectionProvider(connString, GetProvider(provider)).GetConnection();
                    return new DapperFactory<TEntity>(_conn);
                default:
                    throw new NotImplementedException();
            }
        }

        private static string GetProvider(ProviderType type)
        {
            switch (type)
            {
                case ProviderType.SqlClient:
                    return "System.Data.SqlClient";
                case ProviderType.SQLite:
                    return "System.Data.SQLite";
                case ProviderType.MySQL:
                    return "MySql.Data";
                case ProviderType.PostgreSQL:
                    throw new NotImplementedException();
                case ProviderType.MongoDB:
                    throw new NotImplementedException();
                default: return "";
            }
        }
    }
}
