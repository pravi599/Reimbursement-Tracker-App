using System;
using System.Collections.Generic;
using System.Linq;
using ReimbursementTrackerApp.Exceptions;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Models.DTOs;
using ReimbursementTrackerApp.Repositories;

namespace ReimbursementTrackerApp.Services
{
    /// <summary>
    /// Service class for managing tracking information related to reimbursement requests.
    /// </summary>
    public class TrackingService : ITrackingService
    {
        private readonly IRepository<int, Tracking> _trackingRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingService"/> class.
        /// </summary>
        /// <param name="trackingRepository">The repository for tracking information.</param>
        public TrackingService(IRepository<int, Tracking> trackingRepository)
        {
            _trackingRepository = trackingRepository;
        }

        /// <summary>
        /// Adds tracking information for a reimbursement request.
        /// </summary>
        /// <param name="trackingDTO">Tracking information to be added.</param>
        /// <returns>Returns true if the addition is successful; otherwise, throws a TrackingNotFoundException.</returns>
        public bool Add(TrackingDTO trackingDTO)
        {
            try
            {
                var existingRequest = _trackingRepository.GetById(trackingDTO.RequestId);

                if (existingRequest != null)
                {
                    var tracking = new Tracking
                    {
                        RequestId = trackingDTO.RequestId,
                        TrackingStatus = trackingDTO.TrackingStatus,
                        ApprovalDate = trackingDTO.ApprovalDate,
                        ReimbursementDate = trackingDTO.ReimbursementDate
                    };

                    _trackingRepository.Add(tracking);
                    return true;
                }

                throw new TrackingNotFoundException();
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                throw new ServiceException();
            }
        }

        /// <summary>
        /// Removes tracking information based on the tracking ID.
        /// </summary>
        /// <param name="trackingId">The ID of the tracking information to be removed.</param>
        /// <returns>Returns true if the removal is successful; otherwise, throws a TrackingNotFoundException.</returns>
        public bool Remove(int trackingId)
        {
            try
            {
                var tracking = _trackingRepository.Delete(trackingId);

                if (tracking != null)
                {
                    return true;
                }

                throw new TrackingNotFoundException();
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                throw new ServiceException();
            }
        }

        /// <summary>
        /// Updates tracking information based on the provided TrackingDTO.
        /// </summary>
        /// <param name="trackingDTO">Updated tracking information.</param>
        /// <returns>Returns the updated TrackingDTO if the update is successful; otherwise, throws a TrackingNotFoundException.</returns>
        public TrackingDTO Update(TrackingDTO trackingDTO)
        {
            try
            {
                var existingTracking = _trackingRepository.GetById(trackingDTO.TrackingId);

                if (existingTracking != null)
                {
                    existingTracking.TrackingStatus = trackingDTO.TrackingStatus;
                    existingTracking.ApprovalDate = trackingDTO.ApprovalDate;
                    existingTracking.ReimbursementDate = trackingDTO.ReimbursementDate;

                    _trackingRepository.Update(existingTracking);

                    return trackingDTO;
                }

                throw new TrackingNotFoundException();
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                throw new ServiceException();
            }
        }

        /// <summary>
        /// Updates tracking information based on the request ID and tracking status.
        /// </summary>
        /// <param name="requestId">The ID of the reimbursement request.</param>
        /// <param name="trackingStatus">The updated tracking status.</param>
        /// <returns>Returns the updated TrackingDTO if the update is successful; otherwise, throws a TrackingNotFoundException.</returns>
        public TrackingDTO Update(int requestId, string trackingStatus)
        {
            try
            {
                var existingRequest = _trackingRepository.GetById(requestId);

                if (existingRequest != null)
                {
                    var existingTracking = _trackingRepository.GetAll()
                        .FirstOrDefault(t => t.RequestId == requestId);

                    if (existingTracking != null)
                    {
                        existingTracking.TrackingStatus = trackingStatus;
                        _trackingRepository.Update(existingTracking);

                        return new TrackingDTO
                        {
                            TrackingId = existingTracking.TrackingId,
                            RequestId = existingTracking.RequestId,
                            TrackingStatus = existingTracking.TrackingStatus,
                            ApprovalDate = existingTracking.ApprovalDate,
                            ReimbursementDate = existingTracking.ReimbursementDate
                        };
                    }

                    throw new TrackingNotFoundException();
                }

                throw new TrackingNotFoundException();
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                throw new ServiceException();
            }
        }

        /// <summary>
        /// Gets tracking information based on the request ID.
        /// </summary>
        /// <param name="requestId">The ID of the reimbursement request.</param>
        /// <returns>Returns the TrackingDTO if the tracking information is found; otherwise, throws a TrackingNotFoundException.</returns>
        public TrackingDTO GetTrackingByRequestId(int requestId)
        {
            try
            {
                var existingTracking = _trackingRepository.GetAll()
                    .FirstOrDefault(t => t.RequestId == requestId);

                if (existingTracking != null)
                {
                    return new TrackingDTO
                    {
                        TrackingId = existingTracking.TrackingId,
                        RequestId = existingTracking.RequestId,
                        TrackingStatus = existingTracking.TrackingStatus,
                        ApprovalDate = existingTracking.ApprovalDate,
                        ReimbursementDate = existingTracking.ReimbursementDate
                    };
                }

                throw new TrackingNotFoundException();
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                throw new ServiceException();
            }
        }

        /// <summary>
        /// Gets tracking information based on the tracking ID.
        /// </summary>
        /// <param name="trackingId">The ID of the tracking information to retrieve.</param>
        /// <returns>Returns the TrackingDTO if the tracking information is found; otherwise, throws a TrackingNotFoundException.</returns>
        public TrackingDTO GetTrackingByTrackingId(int trackingId)
        {
            try
            {
                var existingTracking = _trackingRepository.GetById(trackingId);

                if (existingTracking != null)
                {
                    return new TrackingDTO
                    {
                        TrackingId = existingTracking.TrackingId,
                        RequestId = existingTracking.RequestId,
                        TrackingStatus = existingTracking.TrackingStatus,
                        ApprovalDate = existingTracking.ApprovalDate,
                        ReimbursementDate = existingTracking.ReimbursementDate
                    };
                }

                throw new TrackingNotFoundException();
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                throw new ServiceException();
            }
        }

        /// <summary>
        /// Gets all tracking information.
        /// </summary>
        /// <returns>Returns a collection of TrackingDTO representing all tracking information.</returns>
        public IEnumerable<TrackingDTO> GetAllTrackings()
        {
            try
            {
                var trackings = _trackingRepository.GetAll();

                if (trackings != null && trackings.Any())
                {
                    var trackingDTOs = new List<TrackingDTO>();
                    foreach (var existingTracking in trackings)
                    {
                        trackingDTOs.Add(new TrackingDTO
                        {
                            TrackingId = existingTracking.TrackingId,
                            RequestId = existingTracking.RequestId,
                            TrackingStatus = existingTracking.TrackingStatus,
                            ApprovalDate = existingTracking.ApprovalDate,
                            ReimbursementDate = existingTracking.ReimbursementDate
                        });
                    }
                    return trackingDTOs;
                }

                throw new TrackingNotFoundException();
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                throw new ServiceException();
            }
        }
    }
}