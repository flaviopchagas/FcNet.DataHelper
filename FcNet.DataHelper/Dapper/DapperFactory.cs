using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace FcNet.DataHelper.Dapper
{
    public class DapperFactory<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbConnection conn;
        private string table;
        private string select;
        private string selectPaging;
        private string update;
        private string create;
        private string delete;

        public DapperFactory(DbConnection connection)
        {
            conn = connection;
            table = GetType().GenericTypeArguments[0].Name;

            QueryHelper<TEntity> query = new QueryHelper<TEntity>();
            select = query.GetSelect();
        }

        public int Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Add(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> All()
        {
            return conn.Query<TEntity>(select).AsList();
        }

        public Task<IEnumerable<TEntity>> AllAsync()
        {
            IEnumerable<TEntity> result = conn.Query<TEntity>(select).AsList();

            return Task.FromResult(result);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TEntity Find(object pksFields)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(object pksFields)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetData(object filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetData(string query, object parameters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetDataAsync(object filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetDataAsync(string query, object parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetDataPaginated(int top, int skip, Func<TEntity, object> orderBy, bool ascending = true)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetDataPaginatedAsync(int top, int skip, Func<TEntity, object> orderBy, bool ascending = true)
        {
            throw new NotImplementedException();
        }

        public int InstertOrUpdate(TEntity entity, object pks)
        {
            throw new NotImplementedException();
        }

        public Task<int> InstertOrUpdateAsync(TEntity entity, object pks)
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(object key)
        {
            throw new NotImplementedException();
        }

        public int Update(TEntity entity, object pks)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(TEntity entity, object pks)
        {
            throw new NotImplementedException();
        }
    }
}
