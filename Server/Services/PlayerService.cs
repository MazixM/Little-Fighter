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

        public Player CreatePlayer(string nick)
        {
            Player player = new Player
            {
                Nick = nick
            };

            return _playerDao.Insert(player);
        }
        public bool IsNickAvailable(string nick)
        {
            return _playerDao.IsNickAvailable(nick);
        }
    }
}