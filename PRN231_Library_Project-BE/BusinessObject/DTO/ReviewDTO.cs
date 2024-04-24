namespace PRN231_Library_Project.BusinessObject.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public DateTime? Date { get; set; }
        public int? Rating { get; set; }
        public int BookId { get; set; }
        public string? ReviewDescription { get; set; }
    }
}
