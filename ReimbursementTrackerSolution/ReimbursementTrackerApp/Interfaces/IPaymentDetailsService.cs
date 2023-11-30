using ReimbursementTrackerApp.Models.DTOs;

namespace ReimbursementTrackerApp.Interfaces
{
    public interface IPaymentDetailsService
    {
        public bool Add(PaymentDetailsDTO paymentDetailsDTO);
        public bool Remove(int paymentId);
        public PaymentDetailsDTO Update(PaymentDetailsDTO paymentDetailsDTO);
        public PaymentDetailsDTO GetPaymentDetailsById(int paymentId);
        public IEnumerable<PaymentDetailsDTO> GetAllPaymentDetails();
    }
}
