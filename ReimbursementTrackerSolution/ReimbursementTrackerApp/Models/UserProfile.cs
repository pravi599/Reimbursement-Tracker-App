using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReimbursementTrackerApp.Models
{
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; } //ForeignKey
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string City { get; set; }
        public string ContactNumber { get; set; }
        public string BankAccountNumber { get; set; }

        [ForeignKey("Username")]
        public User? User { get; set; }
    }
}
