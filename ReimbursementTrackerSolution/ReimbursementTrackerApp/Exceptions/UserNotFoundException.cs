using System.Runtime.Serialization;

namespace ReimbursementTrackerApp.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        string message;
        public UserNotFoundException()
        {
            message = "User not found while adding a request";
        }
        public override string Message => message;

    }
}