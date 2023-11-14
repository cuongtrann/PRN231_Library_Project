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
    [Route("/api/reviews")]
    public class ReviewsController:ControllerBase
    {
        private readonly IReviewRepository reviewRepository;
        private ReviewService reviewService;

        public ReviewsController(IReviewRepository reviewRepository, ReviewService reviewService)
        {
            this.reviewRepository = reviewRepository;
            this.reviewService = reviewService; 
        }

        [HttpGet("search/findByBookId")]
        public ActionResult<ReviewsResponse> GetByBookId([FromQuery] int bookId, [FromQuery] int? page, [FromQuery] int? size)
        {
            return reviewRepository.GetReviewsByBookId(bookId, page, size);
        }

        [HttpPost("secure")]
        [Authorize]
        public IActionResult PostReview([FromHeader] string authorization, [FromBody] ReviewRequest reviewRequest)
        {
            string email = ExtractJWT.PayloadJWTExtraction(authorization, "\"sub\"");
            if (email == null)
            {
                return BadRequest("User email is missing");
            }
            reviewService.PostReview(email, reviewRequest);
            return NoContent();
        }

        [HttpGet("secure/user/book")]
        [Authorize]
        public ActionResult<bool> ReviewBookByUser([FromHeader] string authorization, [FromQuery] int bookId)
        {
            string email = ExtractJWT.PayloadJWTExtraction(authorization, "\"sub\"");
            if (email == null)
            {
                return BadRequest("User email is missing");
            }
            return reviewService.userReviewListed(email, bookId);
        }

        


    }
}
