using Server.Services.Dao;

namespace Server.Services.Model
{
    public class Player : DaoModel
    {
        public string Username { get; set; }
        public string Nick { get; set; }
    }
}
