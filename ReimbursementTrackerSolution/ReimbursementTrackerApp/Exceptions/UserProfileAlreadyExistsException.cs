using System.Runtime.Serialization;

namespace ReimbursementTrackerApp.Exceptions
{
    [Serializable]
    internal class UserProfileAlreadyExistsException : Exception
    {
        string message;
        public UserProfileAlreadyExistsException()
        {
            message = "User profile with this username already exists";
        }
        public override string Message => message;
    }
}