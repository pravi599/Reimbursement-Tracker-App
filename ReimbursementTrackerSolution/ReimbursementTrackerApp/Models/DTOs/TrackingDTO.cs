using System.ComponentModel.DataAnnotations.Schema;

namespace ReimbursementTrackerApp.Models.DTOs
{
    public class TrackingDTO
    {
        public int TrackingId { get; set; }
        public int RequestId { get; set; }
        public string TrackingStatus { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? ReimbursementDate { get; set; }
    }
}
