using MongoDB.Driver;
using Server.Services.Model;
using System;

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
            try
            {
                //TODO - Nie mam pomysłu na to, jak nie ma takiego rekordu, to dostaje Exeption "Server sent an invalid nonce."
                return GetCollection().Find(player => player.Username == username).Project(player => player.Id).Any();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
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