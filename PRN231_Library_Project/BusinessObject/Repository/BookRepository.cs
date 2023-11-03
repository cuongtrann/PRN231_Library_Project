using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.DAO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository
{
    public class BookRepository : IBookRepository
    {
        private BookDAO bookDAO;

        public BookRepository(BookDAO bookDAO)
        {
            this.bookDAO = bookDAO;
        }

        public List<Book> GetAll(int page, int size)
        {
            return bookDAO.GetBooks(page, size);
        }
    }
}
