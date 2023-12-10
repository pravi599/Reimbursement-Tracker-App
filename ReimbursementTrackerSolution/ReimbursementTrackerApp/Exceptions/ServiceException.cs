using System.Runtime.Serialization;

namespace ReimbursementTrackerApp.Exceptions
{
    [Serializable]
    public class ServiceException : Exception
    {
        string message;
        public ServiceException()
        {
            message = "Error occurred while performing operations on tracking";
        }
        public override string Message => message;

    }
}