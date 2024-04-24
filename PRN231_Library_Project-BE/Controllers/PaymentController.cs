using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231_Library_Project.BusinessObject.DTO;
using PRN231_Library_Project.BusinessObject.Repository.IRepository;
using PRN231_Library_Project.DataAccess.Models;
using PRN231_Library_Project.Service;
using PRN231_Library_Project.Utils;
using Stripe;

namespace PRN231_Library_Project.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private PaymentService paymentService;
        private IPaymentRepository paymentRepository;

        public PaymentController(PaymentService paymentService, IPaymentRepository paymentRepository)
        {
            this.paymentService = paymentService;
            this.paymentRepository = paymentRepository;
        }
        [HttpGet("search/findByUserEmail")]
        public ActionResult<Payment> FindByUserEmail([FromQuery] string userEmail)
        {
            return paymentRepository.FindByUserEmail(userEmail);
        }

        [HttpPost("secure/payment-intent")]
        public IActionResult CreatePaymentIntent([FromBody] PaymentInfoRequest paymentInfoRequest)
        {
            try
            {
                PaymentIntent paymentIntent = paymentService.CreatePaymentIntent(paymentInfoRequest);
                string paymentStr = paymentIntent.ToJson();
                return Ok(paymentStr);
            }
            catch (StripeException ex)
            {
                return StatusCode((int)ex.HttpStatusCode, ex.Message);
            }
        }

        [HttpPut("secure/payment-complete")]
        public IActionResult StripePaymentComplete([FromHeader] string authorization)
        {
            try
            {
                string email = ExtractJWT.PayloadJWTExtraction(authorization, "\"sub\"");
                if (email == null)
                {
                    return BadRequest("User email is missing");
                }
                paymentService.StripePayment(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
