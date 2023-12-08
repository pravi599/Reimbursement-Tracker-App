using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models.DTOs;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ReimbursementTrackerApp.Services
{
    /// <summary>
    /// Service class for managing user profiles.
    /// </summary>
    public class UserProfileService : IUserProfileService
    {
        private readonly IRepository<int, UserProfile> _userProfileRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileService"/> class.
        /// </summary>
        /// <param name="userProfileRepository">The repository for user profiles.</param>
        public UserProfileService(IRepository<int, UserProfile> userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        /// <summary>
        /// Adds a new user profile.
        /// </summary>
        /// <param name="userProfileDTO">User profile information for addition.</param>
        /// <returns>Returns true if the addition is successful; otherwise, throws a UserProfileAlreadyExistsException.</returns>
        public bool Add(UserProfileDTO userProfileDTO)
        {
            var existingProfile = _userProfileRepository.GetAll()
                .FirstOrDefault(u => u.Username == userProfileDTO.Username);

            if (existingProfile == null)
            {
                var userProfile = new UserProfile
                {
                    Username = userProfileDTO.Username,
                    FirstName = userProfileDTO.FirstName,
                    LastName = userProfileDTO.LastName,
                    City = userProfileDTO.City,
                    ContactNumber = userProfileDTO.ContactNumber,
                    BankAccountNumber = userProfileDTO.BankAccountNumber
                };

                _userProfileRepository.Add(userProfile);

                return true;
            }

            throw new UserProfileAlreadyExistsException();
        }

        /// <summary>
        /// Removes a user profile by username.
        /// </summary>
        /// <param name="username">The username associated with the user profile to be removed.</param>
        /// <returns>Returns true if the removal is successful; otherwise, throws a UserProfileNotFoundException.</returns>
        public bool Remove(string username)
        {
            var userProfile = _userProfileRepository.GetAll()
                .FirstOrDefault(u => u.Username == username);

            if (userProfile != null)
            {
                _userProfileRepository.Delete(userProfile.UserId);
                return true;
            }

            throw new UserProfileNotFoundException();
        }

        /// <summary>
        /// Updates an existing user profile.
        /// </summary>
        /// <param name="userProfileDTO">Updated user profile information.</param>
        /// <returns>Returns the updated UserProfileDTO if the update is successful; otherwise, throws a UserProfileNotFoundException.</returns>
        public UserProfileDTO Update(UserProfileDTO userProfileDTO)
        {
            var existingProfile = _userProfileRepository.GetById(userProfileDTO.UserId);

            if (existingProfile != null)
            {
                existingProfile.FirstName = userProfileDTO.FirstName;
                existingProfile.LastName = userProfileDTO.LastName;
                existingProfile.City = userProfileDTO.City;
                existingProfile.ContactNumber = userProfileDTO.ContactNumber;
                existingProfile.BankAccountNumber = userProfileDTO.BankAccountNumber;

                _userProfileRepository.Update(existingProfile);

                return new UserProfileDTO
                {
                    UserId = existingProfile.UserId,
                    Username = existingProfile.Username,
                    FirstName = existingProfile.FirstName,
                    LastName = existingProfile.LastName,
                    City = existingProfile.City,
                    ContactNumber = existingProfile.ContactNumber,
                    BankAccountNumber = existingProfile.BankAccountNumber
                };
            }

            throw new UserProfileNotFoundException();
        }

        /// <summary>
        /// Gets a user profile by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user profile to retrieve.</param>
        /// <returns>Returns the UserProfileDTO if the user profile is found; otherwise, throws a UserProfileNotFoundException.</returns>
        public UserProfileDTO GetUserProifleById(int userId)
        {
            var userProfile = _userProfileRepository.GetById(userId);

            if (userProfile != null)
            {
                return new UserProfileDTO
                {
                    UserId = userProfile.UserId,
                    Username = userProfile.Username,
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName,
                    City = userProfile.City,
                    ContactNumber = userProfile.ContactNumber,
                    BankAccountNumber = userProfile.BankAccountNumber
                };
            }

            throw new UserProfileNotFoundException();
        }

        /// <summary>
        /// Gets a user profile by username.
        /// </summary>
        /// <param name="username">The username associated with the user profile to retrieve.</param>
        /// <returns>Returns the UserProfileDTO if the user profile is found; otherwise, throws a UserProfileNotFoundException.</returns>
        public UserProfileDTO GetUserProfileByUsername(string username)
        {
            var userProfile = _userProfileRepository.GetAll()
                .FirstOrDefault(u => u.Username == username);

            if (userProfile != null)
            {
                return new UserProfileDTO
                {
                    UserId = userProfile.UserId,
                    Username = userProfile.Username,
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName,
                    City = userProfile.City,
                    ContactNumber = userProfile.ContactNumber,
                    BankAccountNumber = userProfile.BankAccountNumber
                };
            }

            throw new UserProfileNotFoundException();
        }

        /// <summary>
        /// Gets all user profiles.
        /// </summary>
        /// <returns>Returns a collection of UserProfileDTO representing all user profiles.</returns>
        public IEnumerable<UserProfileDTO> GetAllUserProfiles()
        {
            var userProfiles = _userProfileRepository.GetAll();

            return userProfiles.Select(u => new UserProfileDTO
            {
                UserId = u.UserId,
                Username = u.Username,
                FirstName = u.FirstName,
                LastName = u.LastName,
                City = u.City,
                ContactNumber = u.ContactNumber,
                BankAccountNumber = u.BankAccountNumber
            });
        }
    }
}