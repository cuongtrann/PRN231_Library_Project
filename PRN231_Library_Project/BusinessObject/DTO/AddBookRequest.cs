using Newtonsoft.Json;

namespace PRN231_Library_Project.BusinessObject.DTO
{
    public class AddBookRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Copies { get; set; }
        public string Category { get; set; }
        public string Img { get; set; }
        [JsonIgnore]
        public string FilePath { get; set; }
    }
}
