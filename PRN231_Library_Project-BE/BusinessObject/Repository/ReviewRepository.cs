using Microsoft.AspNetCore.Identity;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.DAO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private ReviewDAO reviewDAO;

        public ReviewRepository(ReviewDAO reviewDAO)
        {
            this.reviewDAO = reviewDAO;
        }

        public Review findByUserEmailAndBookId(string userEmail, int bookId)
        {
            return reviewDAO.findByUserEmailAndBookId(userEmail, bookId);
        }

        public ReviewsResponse GetReviewsByBookId(int bookId, int? page, int? size)
        {
            return reviewDAO.GetReviewsByBookId(bookId, page, size);
        }

        public void save(Review review)
        {
            reviewDAO.AddReview(review);
        }
    }
}
