using System;
using System.Collections.Generic;

namespace PRN231_Library_Project.DataAccess.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public decimal? Amount { get; set; }
    }
}
