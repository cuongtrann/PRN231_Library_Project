using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.DTO
{
    public class ReviewsResponse
    {
        public ReviewsResponse()
        {
            Reviews = new List<ReviewDTO>();
        }

        public List<ReviewDTO> Reviews { get; set; }
        public Page Page { get; set; }
    }
}
