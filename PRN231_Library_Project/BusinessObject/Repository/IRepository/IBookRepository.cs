using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository.IRepository
{
    public interface IBookRepository
    {
        BooksResponse GetAll(int page, int size);
        BookDTO GetById(int id);
        BooksResponse FindByTitleContaining(string title, int page, int size);
        BooksResponse FindByCategory(string category, int page, int size);
        Book findById(int bookId);
        void save(Book book);
        List<BookDTO> FindBooksByBookIds(List<long> bookIdList);
        void AddBook(AddBookRequest addBookRequest);
        void Delete(object book);
    }
}
