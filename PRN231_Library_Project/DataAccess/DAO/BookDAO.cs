using AutoMapper;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.DataAccess.DAO
{
    public class BookDAO
    {
        private readonly PRN231_Library_ProjectContext context;
        private readonly IMapper mapper;

        public BookDAO(PRN231_Library_ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public BooksResponse FindByTitleContaining(string title, int page, int size)
        {
            var startIndex = page * size;
            List<Book> books = context.Books.Where(x => x.Title.ToLower().Contains(title.ToLower())).Skip(startIndex).Take(size).ToList();
            List<BookDTO> bookDTOs = mapper.Map<List<BookDTO>>(books);
            int totalElements = context.Books.Where(x => x.Title.ToLower().Contains(title.ToLower())).Count();
            int totalPages = (int)Math.Ceiling((double)totalElements / size);
            BooksResponse booksResponse = new BooksResponse()
            {
                Books = bookDTOs,
                Page = new Page()
                {
                    TotalElements = totalElements,
                    TotalPages = totalPages,
                    Size = size
                }
            };
            return booksResponse;
        }

        public BooksResponse GetBooks(int page, int size)
        {
            // Tính toán vị trí bắt đầu và kết thúc của danh sách sản phẩm
            var startIndex = page * size;
            List<Book> books = context.Books.Skip(startIndex).Take(size).ToList();
            List<BookDTO> bookDTOs = mapper.Map<List<BookDTO>>(books);
            int totalElements = context.Books.Count();
            int totalPages = (int)Math.Ceiling((double)totalElements / size);
            BooksResponse booksResponse = new BooksResponse()
            {
                Books = bookDTOs,
                Page = new Page()
                {
                    TotalElements = totalElements,
                    TotalPages = totalPages,
                    Size = size
                }
            };
            return booksResponse;
        }

        public BookDTO getById(int id)
        {
            Book book = context.Books.FirstOrDefault(x => x.Id == id);
            return mapper.Map<BookDTO>(book);
        }

        public BooksResponse FindByCategory(string category, int page, int size)
        {
            var startIndex = page * size;
            List<Book> books = context.Books.Where(x => x.Category.ToLower().Equals(category.ToLower())).Skip(startIndex).Take(size).ToList();
            List<BookDTO> bookDTOs = mapper.Map<List<BookDTO>>(books);
            int totalElements = context.Books.Where(x => x.Category.ToLower().Equals(category.ToLower())).Count();
            int totalPages = (int)Math.Ceiling((double)totalElements / size);
            BooksResponse booksResponse = new BooksResponse()
            {
                Books = bookDTOs,
                Page = new Page()
                {
                    TotalElements = totalElements,
                    TotalPages = totalPages,
                    Size = size
                }
            };
            return booksResponse;
        }

        public void save(Book book)
        {
            Book bookUpdate = context.Books.FirstOrDefault(x => x.Id == book.Id);
            bookUpdate.Title = book.Title;
            bookUpdate.Author = book.Author;
            bookUpdate.Description = book.Description;
            bookUpdate.Copies = book.Copies;
            bookUpdate.CopiesAvailable = book.CopiesAvailable;
            bookUpdate.Category = book.Category;
            bookUpdate.Img = book.Img;
            context.SaveChanges();
        }

        public List<BookDTO> FindBooksByBookIds(List<long> bookIdList)
        {
            List<Book> books = new List<Book>();
            foreach (var bookId in bookIdList)
            {
                Book book = context.Books.FirstOrDefault(x => x.Id == bookId);
                if(book != null)
                {
                    books.Add(book);
                }
            }
            return mapper.Map<List<BookDTO>>(books);
        }

        public void AddBook(Book book)
        {
            context.Books.Add(book);            
            context.SaveChanges();
        }
    }
}
