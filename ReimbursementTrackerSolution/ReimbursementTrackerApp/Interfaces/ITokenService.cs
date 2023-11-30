using ReimbursementTrackerApp.Models.DTOs;

namespace ReimbursementTrackerApp.Interfaces
{
    public interface ITokenService
    {
        string GetToken(UserDTO user);
    }
}
