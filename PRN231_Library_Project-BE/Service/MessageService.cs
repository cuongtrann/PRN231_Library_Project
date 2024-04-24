using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.Service
{
    public class MessageService
    {
        private readonly IMessageRepository messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public void PutMessage(string adminEmail, AdminMessageRequest adminMessageRequest)
        {
            Message message = messageRepository.FindById(adminMessageRequest.Id);
            if (message == null)
            {
                throw new Exception(("Message not found"));
            }
            message.AdminEmail = adminEmail;
            message.Response = adminMessageRequest.Response;
            message.Closed = true;
            messageRepository.update(message);

        }

        public void PostMessage(Message message, string email)
        {
            Message messagePost = new Message(message.Title, message.Question);
            messagePost.UserEmail = email;
            messagePost.Closed = false;
            messageRepository.add(messagePost);
        }
    }
}
