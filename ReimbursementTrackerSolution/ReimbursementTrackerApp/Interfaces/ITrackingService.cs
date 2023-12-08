using ReimbursementTrackerApp.Models.DTOs;
using System.Collections.Generic;

namespace ReimbursementTrackerApp.Interfaces
{
    /// <summary>
    /// Interface for the Tracking Service, providing methods for managing tracking information.
    /// </summary>
    public interface ITrackingService
    {
        /// <summary>
        /// Adds new tracking information.
        /// </summary>
        /// <param name="trackingDTO">The TrackingDTO containing tracking information to be added.</param>
        /// <returns>True if the tracking information is added successfully; otherwise, false.</returns>
        bool Add(TrackingDTO trackingDTO);

        /// <summary>
        /// Removes tracking information by tracking ID.
        /// </summary>
        /// <param name="trackingId">The ID of the tracking information to be removed.</param>
        /// <returns>True if the tracking information is removed successfully; otherwise, false.</returns>
        bool Remove(int trackingId);

        /// <summary>
        /// Updates existing tracking information.
        /// </summary>
        /// <param name="trackingDTO">The TrackingDTO containing updated tracking information.</param>
        /// <returns>The updated TrackingDTO if the update is successful; otherwise, null.</returns>
        TrackingDTO Update(TrackingDTO trackingDTO);

        /// <summary>
        /// Updates tracking status for a request.
        /// </summary>
        /// <param name="requestId">The ID of the request for which to update the tracking status.</param>
        /// <param name="trackingStatus">The new tracking status.</param>
        /// <returns>The updated TrackingDTO if the update is successful; otherwise, null.</returns>
        TrackingDTO Update(int requestId, string trackingStatus);

        /// <summary>
        /// Gets tracking information by request ID.
        /// </summary>
        /// <param name="requestId">The ID of the request for which to retrieve tracking information.</param>
        /// <returns>The TrackingDTO representing the tracking information for the specified request ID.</returns>
        TrackingDTO GetTrackingByRequestId(int requestId);

        /// <summary>
        /// Gets tracking information by tracking ID.
        /// </summary>
        /// <param name="trackingId">The ID of the tracking information to retrieve.</param>
        /// <returns>The TrackingDTO representing the tracking information with the specified ID.</returns>
        TrackingDTO GetTrackingByTrackingId(int trackingId);

        /// <summary>
        /// Gets all tracking information.
        /// </summary>
        /// <returns>An IEnumerable of TrackingDTO representing all tracking information.</returns>
        IEnumerable<TrackingDTO> GetAllTrackings();
    }
}
