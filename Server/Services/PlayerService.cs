using Server.Services.Dao;
using Server.Services.Model;
using SolutionShared;

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
            if (InputValidationCheck.Nick(nick) && !IsUsernameExist(username) && IsNickAvailable(nick))
            {
                Player player = new Player
                {
                    Nick = nick,
                    Username = username,
                    Level = 1,
                    Exp = 0,
                    ExpMax = 10,
                    Energy = 10,
                    EnergyMax = 10
                };

                return _playerDao.Insert(player);
            }
            else
            {
                return new Player();
            }
        }
        public Player GetPlayer(string username)
        {
            return _playerDao.GetByUsername(username);
        }
        public bool DeletePlayer(string username)
        {
            return _playerDao.DeletePlayerByUsername(username);
        }
        public bool IsNickAvailable(string nick)
        {
            return !_playerDao.ExistsByNick(nick);
        }
        public bool IsUsernameExist(string username)
        {
            return _playerDao.ExistsByUsername(username);
        }
    }
}