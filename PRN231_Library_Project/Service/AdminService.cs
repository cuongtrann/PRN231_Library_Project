using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;

namespace PRN231_Library_Project.Service
{
    public class AdminService
    {
        private IBookRepository bookRepository;
        private ICheckoutRepository checkoutRepository;
        private IReviewRepository reviewRepository;

        public AdminService(IBookRepository bookRepository, ICheckoutRepository checkoutRepository, IReviewRepository reviewRepository)
        {
            this.bookRepository = bookRepository;
            this.checkoutRepository = checkoutRepository;
            this.reviewRepository = reviewRepository;
        }

        public void PostBook(AddBookRequest addBookRequest)
        {
            bookRepository.AddBook(addBookRequest);
        }

        public void IncreaseBookQuantity(int bookId)
        {
            var book = bookRepository.findById(bookId);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            book.CopiesAvailable++;
            book.Copies++;

            bookRepository.save(book);
        }

        public void DecreaseBookQuantity(int bookId)
        {
            var book = bookRepository.findById(bookId);

            if (book == null || book.CopiesAvailable <= 0 || book.Copies <= 0)
            {
                throw new Exception("Book not found or quantity locked");
            }

            book.CopiesAvailable--;
            book.Copies--;

            bookRepository.save(book);
        }

        public void DeleteBook(int bookId)
        {
            var book = bookRepository.findById(bookId);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            bookRepository.Delete(bookId);
        }
    }
}
