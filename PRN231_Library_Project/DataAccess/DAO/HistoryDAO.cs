using AutoMapper;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.DataAccess.DAO
{
    public class HistoryDAO
    {
        private PRN231_Library_ProjectContext context;

        public HistoryDAO(PRN231_Library_ProjectContext context)
        {
            this.context = context;
        }

        public HistoriesResponse FindByUserEmail(string userEmail, int page, int size)
        {
            var startIndex = page * size;
            List<History> history = context.Histories.Where(x => x.UserEmail.ToLower().Equals(userEmail.ToLower())).Skip(startIndex).Take(size).ToList();
            int totalElements = context.Histories.Where(x => x.UserEmail.ToLower().Equals(userEmail.ToLower())).Count();
            int totalPages = (int)Math.Ceiling((double)totalElements / size);
            HistoriesResponse historiesResponse = new HistoriesResponse()
            {
                Histories = history,
                Page = new Page()
                {
                    TotalElements = totalElements,
                    TotalPages = totalPages,
                    Size = size
                }
            };
            return historiesResponse;
        }

        public void add(History history)
        {
            context.Histories.Add(history);
            context.SaveChanges();
        }
    }
}
