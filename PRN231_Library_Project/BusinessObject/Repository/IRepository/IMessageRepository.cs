using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository.IRepository
{
    public interface IMessageRepository
    {
        void add(Message messagePost);
        MessagesResponse FindByClosed(bool closed, int page, int size);
        Message FindById(int id);
        MessagesResponse FindByUserEmail(string userEmail, int page, int size);
        void update(Message message);
    }
}
