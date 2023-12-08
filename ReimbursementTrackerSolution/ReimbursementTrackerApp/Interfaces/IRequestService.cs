using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Models.DTOs;

namespace ReimbursementTrackerApp.Interfaces
{
    /// <summary>
    /// Interface for the Request Service, defining methods for managing user requests.
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// Adds a new request.
        /// </summary>
        /// <param name="requestDTO">The RequestDTO containing request information.</param>
        /// <returns>True if the request is added successfully, otherwise false.</returns>
        bool Add(RequestDTO requestDTO);

        /// <summary>
        /// Removes a request by its ID.
        /// </summary>
        /// <param name="requestId">The ID of the request to be removed.</param>
        /// <returns>True if the request is removed successfully, otherwise false.</returns>
        bool Remove(int requestId);

        /// <summary>
        /// Updates an existing request.
        /// </summary>
        /// <param name="requestDTO">The updated RequestDTO.</param>
        /// <returns>The updated RequestDTO if the update is successful, otherwise null.</returns>
        RequestDTO Update(RequestDTO requestDTO);

        /// <summary>
        /// Updates the status of a request by its ID.
        /// </summary>
        /// <param name="requestId">The ID of the request to update.</param>
        /// <param name="trackingStatus">The new tracking status.</param>
        /// <returns>The updated RequestDTO if the update is successful, otherwise null.</returns>

        RequestDTO GetRequestById(int complaintId);

        /// <summary>
        /// Retrieves requests by expense category.
        /// </summary>
        /// <param name="expenseCategory">The expense category to filter requests.</param>
        /// <returns>A collection of RequestDTOs matching the specified category.</returns>
        Request GetRequestsByCategory(string expenseCategory);

        /// <summary>
        /// Retrieves requests by username.
        /// </summary>
        /// <param name="username">The username to filter requests.</param>
        /// <returns>A collection of RequestDTOs associated with the specified username.</returns>
        public IEnumerable<Request> GetRequestsByUsername(string username);

        /// <summary>
        /// Retrieves all requests.
        /// </summary>
        /// <returns>A collection of all RequestDTOs.</returns>
        IEnumerable<Request> GetAllRequests();
    }
}
