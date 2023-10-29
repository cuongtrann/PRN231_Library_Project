using System;
using System.Collections.Generic;

namespace PRN231_Library_Project.DataAccess.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public DateTime? Date { get; set; }
        public int? Rating { get; set; }
        public int BookId { get; set; }
        public string? ReviewDescription { get; set; }

        public virtual Book Book { get; set; } = null!;
    }
}
