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
        public Player GetPlayer(string username)
        {
            return _playerDao.GetPlayer(username);
        }
        public bool IsNickAvailable(string nick)
        {
            return _playerDao.IsNickAvailable(nick);
        }
        public bool IsUsernameExist(string username)
        {
            return _playerDao.IsUsernameExist(username);
        }
    }
}