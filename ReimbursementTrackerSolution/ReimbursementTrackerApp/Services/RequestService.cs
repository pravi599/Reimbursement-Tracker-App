using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models.DTOs;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Exceptions;

namespace ReimbursementTrackerApp.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<int, Request> _requestRepository;
        private readonly IRepository<int, Tracking> _trackingRepository;
        private readonly IRepository<string, User> _userRepository;

        public RequestService(
            IRepository<int, Request> requestRepository,
            IRepository<int, Tracking> trackingRepository,
            IRepository<string, User> userRepository)
        {
            _requestRepository = requestRepository;
            _trackingRepository = trackingRepository;
            _userRepository = userRepository;
        }

        private RequestDTO CreateRequestDTOFromModel(Request request)
        {
            return new RequestDTO
            {
                RequestId = request.RequestId,
                ExpenseCategory = request.ExpenseCategory,
                Amount = request.Amount,
                Document = request.Document,
                //Receipt = request.Receipt,
                Description = request.Description,
                RequestDate = request.RequestDate,
                Username = request.Username
            };
        }

        public bool Add(RequestDTO requestDTO)
        {
            var user = _userRepository.GetAll()
                .FirstOrDefault(r => r.Username == requestDTO.Username);

            if (user != null)
            {
                var request = new Request
                {
                    ExpenseCategory = requestDTO.ExpenseCategory,
                    Amount = requestDTO.Amount,
                    Document = requestDTO.Document,
                    //Receipt = requestDTO.Receipt,
                    Description = requestDTO.Description,
                    RequestDate = DateTime.Now,
                    Username = requestDTO.Username
                };

                _requestRepository.Add(request);

                var tracking = new Tracking
                {
                    TrackingStatus = "Pending",
                    ApprovalDate = null,
                    ReimbursementDate = null,
                    Request = request
                };

                _trackingRepository.Add(tracking);

                return true;
            }

            throw new UserNotFoundException();
        }

        public bool Remove(int requestId)
        {
            var request = _requestRepository.Delete(requestId);

            if (request != null)
            {
                return true;
            }

            throw new RequestNotFoundException();
        }

        public RequestDTO Update(RequestDTO requestDTO)
        {
            var existingRequest = _requestRepository.GetById(requestDTO.RequestId);

            if (existingRequest != null)
            {
                existingRequest.ExpenseCategory = requestDTO.ExpenseCategory;
                existingRequest.Amount = requestDTO.Amount;
                existingRequest.Document = requestDTO.Document;
                //existingRequest.Receipt = requestDTO.Receipt;
                existingRequest.Description = requestDTO.Description;

                _requestRepository.Update(existingRequest);

                return CreateRequestDTOFromModel(existingRequest);
            }

            throw new RequestNotFoundException();
        }

        public RequestDTO Update(int requestId, string trackingStatus)
        {
            var existingRequest = _requestRepository.GetById(requestId);

            if (existingRequest != null)
            {
                var existingTracking = _trackingRepository.GetAll()
                    .FirstOrDefault(t => t.RequestId == requestId);

                if (existingTracking != null)
                {
                    existingTracking.TrackingStatus = trackingStatus;
                    _trackingRepository.Update(existingTracking);

                    return CreateRequestDTOFromModel(existingRequest);
                }
                else
                {
                    // Handle the case where tracking information is not found for the request.
                    throw new TrackingNotFoundException();
                }
            }

            throw new RequestNotFoundException();
        }

        public RequestDTO GetRequestById(int requestId)
        {
            var existingRequest = _requestRepository.GetById(requestId);

            if (existingRequest != null)
            {
                return CreateRequestDTOFromModel(existingRequest);
            }

            throw new RequestNotFoundException();
        }

        public RequestDTO GetRequestByCategory(string expenseCategory)
        {
            var existingRequest = _requestRepository.GetAll()
                .FirstOrDefault(r => r.ExpenseCategory == expenseCategory);

            if (existingRequest != null)
            {
                return CreateRequestDTOFromModel(existingRequest);
            }

            throw new RequestNotFoundException();
        }

        public IEnumerable<RequestDTO> GetRequestsByUsername(string username)
        {
            var requests = _requestRepository.GetAll()
            .Where(r => r.User.Username == username).ToList();

            var requestDTOs = new List<RequestDTO>();

            foreach (var existingRequest in requests)
            {
                requestDTOs.Add(CreateRequestDTOFromModel(existingRequest));
            }

            return requestDTOs;
        }

        public IEnumerable<RequestDTO> GetAllRequests()
        {
            var requests = _requestRepository.GetAll();

            var requestDTOs = new List<RequestDTO>();

            foreach (var existingRequest in requests)
            {
                requestDTOs.Add(CreateRequestDTOFromModel(existingRequest));
            }

            return requestDTOs;
        }
    }
}

