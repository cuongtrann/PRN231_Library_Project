using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.Models;
using PRN231_Library_Project.Service;
using PRN231_Library_Project.Utils;

namespace PRN231_Library_Project.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private MessageService messageService;
        private IMessageRepository messageRepository;
        public MessagesController(MessageService messageService, IMessageRepository messageRepository)
        {
            this.messageService = messageService;
            this.messageRepository = messageRepository;
        }

        [HttpPost("secure/add/message")]
        [Authorize]
        public IActionResult PostMessage([FromHeader] string authorization, [FromBody] Message message)
        {
            string email = ExtractJWT.PayloadJWTExtraction(authorization, "\"sub\"");
            if (email == null)
            {
                return BadRequest("User email is missing");
            }
            messageService.PostMessage(message, email);
            return NoContent();
        }

        [HttpGet("search/findByUserEmail")]
        public ActionResult<MessagesResponse> FindByUserEmail([FromQuery] string userEmail, [FromQuery] int page, [FromQuery] int size)
        {
            return messageRepository.FindByUserEmail(userEmail, page, size);
        }

        [HttpGet("search/findByClosed")]
        public ActionResult<MessagesResponse> FindByClosed([FromQuery] bool closed, [FromQuery] int page, [FromQuery] int size)
        {
            return messageRepository.FindByClosed(closed, page, size);
        }

        [HttpPut("secure/admin/message")]
        [Authorize]
        public IActionResult PutMessage([FromHeader] string authorization, AdminMessageRequest adminMessageRequest)
        {
            string adminEmail = ExtractJWT.PayloadJWTExtraction(authorization, "\"sub\"");
            string isAdmin = ExtractJWT.PayloadJWTExtraction(authorization, "\"userType\"");
            if(isAdmin == null || !isAdmin.Equals("admin"))
            {
                return Unauthorized("Just for admin!");
            }
            messageService.PutMessage(adminEmail, adminMessageRequest);
            return NoContent();
        }
    }
}
