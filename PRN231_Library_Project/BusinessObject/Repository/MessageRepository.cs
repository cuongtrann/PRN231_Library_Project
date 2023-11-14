using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.DAO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private MessageDAO messageDAO;

        public MessageRepository(MessageDAO messageDAO)
        {
            this.messageDAO = messageDAO;
        }

        public void add(Message messagePost)
        {
            messageDAO.add(messagePost);
        }

        public MessagesResponse FindByClosed(bool closed, int page, int size)
        {
            return messageDAO.FindByClosed(closed, page, size);
        }

        public Message FindById(int id)
        {
            return messageDAO.FindById(id);
        }

        public MessagesResponse FindByUserEmail(string userEmail, int page, int size)
        {
            return messageDAO.FindByUserEmail(userEmail, page, size);
        }

        public void update(Message message)
        {
            messageDAO.update(message);
        }
    }
}
