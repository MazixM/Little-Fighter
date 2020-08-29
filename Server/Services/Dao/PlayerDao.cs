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

        public bool ExistsByNick(string nick)
        {
            return GetCollection().Find(player => player.Nick == nick).Project(player => player.Id).FirstOrDefault() != null;
        }
        public bool ExistsByUsername(string username)
        {
            return GetCollection().Find(player => player.Username == username).Project(player => player.Id).FirstOrDefault().HasValue;
        }
        public bool DeletePlayerByUsername(string username)
        {
            if (GetCollection().DeleteOne(player => player.Username == username).DeletedCount > 0)
            {
                return true;
            }
            return false;
        }
        public Player GetByUsername(string username)
        {
            if (ExistsByUsername(username))
            {
                return GetCollection().Find(player => player.Username == username).FirstOrDefault();
            }
            else
            {
                return new Player();
            }
        }
    }
}