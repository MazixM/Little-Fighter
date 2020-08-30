using Server.Services.Dao;

namespace Server.Services.Model
{
    public class Player : DaoModel
    {
        public string Username { get; set; }
        public string Nick { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int ExpMax { get; set; }
        public int Energy { get; set; }
        public int EnergyMax { get; set; }
    }
}
