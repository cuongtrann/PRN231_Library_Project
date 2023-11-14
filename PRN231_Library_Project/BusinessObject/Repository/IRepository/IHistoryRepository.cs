using Microsoft.AspNetCore.Mvc;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository.IRepository
{
    public interface IHistoryRepository
    {
        HistoriesResponse FindByUserEmail(string userEmail, int page, int size);
        void save(History history);
    }
}
