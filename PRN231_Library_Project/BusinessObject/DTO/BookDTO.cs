namespace PRN231_Library_Project.BusinessObject.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public int? Copies { get; set; }
        public int? CopiesAvailable { get; set; }
        public string Category { get; set; } = null!;
        public string? Img { get; set; }
        public string? BookContent { get; set; }
    }
}
