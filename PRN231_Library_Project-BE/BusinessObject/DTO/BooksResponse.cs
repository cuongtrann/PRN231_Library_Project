using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.DTO
{
    public class BooksResponse
    {
        public BooksResponse()
        {
            Books = new List<BookDTO>();
        }

        public List<BookDTO> Books { get; set; }
        public Page Page { get; set; }
    }
}
