using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReimbursementTrackerApp.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        public string Username { get; set; }//Foreign key
        [Required]
        public string ExpenseCategory { get; set; }
        public float Amount { get; set; }
        public string? Document { get; set; }
        //public string Receipt { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; }
        [ForeignKey("Username")]
        public User? User { get; set; }
    }
}
