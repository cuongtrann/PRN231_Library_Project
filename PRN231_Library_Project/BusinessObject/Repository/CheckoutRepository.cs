using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.DAO;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly CheckoutDAO checkoutDAO;

        public CheckoutRepository(CheckoutDAO checkoutDAO)
        {
            this.checkoutDAO = checkoutDAO;
        }

        public void DeleteById(int id)
        {
            checkoutDAO.DeleteById(id);
        }

        public List<Checkout> FindByUserEmail(string email)
        {
            return checkoutDAO.FindByUserEmail(email);
        }

        public Checkout FindByUserEmailAndBookId(string email, int bookId)
        {
            return checkoutDAO.FindByUserEmailAndBookId(email, bookId);
        }

        public void save(Checkout checkout)
        {
            checkoutDAO.save(checkout);
        }

        public void update(Checkout validateCheckout)
        {
            checkoutDAO.update(validateCheckout);
        }
    }
}
