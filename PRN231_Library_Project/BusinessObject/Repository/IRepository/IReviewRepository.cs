using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository.IRepository
{
    public interface IReviewRepository
    {
        Review findByUserEmailAndBookId(string userEmail, int bookId);
        ReviewsResponse GetReviewsByBookId(int bookId, int? page, int? size);
        void save(Review review);
    }
}
