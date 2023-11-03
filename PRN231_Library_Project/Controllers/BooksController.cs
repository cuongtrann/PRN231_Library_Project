using Microsoft.AspNetCore.Mvc;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.Controllers
{
    [ApiController]
    [Route("/api/books")]
    public class BooksController : ControllerBase
    {
        private IBookRepository bookRepository;

        public BooksController(ILogger<BooksController> logger, IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet]
        public IEnumerable<DataAccess.Models.Book> Book([FromQuery] int page, [FromQuery] int size)
        {
            return bookRepository.GetAll(page, size);
        }
    }
}