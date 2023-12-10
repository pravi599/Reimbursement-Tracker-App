using ReimbursementTrackerApp.Models.DTOs;
using System.Collections.Generic;

namespace ReimbursementTrackerApp.Interfaces
{
    /// <summary>
    /// Interface for the User Profile Service, providing methods for managing user profiles.
    /// </summary>
    public interface IUserProfileService
    {
        /// <summary>
        /// Adds a new user profile.
        /// </summary>
        /// <param name="userProfileDTO">The UserProfileDTO containing user profile information.</param>
        /// <returns>True if the user profile is added successfully; otherwise, false.</returns>
        bool Add(UserProfileDTO userProfileDTO);

        /// <summary>
        /// Removes a user profile by username.
        /// </summary>
        /// <param name="username">The username of the user profile to be removed.</param>
        /// <returns>True if the user profile is removed successfully; otherwise, false.</returns>
        bool Remove(string username);

        /// <summary>
        /// Updates an existing user profile.
        /// </summary>
        /// <param name="userProfileDTO">The UserProfileDTO containing updated user profile information.</param>
        /// <returns>The updated UserProfileDTO if the update is successful; otherwise, null.</returns>
        UserProfileDTO Update(UserProfileDTO userProfileDTO);

        /// <summary>
        /// Gets a user profile by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user profile to retrieve.</param>
        /// <returns>The UserProfileDTO representing the user profile with the specified ID.</returns>
        UserProfileDTO GetUserProfileById(int userId);

        /// <summary>
        /// Gets a user profile by username.
        /// </summary>
        /// <param name="username">The username of the user profile to retrieve.</param>
        /// <returns>The UserProfileDTO representing the user profile with the specified username.</returns>
        UserProfileDTO GetUserProfileByUsername(string username);

        /// <summary>
        /// Gets all user profiles.
        /// </summary>
        /// <returns>An IEnumerable of UserProfileDTO representing all user profiles.</returns>
        IEnumerable<UserProfileDTO> GetAllUserProfiles();
    }
}