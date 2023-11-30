using ReimbursementTrackerApp.Models.DTOs;

namespace ReimbursementTrackerApp.Interfaces
{
    public interface IUserProfileService
    {
        bool Add(UserProfileDTO userProfileDTO);
        bool Remove(string username);
        UserProfileDTO Update(UserProfileDTO userProfileDTO);
        UserProfileDTO GetUserProifleById(int userId);
        UserProfileDTO GetUserProifleByUsername(string username);
        IEnumerable<UserProfileDTO> GetAllUserProfiles();
    }
}
