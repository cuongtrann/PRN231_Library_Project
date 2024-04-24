using System;
using System.Collections.Generic;

namespace PRN231_Library_Project.DataAccess.Models
{
    public partial class History
    {
        public History(string userEmail, string checkoutDate, string returnedDate, string title, string author, string description, string img)
        {
            UserEmail = userEmail;
            CheckoutDate = checkoutDate;
            ReturnedDate = returnedDate;
            Title = title;
            Author = author;
            Description = description;
            Img = img;
        }

        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public string? CheckoutDate { get; set; }
        public string? ReturnedDate { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? Img { get; set; }
    }
}
