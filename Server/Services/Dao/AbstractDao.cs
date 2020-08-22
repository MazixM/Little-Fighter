using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Server.Services.Dao
{
    public abstract class AbstractDao<T> where T : DaoModel
    {
        protected abstract string CollectionName { get; }
        private readonly IMongoDatabase _database;

        protected AbstractDao(IMongoDatabase database)
        {
            _database = database;
        }

        public T Insert(T model)
        {
            model.CreationTime ??= DateTime.UtcNow;
            model.ModificationTime ??= DateTime.UtcNow;

            GetCollection().InsertOne(model);

            return model;
        }

        public T Get(ObjectId id)
        {
            return GetCollection().Find(x => x.Id == id).First();
        }

        protected IMongoCollection<T> GetCollection()
        {
            return _database.GetCollection<T>(CollectionName);
        }
    }
}