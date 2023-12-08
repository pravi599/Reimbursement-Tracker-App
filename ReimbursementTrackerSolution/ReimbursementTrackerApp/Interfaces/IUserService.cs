using ReimbursementTrackerApp.Models.DTOs;

namespace ReimbursementTrackerApp.Interfaces
{
    /// <summary>
    /// Interface for the User Service, providing methods for user authentication and registration.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Validates user credentials and performs user login.
        /// </summary>
        /// <param name="userDTO">The UserDTO containing user login information.</param>
        /// <returns>A UserDTO representing the logged-in user.</returns>
        UserDTO Login(UserDTO userDTO);

        /// <summary>
        /// Registers a new user with the provided information.
        /// </summary>
        /// <param name="userDTO">The UserDTO containing user registration information.</param>
        /// <returns>A UserDTO representing the newly registered user.</returns>
        UserDTO Register(UserDTO userDTO);
    }
}
