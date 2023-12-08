using System.ComponentModel.DataAnnotations;

namespace ReimbursementTrackerApp.Models.DTOs
{
    public class RequestDTO
    {
        public int RequestId { get; set; }
        public string Username { get; set; }//Foreign key
        public string ExpenseCategory { get; set; }
        public float Amount { get; set; }
        public IFormFile? Document { get; set; }
        //public string Receipt { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
