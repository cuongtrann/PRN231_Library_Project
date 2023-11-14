using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace PRN231_Library_Project.BusinessObject.DTO
{
    public class BookRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Copies { get; set; }
        public string Category { get; set; }
        public string Img { get; set; }
        public IFormFile File { get; set; }
        [JsonIgnore]
        public string FilePath { get; set; }
    }
}
