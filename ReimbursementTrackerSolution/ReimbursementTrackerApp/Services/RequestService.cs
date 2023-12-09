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
        private readonly IWebHostEnvironment _hostingEnvironment;

        public RequestService(
            IRepository<int, Request> requestRepository,
            IRepository<int, Tracking> trackingRepository,
            IWebHostEnvironment hostingEnvironment)
        {
            _requestRepository = requestRepository;
            _trackingRepository = trackingRepository;
            _hostingEnvironment = hostingEnvironment; // Assign IWebHostEnvironment
        }


        public bool Add(RequestDTO requestDTO)
        {
            var documentPath = SaveDocument(requestDTO.Document);

            var request = new Request
            {
                ExpenseCategory = requestDTO.ExpenseCategory,
                Amount = requestDTO.Amount,
                Document = documentPath,
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


        private string SaveDocument(IFormFile document)
        {
            if (document != null && document.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Documents");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + document.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    document.CopyTo(stream);
                }

                return "https://localhost:7007/Documents/" + uniqueFileName;
            }

            return null;
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
                existingRequest.Description = requestDTO.Description;
                existingRequest.RequestDate = requestDTO.RequestDate;


                // Update the document only if a new file is provided
                if (requestDTO.Document != null)
                {
                    var documentPath = SaveDocument(requestDTO.Document);
                    existingRequest.Document = documentPath;

                    /*                    // Save the new file to wwwroot/Images
                                        var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "Images", requestDTO.Document.FileName);
                                        using (var stream = new FileStream(imagePath, FileMode.Create))
                                        {
                                            requestDTO.Document.CopyTo(stream);
                                        }*/

                    //existingRequest.Document = "https://localhost:7007/Documents/" + requestDTO.Document.FileName; // Adjust the path
                }

                _requestRepository.Update(existingRequest);

                return requestDTO;
            }

            throw new RequestNotFoundException();
        }




        public RequestDTO GetRequestById(int requestId)
        {
            var existingRequest = _requestRepository.GetById(requestId);

            if (existingRequest != null)
            {
                var requestDto = new RequestDTO
                {
                    RequestId = existingRequest.RequestId,
                    Username = existingRequest.Username,
                    ExpenseCategory = existingRequest.ExpenseCategory,
                    Amount = existingRequest.Amount,
                    Description = existingRequest.Description,
                    RequestDate = existingRequest.RequestDate,
                    Document = GetDocumentAsFormFile(existingRequest.Document)
                };

                return requestDto;
            }

            throw new RequestNotFoundException();
        }




        public Request GetRequestsByCategory(string expenseCategory)
        {
            var existingRequest = _requestRepository.GetAll()
                .FirstOrDefault(r => r.ExpenseCategory == expenseCategory);

            if (existingRequest != null)
            {
                return new Request
                {
                    RequestId = existingRequest.RequestId,
                    ExpenseCategory = existingRequest.ExpenseCategory,
                    Amount = existingRequest.Amount,
                    Description = existingRequest.Description,
                    RequestDate = existingRequest.RequestDate,
                    Document = existingRequest.Document,
                    Username = existingRequest.Username
                };
            }

            throw new RequestNotFoundException();
        }
        public IEnumerable<Request> GetRequestsByUsername(string username)
        {
            var requests = _requestRepository.GetAll()
                .Where(r => r.Username == username);

            return requests.Select(existingRequest => new Request
            {
                RequestId = existingRequest.RequestId,
                ExpenseCategory = existingRequest.ExpenseCategory,
                Amount = existingRequest.Amount,
                Description = existingRequest.Description,
                RequestDate = existingRequest.RequestDate,
                Document = existingRequest.Document,
                Username = existingRequest.Username
            }).ToList();
        }

        public IEnumerable<Request> GetAllRequests()
        {
            var requests = _requestRepository.GetAll();

            return requests.Select(existingRequest => new Request
            {
                RequestId = existingRequest.RequestId,
                ExpenseCategory = existingRequest.ExpenseCategory,
                Amount = existingRequest.Amount,
                Description = existingRequest.Description,
                RequestDate = existingRequest.RequestDate,
                Document = existingRequest.Document,
                Username = existingRequest.Username
            }).ToList();
        }


        private IFormFile GetDocumentAsFormFile(string documentPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(documentPath))
                {
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, documentPath);

                    if (File.Exists(imagePath))
                    {
                        var fileBytes = File.ReadAllBytes(imagePath);
                        var memoryStream = new MemoryStream(fileBytes);
                        return new FormFile(memoryStream, 0, fileBytes.Length, "Document", Path.GetFileName(imagePath));
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it according to your application's needs.
                Console.WriteLine($"Error loading document: {ex.Message}");
            }

            return null;
        }



        // Helper method to retrieve image as base64

    }
}