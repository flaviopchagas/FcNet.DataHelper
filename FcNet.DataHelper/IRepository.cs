using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FcNet.DataHelper
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();

        IEnumerable<TEntity> GetData(string query, object parameters);
        Task<IEnumerable<TEntity>> GetDataAsync(string query, object parameters);

        IEnumerable<TEntity> GetDataPaginated(int top, int skip, Func<TEntity, object> orderBy, bool ascending = true);
        Task<IEnumerable<TEntity>> GetDataPaginatedAsync(int top, int skip, Func<TEntity, object> orderBy, bool ascending = true);

        TEntity Find(object pksFields);
        Task<TEntity> FindAsync(object pksFields);

        int Add(TEntity entity);
        Task<int> AddAsync(TEntity entity);

        int Add(IEnumerable<TEntity> entities);
        Task<int> AddAsync(IEnumerable<TEntity> entities);

        void Remove(object key);
        Task RemoveAsync(object key);

        int Update(TEntity entity, object pks);
        Task<int> UpdateAsync(TEntity entity, object pks);

        int InstertOrUpdate(TEntity entity, object pks);
        Task<int> InstertOrUpdateAsync(TEntity entity, object pks);
    }
}
