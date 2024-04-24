using AutoMapper;
using PRN231_Library_Project.DataAccess.Models;

namespace PRN231_Library_Project.DataAccess.DAO
{
    public class CheckoutDAO
    {
        private readonly PRN231_Library_ProjectContext context;
        private readonly IMapper mapper;

        public CheckoutDAO(PRN231_Library_ProjectContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Checkout> FindByUserEmail(string email)
        {
            return context.Checkouts.Where(x => x.UserEmail.Equals(email)).ToList();
        }

        public Checkout FindByUserEmailAndBookId(string email, int bookId)
        {
            return context.Checkouts.FirstOrDefault(x => x.UserEmail.Equals(email) && x.BookId == bookId);
        }

        public void save(Checkout checkout)
        {
            context.Checkouts.Add(checkout);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            Checkout checkout = context.Checkouts.FirstOrDefault(x => x.Id == id);
            context.Checkouts.Remove(checkout);
            context.SaveChanges();
        }

        public void update(Checkout checkout)
        {
            Checkout checkoutUpdated = context.Checkouts.FirstOrDefault(x => x.Id == checkout.Id);
            checkoutUpdated.UserEmail = checkout.UserEmail;
            checkoutUpdated.CheckoutDate = checkout.CheckoutDate;
            checkoutUpdated.ReturnDate = checkout.ReturnDate;
            checkoutUpdated.BookId = checkout.BookId;
            context.SaveChanges();
        }
    }
}
