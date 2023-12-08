using ReimbursementTrackerApp.Models.DTOs;

namespace ReimbursementTrackerApp.Interfaces
{
    /// <summary>
    /// Interface for the Token Service, providing a method for generating authentication tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates an authentication token for the specified user.
        /// </summary>
        /// <param name="user">The UserDTO for which to generate the token.</param>
        /// <returns>A string representing the generated authentication token.</returns>
        string GetToken(UserDTO user);
    }
}