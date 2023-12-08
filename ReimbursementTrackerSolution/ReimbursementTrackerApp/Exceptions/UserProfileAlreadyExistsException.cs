using System.Runtime.Serialization;

namespace ReimbursementTrackerApp.Exceptions
{
    [Serializable]
    public class UserProfileAlreadyExistsException : Exception
    {
        string message;
        public UserProfileAlreadyExistsException()
        {
            message = "User profile with this username already exists";
        }
        public override string Message => message;
    }
}