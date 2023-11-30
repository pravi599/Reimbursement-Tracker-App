using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models.DTOs;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Exceptions;

namespace ReimbursementTrackerApp.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IRepository<int, UserProfile> _userProfileRepository;

        public UserProfileService(IRepository<int, UserProfile> userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

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

        public UserProfileDTO GetUserProifleByUsername(string username)
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

