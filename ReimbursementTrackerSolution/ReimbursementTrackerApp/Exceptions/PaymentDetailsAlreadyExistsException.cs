// PaymentDetailsService.cs
using System.Drawing;
using System.Runtime.Serialization;

namespace ReimbursementTrackerApp.Exceptions
{
    [Serializable]
    public class PaymentDetailsAlreadyExistsException : Exception
    {
        string message;
        public PaymentDetailsAlreadyExistsException()
        {
            message = "PaymentDetails with ID {paymentId} already exists";
        }
        public override string Message => message;
    }
}