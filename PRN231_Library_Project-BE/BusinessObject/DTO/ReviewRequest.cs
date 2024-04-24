namespace PRN231_Library_Project.BusinessObject.DTO
{
    public class ReviewRequest
    {
        public double Rating { get; set; }

        public int BookId { get; set; }

        public string? reviewDescription { get; set; }
    }
}
