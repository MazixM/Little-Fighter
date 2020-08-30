using Server.Services.Dao;

namespace Server.Services.Model
{
    public class Player : DaoModel
    {
        public string Username { get; set; }
        public string Nick { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int HpMax
        {
            get
            {
                //Can be like that?
                return Durability * 10;
            }
        }
        public int Mana { get; set; }
        public int ManaMax { get; set; }
        public int Exp { get; set; }
        public int ExpMax { get; set; }
        public int Energy { get; set; }
        public int EnergyMax { get; set; }

        //Increase physical damage
        public int Strength { get; set; }
        //Increase physical defence and HP
        public int Durability { get; set; }
        //Increase physical dodge and hit chance
        public int Dexterity { get; set; }
        //Increase magic damage and mana regeneration
        public int Intelligence { get; set; }
        //Increase magic hit chance and physical block chance 
        public int Concentration { get; set; }
        //Increase chance to find something or something else.. depend on some items 
        public int Luck { get; set; }
    }
}
