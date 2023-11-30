namespace ReimbursementTrackerApp.Models.DTOs
{
    public class PaymentDetailsDTO
    {
        public int RequestId { get; set; }
        public int PaymentId { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public float PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }


    }
}
