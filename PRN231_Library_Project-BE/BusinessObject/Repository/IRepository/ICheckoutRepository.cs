using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.BusinessObject.Repository.IRepository
{
    public interface ICheckoutRepository
    {
        void DeleteById(int id);
        List<Checkout> FindByUserEmail(string email);
        Checkout FindByUserEmailAndBookId(string email, int bookId);
        void save(Checkout checkout);
        void update(Checkout validateCheckout);
    }
}
