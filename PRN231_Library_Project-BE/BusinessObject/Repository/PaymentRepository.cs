using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.DAO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private PaymentDAO paymentDAO;
        public PaymentRepository(PaymentDAO paymentDAO, PRN231_Library_ProjectContext context)
        {
            this.paymentDAO = paymentDAO;
        }

        public void Add(Payment payment)
        {
            paymentDAO.Add(payment);
        }

        public Payment FindByUserEmail(string email)
        {
            return paymentDAO.FindByUserEmail(email);
                
        }

        public void Save(Payment payment)
        {
            paymentDAO.Update(payment);
        }
    }
}
