using Dapper;
using FcNet.DataHelper.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FcNet.DataHelper.Factory.Dapper
{
    public class DapperFactory<TEntity> : IDataFactory<TEntity> where TEntity : class
    {
        private readonly DbConnection conn;
        private QueryGenerator<TEntity> queryGen;

        public DapperFactory(DbConnection connection)
        {
            conn = connection;
            queryGen = new QueryGenerator<TEntity>();
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
            return conn.Query<TEntity>(queryGen.GenerateSelect()).AsList();
        }

        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await Task.FromResult(All());
        }

        public IEnumerable<TEntity> GetData(Expression<Func<TEntity, bool>> filter)
        {
            return conn.Query<TEntity>(queryGen.GenerateSelect(filter)).AsList();
        }

        public async Task<IEnumerable<TEntity>> GetDataAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Task.FromResult(GetData(filter));
        }

        public IEnumerable<TEntity> GetDataPaginated(int top, int skip, Func<TEntity, object> orderBy, bool ascending = true)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetDataPaginatedAsync(int top, int skip, Func<TEntity, object> orderBy, bool ascending = true)
        {
            throw new NotImplementedException();
        }

        public int InstertOrUpdate(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> InstertOrUpdateAsync(TEntity entity)
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

        public int Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
