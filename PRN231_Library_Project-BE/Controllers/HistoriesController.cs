using Microsoft.AspNetCore.Mvc;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;

namespace PRN231_Library_Project.Controllers
{
    [ApiController]
    [Route("/api/histories")]
    public class HistoriesController : ControllerBase
    {
        private readonly IHistoryRepository historyRepository;

        public HistoriesController(IHistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }

        [HttpGet("search/findByUserEmail/")]
        public ActionResult<HistoriesResponse> FindByUserEmail([FromQuery] string userEmail, [FromQuery] int page, [FromQuery] int size)
        {
            return historyRepository.FindByUserEmail(userEmail, page, size);
        }
    }
}
