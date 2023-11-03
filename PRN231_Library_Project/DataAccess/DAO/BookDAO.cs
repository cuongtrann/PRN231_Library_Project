using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.DataAccess.DAO
{
    public class BookDAO
    {
        public List<Book> GetBooks(int page, int size)
        {
            using(PRN231_Library_ProjectContext context = new PRN231_Library_ProjectContext())
            {
                // Tính toán vị trí bắt đầu và kết thúc của danh sách sản phẩm
                var startIndex = page * size;
                return context.Books.Skip(startIndex).Take(size).ToList();
            }
        }
    }
}
