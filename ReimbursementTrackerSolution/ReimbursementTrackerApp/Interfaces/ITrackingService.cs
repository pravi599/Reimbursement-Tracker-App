using ReimbursementTrackerApp.Models.DTOs;

namespace ReimbursementTrackerApp.Interfaces
{
    public interface ITrackingService
    {
        bool Add(TrackingDTO tarcikngtDTO);
        bool Remove(int tackingId);
        TrackingDTO Update(TrackingDTO tarcikngtDTO);
        TrackingDTO Update(int RequestId, string TrackingStatus);
        TrackingDTO GetTrackingByRequestId(int requestId);
        TrackingDTO GetTrackingByTrackingId(int complaintId);
        IEnumerable<TrackingDTO> GetAllTrackings();
    }
}
