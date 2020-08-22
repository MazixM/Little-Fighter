using MongoDB.Driver;
using Server.Services.Model;

namespace Server.Services.Dao
{
    public class PlayerDao : AbstractDao<Player>
    {
        public PlayerDao(IMongoDatabase database) : base(database)
        {
        }

        protected override string CollectionName => "player";
    }
}