using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.DTO
{
    public class MessagesResponse
    {
        public List<Message> Messages { get; set; }
        public Page Page { get; set; }

        public MessagesResponse()
        {
            Messages = new List<Message>();
        }
    }
}
