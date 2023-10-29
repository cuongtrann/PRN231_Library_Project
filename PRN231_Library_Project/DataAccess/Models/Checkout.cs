using System;
using System.Collections.Generic;

namespace PRN231_Library_Project.DataAccess.Models
{
    public partial class Checkout
    {
        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public string? CheckoutDate { get; set; }
        public string? ReturnDate { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; } = null!;
    }
}
