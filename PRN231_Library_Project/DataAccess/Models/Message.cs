using System;
using System.Collections.Generic;

namespace PRN231_Library_Project.DataAccess.Models
{
    public partial class Message
    {
        public Message(string? title, string? question)
        {
            Title = title;
            Question = question;
        }

        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public string? Title { get; set; }
        public string? Question { get; set; }
        public string? AdminEmail { get; set; }
        public string? Response { get; set; }
        public bool? Closed { get; set; }
    }
}
