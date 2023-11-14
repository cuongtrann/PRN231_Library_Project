using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.Models;
using PRN231_Library_Project.Service;
using PRN231_Library_Project.Utils;
using System.Net;

namespace PRN231_Library_Project.Controllers
{
    [ApiController]
    [Route("/api/books")]
    public class BooksController : ControllerBase
    {
        private IBookRepository bookRepository;
        private BookService bookService;

        public BooksController(ILogger<BooksController> logger, IBookRepository bookRepository, BookService bookService)
        {
            this.bookRepository = bookRepository;
            this.bookService = bookService;
        }

        [HttpGet]
        public ActionResult<BooksResponse> Book([FromQuery] int page, [FromQuery] int size)
        {
            return bookRepository.GetAll(page, size);
        }

        [HttpGet("{id}")]
        public ActionResult<BookDTO> GetBookById(int id)
        {
            var book = bookRepository.GetById(id);
            if(book == null)
            {
                return NotFound();
            }
            return book;
        }


        [HttpGet("search/findByTitleContaining")]
        public ActionResult<BooksResponse> FindByTitleContaining([FromQuery] string title,[FromQuery] int page, [FromQuery] int size)
        {
            return bookRepository.FindByTitleContaining(title, page, size);
        }

        [HttpGet("search/findByCategory")]
        public ActionResult<BooksResponse> FindByCategory([FromQuery] string category, [FromQuery] int page, [FromQuery] int size)
        {
            return bookRepository.FindByCategory(category, page, size);
        }

        [HttpGet("secure/currentLoans/count")]
        [Authorize]
        public ActionResult<int> LoansCount([FromHeader] string authorization)
        {   
            string email = ExtractJWT.PayloadJWTExtraction(authorization, "\"sub\"");
            if (email == null)
            {
                return BadRequest("User email is missing");
            }
            return bookService.currentLoansCount(email);
        }

        [HttpGet("secure/ischeckedout/byuser")]
        [Authorize]
        public ActionResult<bool> IsCheckoutByUser([FromHeader] string authorization, [FromQuery] int bookId)
        {
            string email = ExtractJWT.PayloadJWTExtraction(authorization, "\"sub\"");
            if (email == null)
            {
                return BadRequest("User email is missing");
            }
            return bookService.checkoutBookByUser(email, bookId);
        }

        [HttpPut("secure/checkout/")]
        [Authorize]
        public ActionResult<BookDTO> Checkout([FromHeader] string authorization, [FromQuery] int bookId)
        {
            try
            {
                string email = ExtractJWT.PayloadJWTExtraction(authorization, "\"sub\"");
                if (email == null)
                {
                    return BadRequest("User email is missing");
                }
                return bookService.checkoutBook(email, bookId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet("secure/currentLoans")]
        [Authorize]
        public ActionResult<List<ShelfCurrentLoansResponse>> CurrentLoans([FromHeader] string authorization)
        {
            string email = ExtractJWT.PayloadJWTExtraction(authorization, "\"sub\"");
            if (email == null)
            {
                return BadRequest("User email is missing");
            }
            return bookService.GetShelfCurrentLoans(email);
        }

        [HttpPut("secure/return/")]
        [Authorize]
        public IActionResult ReturnBook([FromHeader] string authorization, [FromQuery] int bookId)
        {
            try
            {
                string email = ExtractJWT.PayloadJWTExtraction(authorization, "\"sub\"");
                if (email == null)
                {
                    return BadRequest("User email is missing");
                }
                bookService.returnBook(email, bookId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("secure/renew/loan/")]
        [Authorize]
        public IActionResult RenewBook([FromHeader] string authorization, [FromQuery] int bookId)
        {
            string email = ExtractJWT.PayloadJWTExtraction(authorization, "\"sub\"");
            if (email == null)
            {
                return BadRequest("User email is missing");
            }
            bookService.renewBook(email, bookId);
            return NoContent();
        }

    }
}