using System.Collections.Generic;
using System.Threading.Tasks;

namespace FcNet.DataHelper.Dapper
{
    public interface DapperRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetData(object filter);
        Task<IEnumerable<TEntity>> GetDataAsync(object filter);
    }
}
