using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Server.Services.Dao
{
    public abstract class DaoModel
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId? Id { get; set; }

        public DateTime? CreationTime { get; set; }
        public DateTime? ModificationTime { get; set; }
    }
}