using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.Service
{
    public class ReviewService
    {
        private IReviewRepository reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public bool userReviewListed(string email, int bookId)
        {
            Review validateReview = reviewRepository.findByUserEmailAndBookId(email, bookId);
            return validateReview != null;
        }

        public void PostReview(string email, ReviewRequest reviewRequest)
        {
            Review validateReview = reviewRepository.findByUserEmailAndBookId(email, reviewRequest.BookId);
            if (validateReview != null)
            {
                throw new Exception("Review already created");
            }

            Review review = new Review();
            review.BookId = reviewRequest.BookId;
            review.Rating = (int?)reviewRequest.Rating;
            review.UserEmail = email;
            if (reviewRequest.reviewDescription != null)
            {
                review.ReviewDescription = reviewRequest.reviewDescription.ToString();
            }
            review.Date = DateTime.Now;
            reviewRepository.save(review);
        }
    }
}
