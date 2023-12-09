
using System.ComponentModel.DataAnnotations;

namespace ReimbursementTrackerApp.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        // Storing the hashed password as a byte array for added security
        public byte[] Password { get; set; }

        // Storing a key as a byte array 
        public byte[] Key { get; set; }

        // User role (e.g.HR, Employee)
        public string Role { get; set; }

        // Navigation properties
        public ICollection<Request>? Requests{ get; set; }
    }
}
