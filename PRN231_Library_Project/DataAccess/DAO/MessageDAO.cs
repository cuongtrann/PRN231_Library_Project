using AutoMapper;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.DataAccess.DAO
{
    public class MessageDAO
    {
        private readonly PRN231_Library_ProjectContext context;

        public MessageDAO(PRN231_Library_ProjectContext context)
        {
            this.context = context;
        }

        public void add(Message messagePost)
        {
            context.Messages.Add(messagePost);
            context.SaveChanges();
        }

        public MessagesResponse FindByUserEmail(string userEmail, int page, int size)
        {
            var startIndex = page * size;
            List<Message> messages = context.Messages.Where(x => x.UserEmail.ToLower().Contains(userEmail.ToLower())).Skip(startIndex).Take(size).ToList();
            int totalElements = context.Messages.Where(x => x.UserEmail.ToLower().Contains(userEmail.ToLower())).Count();
            int totalPages = (int)Math.Ceiling((double)totalElements / size);
            MessagesResponse messagesResponse = new MessagesResponse()
            {
                Messages = messages,
                Page = new Page()
                {
                    TotalElements = totalElements,
                    TotalPages = totalPages,
                    Size = size
                }
            };
            return messagesResponse;
        }

        internal MessagesResponse FindByClosed(bool closed, int page, int size)
        {
            var startIndex = page * size;
            List<Message> messages = context.Messages.Where(x => x.Closed == closed).Skip(startIndex).Take(size).ToList();
            int totalElements = context.Messages.Where(x => x.Closed == closed).Count();
            int totalPages = (int)Math.Ceiling((double)totalElements / size);
            MessagesResponse messagesResponse = new MessagesResponse()
            {
                Messages = messages,
                Page = new Page()
                {
                    TotalElements = totalElements,
                    TotalPages = totalPages,
                    Size = size
                }
            };
            return messagesResponse;
        }

        public Message FindById(int id)
        {
            return context.Messages.FirstOrDefault(x => x.Id == id);
        }

        public void update(Message message)
        {
            Message messageUpdate = context.Messages.FirstOrDefault(x => x.Id == message.Id);
            if (messageUpdate == null)
            {
                throw new Exception("Not found message");
            }
            messageUpdate.AdminEmail = message.AdminEmail;
            messageUpdate.Closed = message.Closed;
            messageUpdate.Response = message.Response;
            context.SaveChanges();
        }
    }
}
