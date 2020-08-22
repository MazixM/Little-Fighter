using Server.Services.Dao;
using Server.Services.Model;

namespace Server.Services
{
    public class BookService
    {
        private readonly BookDao _bookDao;

        public BookService(BookDao bookDao)
        {
            _bookDao = bookDao;
        }

        public Book CreateBook(string title, string author)
        {
            Book book = new Book
            {
                Title = title,
                Author = author
            };

            return _bookDao.Insert(book);
        }
    }
}