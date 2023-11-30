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
    public class TrackingService : ITrackingService
    {
        private readonly IRepository<int, Tracking> _trackingRepository;

        public TrackingService(IRepository<int, Tracking> trackingRepository)
        {
            _trackingRepository = trackingRepository;
        }

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
                throw new ServiceException();
            }
        }

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
                throw new ServiceException();
            }
        }

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
                throw new ServiceException();
            }
        }

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
