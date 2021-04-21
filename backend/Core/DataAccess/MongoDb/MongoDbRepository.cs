using Core.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.MongoDb
{
    public class MongoDbRepository<T> : IRepository<T>
        where T : IMongoDbEntity
    {
        protected readonly IMongoCollection<T> _collection;
        private readonly MongoDbSettings _settings;

        protected MongoDbRepository(IOptions<MongoDbSettings> options,string collectionName = "")
        {
            _settings = options.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var db = client.GetDatabase(_settings.Database);

            if (collectionName == "")
                _collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
            else
                _collection = db.GetCollection<T>(collectionName);
        }
        public T Add(T entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public bool AddRange(IEnumerable<T> entities)
        {
            return (_collection.BulkWrite((IEnumerable<WriteModel<T>>)entities)).IsAcknowledged;

        }

        public T Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Delete(Expression<Func<T, bool>> filter)
        {
            return _collection.FindOneAndDelete(filter);
        }

        public List<T> Get(Expression<Func<T, bool>> filter = null)
        {
            var entities = filter == null
                ? _collection.AsQueryable()
                : _collection.AsQueryable().Where(filter);
            return entities.ToList();
        }

        public T GetByFilter(Expression<Func<T, bool>> filter)
        {
            return _collection.Find<T>(filter).FirstOrDefault();
        }

        public T Update(T entity, Expression<Func<T, bool>> filter)
        {
            return _collection.FindOneAndReplace(filter, entity);
        }
    }
}
