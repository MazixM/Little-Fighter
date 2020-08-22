using Server.Services.Dao;
using Server.Services.Model;

namespace Server.Services
{
    public class PlayerService
    {
        private readonly PlayerDao _playerDao;

        public PlayerService(PlayerDao playerDao)
        {
            _playerDao = playerDao;
        }

        public Player CreatePlayer(string nick, string username)
        {
            Player player = new Player
            {
                Nick = nick,
                Username = username
            };

            return _playerDao.Insert(player);
        }
    }
}