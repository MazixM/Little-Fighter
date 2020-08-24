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
            return GetCollection().Find(player => player.Username == username).Project(player => player.Id).FirstOrDefault() != null;
        }

        public Player GetByUsername(string username)
        {
            return GetCollection().Find(player => player.Username == username).FirstOrDefault();
        }
    }
}