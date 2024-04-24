using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.DTO
{
    public class ShelfCurrentLoansResponse
    {
        public BookDTO Book { get; set; }

        public int DaysLeft { get; set; }

        public ShelfCurrentLoansResponse(BookDTO book, int daysLeft)
        {
            Book = book;
            DaysLeft = daysLeft;
        }
    }
}
