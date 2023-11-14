using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.Models;
using System.Globalization;
using System.Runtime.InteropServices;

namespace PRN231_Library_Project.Service
{
    public class BookService
    {
        private ICheckoutRepository checkoutRepository;
        private IBookRepository bookRepository;
        private IHistoryRepository historyRepository;
        private IPaymentRepository paymentRepository;


        private IMapper mapper;

        public BookService(ICheckoutRepository checkoutRepository, IBookRepository bookRepository, IHistoryRepository historyRepository, IPaymentRepository paymentRepository, IMapper mapper)
        {
            this.checkoutRepository = checkoutRepository;
            this.bookRepository = bookRepository;
            this.historyRepository = historyRepository;
            this.paymentRepository = paymentRepository;
            this.mapper = mapper;
        }




        // currentLoansCount
        public int currentLoansCount(string userEmail)
        {
            return checkoutRepository.FindByUserEmail(userEmail).Count;
        }

        public bool checkoutBookByUser(string email, int bookId)
        {
            var checkoutValidate = checkoutRepository.FindByUserEmailAndBookId(email, bookId);
            return checkoutValidate != null;
        }

        public BookDTO checkoutBook(string email, int bookId)
        {
            Book book = bookRepository.findById(bookId);
            var validateCheckout = checkoutRepository.FindByUserEmailAndBookId(email, bookId);

            if (book == null || validateCheckout != null || book.CopiesAvailable <= 0)
            {
                throw new Exception("Book doesn't exist or already checked out by user");
            }

            var currentBooksCheckedOut = checkoutRepository.FindByUserEmail(email);
            var currentDate = DateTime.Now.Date;
            bool bookNeedsReturned = false;

            foreach (var checkoutItem in currentBooksCheckedOut)
            {
                var returnDate = DateTime.Parse(checkoutItem.ReturnDate).Date;
                var differenceInDays = (returnDate - currentDate).TotalDays;

                if (differenceInDays < 0)
                {
                    bookNeedsReturned = true;
                    break;
                }
            }

            var userPayment = paymentRepository.FindByUserEmail(email);

            if ((userPayment != null && userPayment.Amount > 0) || (userPayment != null && bookNeedsReturned))
            {
                throw new Exception("Outstanding fees");
            }

            if (userPayment == null)
            {
                var payment = new Payment
                {
                    Amount = 0,
                    UserEmail = email
                };
                paymentRepository.Add(payment);
            }


            book.CopiesAvailable--;
            bookRepository.save(book);

            var checkout = new Checkout(
                email,
                DateTime.Now.ToString(),
                DateTime.Now.AddDays(7).ToString(),
                book.Id
            );
            checkoutRepository.save(checkout);

            return mapper.Map<BookDTO>(book);
        }

        public List<ShelfCurrentLoansResponse> GetShelfCurrentLoans(string userEmail)
        {
            var shelfCurrentLoansResponse = new List<ShelfCurrentLoansResponse>();

            var checkoutList = checkoutRepository.FindByUserEmail(userEmail);
            var bookIdList = new List<long>();

            foreach (var checkout in checkoutList)
            {
                bookIdList.Add(checkout.BookId);
            }

            var books = bookRepository.FindBooksByBookIds(bookIdList);


            foreach (var book in books)
            {
                var checkoutOptional = checkoutList.Where(x => x.BookId == book.Id).FirstOrDefault();

                if (checkoutOptional != null)
                {
                    var checkout = checkoutOptional;
                    var returnDate = DateTime.Parse(checkout.ReturnDate);
                    var currentDate = DateTime.Now;

                    var daysOverdue = (returnDate - currentDate).Days;

                    shelfCurrentLoansResponse.Add(new ShelfCurrentLoansResponse(book, daysOverdue));
                }
            }

            return shelfCurrentLoansResponse;
        }

        public void returnBook(string email, int bookId)
        {
            Book book = bookRepository.findById(bookId);

            Checkout validateCheckout = checkoutRepository.FindByUserEmailAndBookId(email, bookId);
            if (book == null || validateCheckout == null)
            {
                throw new Exception("Book does not exist or not checked out by user");
            }
            book.CopiesAvailable++;

            bookRepository.save(book);

            var currentDate = DateTime.Now.Date;
            var returnDate = DateTime.Parse(validateCheckout.ReturnDate).Date;
            var differenceInDays = (returnDate - currentDate).TotalDays;

            if (differenceInDays < 0)
            {
                var payment = paymentRepository.FindByUserEmail(email);
                payment.Amount += Convert.ToDecimal(Math.Abs(differenceInDays));
                paymentRepository.Save(payment);
            }

            checkoutRepository.DeleteById(validateCheckout.Id);

            History history = new History(email,
                    validateCheckout.CheckoutDate,
                    DateTime.Now.ToString(),
                    book.Title,
                    book.Author,
                    book.Description,
                    book.Img
                    );
            historyRepository.save(history);
        }

        public void renewBook(string email, int bookId)
        {
            Checkout validateCheckout = checkoutRepository.FindByUserEmailAndBookId(email, bookId);

            if (validateCheckout == null)
            {
                throw new Exception("Book does not exist or not checked out by user");
            }

            Date d1 = DateTime.Parse(validateCheckout.ReturnDate);
            Date d2 = DateTime.Now;

            if (d1.CompareTo(d2) > 0 || d1.CompareTo(d2) == 0)
            {
                validateCheckout.ReturnDate = (DateTime.Now.AddDays(7).ToString());
                checkoutRepository.update(validateCheckout);
            }
        }
    }
}
