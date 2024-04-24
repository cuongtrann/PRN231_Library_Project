using Microsoft.AspNetCore.Mvc;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.DTO
{
    public class HistoriesResponse
    {
        public List<History> Histories { get; set; }
        public Page Page { get; set; }
    }
}
