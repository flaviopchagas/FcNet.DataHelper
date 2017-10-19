using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FcNet.DataHelper
{
    public interface IDataFactory<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();

        IEnumerable<TEntity> GetData(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetDataAsync(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> GetDataPaginated(int top, int skip, Func<TEntity, object> orderBy, bool ascending = true);
        Task<IEnumerable<TEntity>> GetDataPaginatedAsync(int top, int skip, Func<TEntity, object> orderBy, bool ascending = true);

        int Add(TEntity entity);
        Task<int> AddAsync(TEntity entity);

        int Add(IEnumerable<TEntity> entities);
        Task<int> AddAsync(IEnumerable<TEntity> entities);

        void Remove(object key);
        Task RemoveAsync(object key);

        int Update(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);

        int InstertOrUpdate(TEntity entity);
        Task<int> InstertOrUpdateAsync(TEntity entity);
    }
}
