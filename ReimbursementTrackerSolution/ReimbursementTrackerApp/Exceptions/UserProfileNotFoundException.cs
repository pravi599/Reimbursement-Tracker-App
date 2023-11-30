using System.Runtime.Serialization;

namespace ReimbursementTrackerApp.Exceptions
{
    [Serializable]
    public class UserProfileNotFoundException : Exception
    {
        string message;
        public UserProfileNotFoundException()
        {
            message = "User profile for this username not found";
        }
        public override string Message => message;

    }
}