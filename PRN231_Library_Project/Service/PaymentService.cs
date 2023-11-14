using Newtonsoft.Json.Linq;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using Stripe;
using System.Net;

namespace PRN231_Library_Project.Service
{
    public class PaymentService
    {
        private IPaymentRepository paymentRepository;
        private readonly string _stripeApiKey;

        public PaymentService(IPaymentRepository paymentRepository, IConfiguration configuration)
        {
            this.paymentRepository = paymentRepository;
            _stripeApiKey = configuration["StripeApiKey"];
            StripeConfiguration.ApiKey = _stripeApiKey;
        }

        public PaymentIntent CreatePaymentIntent(PaymentInfoRequest paymentInfoRequest)
        {
            var paymentMethodTypes = new List<string> { "card" };

            var options = new PaymentIntentCreateOptions
            {
                Amount = paymentInfoRequest.Amount,
                Currency = paymentInfoRequest.Currency,
                PaymentMethodTypes = paymentMethodTypes
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

            return paymentIntent;
        }

        public HttpResponseMessage StripePayment(string userEmail)
        {
            var payment = paymentRepository.FindByUserEmail(userEmail);
            if (payment == null)
            {
                throw new Exception("Payment information is missing");
            }

            payment.Amount = (decimal?)00.00;
            paymentRepository.Save(payment);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
