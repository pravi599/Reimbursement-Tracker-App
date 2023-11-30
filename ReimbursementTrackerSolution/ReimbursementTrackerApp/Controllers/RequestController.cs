using Microsoft.AspNetCore.Mvc;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using ReimbursementTrackerApp.Exceptions;

namespace ReimbursementTrackerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("reactApp")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly ILogger<RequestController> _logger;

        public RequestController(IRequestService requestService, ILogger<RequestController> logger)
        {
            _requestService = requestService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddRequest([FromBody] RequestDTO requestDTO)
        {
            _logger.LogInformation("Adding a request.");

            try
            {
                var result = _requestService.Add(requestDTO);
                return Ok(result);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to add request due to user not found.");
                return NotFound($"Failed to add request. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding request.");
                return BadRequest("Failed to add request");
            }
        }

        [HttpDelete("{requestId}")]
        public ActionResult RemoveRequest(int requestId)
        {
            _logger.LogInformation($"Removing request with ID {requestId}.");

            try
            {
                var success = _requestService.Remove(requestId);

                if (success)
                {
                    _logger.LogInformation("Request deleted");
                    return Ok("Request deleted successfully");
                }

                return NotFound("Request not found");
            }
            catch (RequestNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to remove request.");
                return NotFound($"Failed to remove request. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing request.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateRequest([FromBody] RequestDTO requestDTO)
        {
            _logger.LogInformation($"Updating request with ID {requestDTO.RequestId}.");

            try
            {
                var result = _requestService.Update(requestDTO);

                if (result != null)
                {
                    _logger.LogInformation("Request updated successfully");
                    return Ok(result);
                }

                return NotFound("Request not found");
            }
            catch (RequestNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to update request.");
                return NotFound($"Failed to update request. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating request.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{requestId}")]
        public IActionResult GetRequestById(int requestId)
        {
            _logger.LogInformation($"Getting request with ID {requestId}.");

            try
            {
                var requestDTO = _requestService.GetRequestById(requestId);

                if (requestDTO != null)
                {
                    _logger.LogInformation("Request listed with given ID");
                    return Ok(requestDTO);
                }

                return NotFound("Request not found");
            }
            catch (RequestNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to get request by ID.");
                return NotFound($"Failed to get request by ID. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting request by ID.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult GetAllRequests()
        {
            _logger.LogInformation("Getting all requests.");

            try
            {
                var requestDTOs = _requestService.GetAllRequests();
                _logger.LogInformation("All requests listed");
                return Ok(requestDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all requests.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{requestId}/{trackingStatus}")]
        public IActionResult UpdateRequestStatus(int requestId, string trackingStatus)
        {
            _logger.LogInformation($"Updating request status with ID {requestId} to {trackingStatus}.");

            try
            {
                var result = _requestService.Update(requestId, trackingStatus);
                return Ok(result);
            }
            catch (RequestNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to update request status.");
                return NotFound($"Failed to update request status. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating request status.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("category/{expenseCategory}")]
        public IActionResult GetRequestByCategory(string expenseCategory)
        {
            _logger.LogInformation($"Getting request by category: {expenseCategory}.");

            try
            {
                var requestDTO = _requestService.GetRequestByCategory(expenseCategory);

                if (requestDTO != null)
                {
                    _logger.LogInformation($"Request listed with category: {expenseCategory}");
                    return Ok(requestDTO);
                }

                return NotFound($"Request not found with category: {expenseCategory}");
            }
            catch (RequestNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to get request by category.");
                return NotFound($"Failed to get request by category. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting request by category.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("user/{username}")]
        public IActionResult GetRequestByUsername(string username)
        {
            _logger.LogInformation($"Getting request by username: {username}.");

            try
            {
                var requestDTOs = _requestService.GetRequestsByUsername(username);

                if (requestDTOs != null)
                {
                    _logger.LogInformation($"Request listed with username: {username}");
                    return Ok(requestDTOs);
                }

                return NotFound($"Request not found with username: {username}");
            }
            catch (RequestNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to get request by username.");
                return NotFound($"Failed to get request by username. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting request by username.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
