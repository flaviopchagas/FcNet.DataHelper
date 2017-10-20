using Dapper;
using FcNet.DataHelper.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
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

        public bool Insert(TEntity entity)
        {
            try
            {
                int id = conn.Query<int>(queryGen.GenetareInsert(entity), entity).First();
                return id > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            return await Task.FromResult(Insert(entity));
        }

        public bool Remove(object key)
        {
            try
            {
                int rows = conn.Execute(queryGen.GenerateDelete(key), key);
                return rows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> RemoveAsync(object key)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entity)
        {
            try
            {
                int rows = conn.Execute(queryGen.GenerateUpdate(entity), entity);
                return rows > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
