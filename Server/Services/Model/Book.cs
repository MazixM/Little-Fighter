using Server.Services.Dao;

namespace Server.Services.Model
{
    public class Book : DaoModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}