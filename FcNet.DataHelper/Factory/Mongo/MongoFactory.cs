using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace FcNet.DataHelper.Factory.Mongo
{
    public class MongoFactory<TEntity> : IDataFactory<TEntity> where TEntity : class
    {
        private IMongoDatabase _database;
        private IMongoCollection<TEntity> _collection;

        protected IMongoCollection<TEntity> Collection
        {
            get { return _collection; }
        }

        public MongoFactory(string connectionString)
        {
            if (connectionString == null) { throw new ArgumentException("Missing MongoDB connection string"); }

            MongoClient client = new MongoClient(connectionString);
            MongoUrl mongoUrl = MongoUrl.Create(connectionString);

            _database = client.GetDatabase(mongoUrl.DatabaseName);
            _collection = _database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower());
        }

        public IEnumerable<TEntity> All()
        {
            return _collection.Find(new BsonDocument()).ToEnumerable();
        }

        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await Task.FromResult(All());
        }

        public IEnumerable<TEntity> GetData(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetDataAsync(Expression<Func<TEntity, bool>> filter)
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

        public int Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Add(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(IEnumerable<TEntity> entities)
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

        public int InstertOrUpdate(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> InstertOrUpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
