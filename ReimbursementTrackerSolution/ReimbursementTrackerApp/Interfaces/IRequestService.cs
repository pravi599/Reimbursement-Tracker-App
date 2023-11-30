using ReimbursementTrackerApp.Models.DTOs;

namespace ReimbursementTrackerApp.Interfaces
{
    public interface IRequestService
    {
        bool Add(RequestDTO requestDTO);
        bool Remove(int requestId);
        RequestDTO Update(RequestDTO requestDTO);
        RequestDTO Update(int RequestId, string TrackingStatus);
        RequestDTO GetRequestById(int complaintId);
        RequestDTO GetRequestByCategory(string expenseCategory);
        public IEnumerable<RequestDTO> GetRequestsByUsername(string username);
        IEnumerable<RequestDTO> GetAllRequests();
    }
}
