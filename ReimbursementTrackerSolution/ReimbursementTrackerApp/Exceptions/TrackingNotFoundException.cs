using System.Runtime.Serialization;

namespace ReimbursementTrackerApp.Exceptions
{
    [Serializable]
    public class TrackingNotFoundException : Exception
    {
        string message;
        public TrackingNotFoundException()
        {
            message = "Tracking information not found for Request with ID {requestId}.";
        }
        public override string Message => message;
    }
}