using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.DataAccess.DAO
{
    public class PaymentDAO
    {
        private PRN231_Library_ProjectContext context;

        public PaymentDAO(PRN231_Library_ProjectContext context)
        {
            this.context = context;
        }

        public void Update(Payment payment)
        {
            Payment paymentUpdate = context.Payments.FirstOrDefault(x => x.Id == payment.Id);
            paymentUpdate.Amount = payment.Amount;
            context.SaveChanges();
        }

        public void Add(Payment payment)
        {
            context.Payments.Add(payment);
            context.SaveChanges();
        }

        public Payment FindByUserEmail(string email)
        {
            return context.Payments.FirstOrDefault(x => x.UserEmail.Equals(email));
        }
    }
}
