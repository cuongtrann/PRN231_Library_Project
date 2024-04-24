namespace PRN231_Library_Project.BusinessObject.DTO
{
    public class PaymentInfoRequest
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string ReceiptEmail { get; set; }
    }
}
