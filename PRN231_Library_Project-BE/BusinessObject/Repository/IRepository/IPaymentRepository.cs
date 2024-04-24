using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository.IRepository
{
    public interface IPaymentRepository
    {
        Payment FindByUserEmail(string email);
        void Save(Payment payment);
        void Add(Payment payment);
    }
}
