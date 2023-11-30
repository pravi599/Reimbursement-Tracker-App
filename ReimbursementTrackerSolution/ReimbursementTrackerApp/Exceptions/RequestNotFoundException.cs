using System.Runtime.Serialization;

namespace ReimbursementTrackerApp.Exceptions
{
    [Serializable]
    public class RequestNotFoundException : Exception
    {
        string message;
        public RequestNotFoundException()
        {
            message = "Request with ID {requestId} not found";
        }
        public override string Message => message;
    }
}