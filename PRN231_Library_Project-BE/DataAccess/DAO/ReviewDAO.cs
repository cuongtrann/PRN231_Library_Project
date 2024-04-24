using AutoMapper;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.DataAccess.Models;
using System.Net;

namespace PRN231_Library_Project.DataAccess.DAO
{
    public class ReviewDAO
    {
        private readonly PRN231_Library_ProjectContext context;
        private readonly IMapper mapper;

        public ReviewDAO(PRN231_Library_ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ReviewsResponse GetReviewsByBookId(int bookId, int? page, int? size)
        {
            if (page == null) page = 0;
            if (size == null) size = 2;
            int startIndex = (int)(page * size);
            List<Review> reviewsByBookId = context.Reviews.Where(x => x.BookId == bookId).Skip(startIndex).Take((int)size).ToList();
            List<ReviewDTO> reviewDTOs = mapper.Map<List<ReviewDTO>>(reviewsByBookId);
            int totalElements = context.Reviews.Where(x => x.BookId == bookId).Skip(startIndex).Take((int)size).Count();
            int totalPages = 0;
            totalPages = (int)Math.Ceiling((double)totalElements / (int)size);
            ReviewsResponse reviewsResponse = new ReviewsResponse()
            {
                Reviews = reviewDTOs,
                Page = new Page()
                {
                    TotalElements = totalElements,
                    TotalPages = totalPages,
                    Size = (int)size
                }
            };
            return reviewsResponse;
        }

        public Review findByUserEmailAndBookId(string userEmail, int bookID)
        {
            return context.Reviews.FirstOrDefault(x => x.UserEmail.Equals(userEmail) && x.BookId == bookID);
        }

        public void AddReview(Review review)
        {
            context.Reviews.Add(review);
            context.SaveChanges();
        }
    }
}
