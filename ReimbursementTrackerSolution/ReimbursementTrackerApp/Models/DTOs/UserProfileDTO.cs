using System.ComponentModel.DataAnnotations.Schema;

namespace ReimbursementTrackerApp.Models.DTOs
{
    public class UserProfileDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string ContactNumber { get; set; }
        public string BankAccountNumber { get; set; }

    }
}
