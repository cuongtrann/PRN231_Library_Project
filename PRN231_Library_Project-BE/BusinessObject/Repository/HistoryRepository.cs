using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.DAO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private HistoryDAO historyDAO;

        public HistoryRepository(HistoryDAO historyDAO)
        {
            this.historyDAO = historyDAO;
        }

        public HistoriesResponse FindByUserEmail(string userEmail, int page, int size)
        {
            return historyDAO.FindByUserEmail(userEmail, page, size);
        }

        public void save(History history)
        {
            historyDAO.add(history);
        }
    }
}
