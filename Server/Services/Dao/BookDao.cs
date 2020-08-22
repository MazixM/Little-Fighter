using MongoDB.Driver;
using Server.Services.Model;

namespace Server.Services.Dao
{
    public class BookDao : AbstractDao<Book>
    {
        public BookDao(IMongoDatabase database) : base(database)
        {
        }

        protected override string CollectionName => "book";
    }
}