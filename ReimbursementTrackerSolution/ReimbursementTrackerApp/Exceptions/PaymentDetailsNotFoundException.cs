// PaymentDetailsService.cs
using System.Runtime.Serialization;

namespace ReimbursementTrackerApp.Exceptions
{
    [Serializable]
    public class PaymentDetailsNotFoundException : Exception
    {
        string message;
        public PaymentDetailsNotFoundException()
        {
            message = "PaymentDetails with ID {paymentId} not found while performing operations on payment details";
        }
        public override string Message => message;

    }
}