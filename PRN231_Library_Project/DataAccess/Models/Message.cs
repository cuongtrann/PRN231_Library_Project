using System;
using System.Collections.Generic;

namespace PRN231_Library_Project.DataAccess.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public string? Tittle { get; set; }
        public string? Question { get; set; }
        public string? AdminEmail { get; set; }
        public string? Response { get; set; }
        public bool? Closed { get; set; }
    }
}
