using System;
using System.Collections.Generic;

namespace PRN231_Library_Project.DataAccess.Models
{
    public partial class Book
    {
        public Book()
        {
            Checkouts = new HashSet<Checkout>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string? Tittle { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public int? Copies { get; set; }
        public int? CopiesAvailable { get; set; }
        public int CategoryId { get; set; }
        public byte[]? Img { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Checkout> Checkouts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
