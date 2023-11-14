using AutoMapper;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.DAO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository
{
    public class BookRepository : IBookRepository
    {
        private BookDAO bookDAO;
        private IMapper mapper;

        public BookRepository(BookDAO bookDAO, IMapper mapper)
        {
            this.bookDAO = bookDAO;
            this.mapper = mapper;
        }

        public void AddBook(AddBookRequest addBookRequest)
        {
            bookDAO.AddBook(mapper.Map<Book>(addBookRequest));
        }

        public void Delete(object book)
        {
            throw new NotImplementedException();
        }

        public List<BookDTO> FindBooksByBookIds(List<long> bookIdList)
        {
            return bookDAO.FindBooksByBookIds(bookIdList);
        }

        public BooksResponse FindByCategory(string category, int page, int size)
        {
            return bookDAO.FindByCategory(category, page, size);
        }

        public Book findById(int bookId)
        {
            return mapper.Map<Book>(bookDAO.getById(bookId));
        }

        public BooksResponse FindByTitleContaining(string title, int page, int size)
        {
            return bookDAO.FindByTitleContaining(title, page, size);
        }

        public BooksResponse GetAll(int page, int size)
        {
            return bookDAO.GetBooks(page, size);
        }

        public BookDTO GetById(int id)
        {
            return bookDAO.getById(id);
        }

        public void save(Book book)
        {
            bookDAO.save(book);
        }
    }
}
