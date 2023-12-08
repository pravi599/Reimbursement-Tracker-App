using Microsoft.AspNetCore.Mvc;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using ReimbursementTrackerApp.Exceptions;

namespace ReimbursementTrackerApp.Controllers
{
    /// <summary>
    /// Controller for managing tracking-related operations through RESTful API.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("reactApp")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly ILogger<RequestController> _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestController"/> class.
        /// </summary>
        /// <param name="requestService">The service for managing request-related operations.</param>
        /// <param name="logger">The logger for logging events in the controller.</param>
        public RequestController(IRequestService requestService, ILogger<RequestController> logger)
        {
            _requestService = requestService;
            _logger = logger;
        }
        /// <summary>
        /// Adds a new request.
        /// </summary>
        /// <param name="requestDTO">The data for the new request.</param>
        /// <returns>The result of the request addition operation.</returns>
        [HttpPost]
        public IActionResult AddRequest([FromForm] RequestDTO requestDTO)
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
        /// <summary>
        /// Removes a request by ID.
        /// </summary>
        /// <param name="requestId">The ID of the request to be removed.</param>
        /// <returns>The result of the request removal operation.</returns>
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
        /// <summary>
        /// Updates a request.
        /// </summary>
        /// <param name="requestDTO">The data for updating the request.</param>
        /// <returns>The result of the request update operation.</returns>
        [HttpPut]
        public IActionResult UpdateRequest([FromForm] RequestDTO requestDTO)
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
        /// <summary>
        /// Gets a request by ID.
        /// </summary>
        /// <param name="requestId">The ID of the request to be retrieved.</param>
        /// <returns>The result of the request retrieval operation.</returns>
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
        /// <summary>
        /// Gets all requests.
        /// </summary>
        /// <returns>The result of the operation to get all requests.</returns>
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
        /// <summary>
        /// Gets a request by expense category.
        /// </summary>
        /// <param name="expenseCategory">The expense category of the request.</param>
        /// <returns>The result of the request retrieval by category operation.</returns>

        [HttpGet("category/{expenseCategory}")]
        public IActionResult GetRequestByCategory(string expenseCategory)
        {
            _logger.LogInformation($"Getting request by category: {expenseCategory}.");

            try
            {
                var requestDTO = _requestService.GetRequestsByCategory(expenseCategory);

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
        /// <summary>
        /// Gets requests by username.
        /// </summary>
        /// <param name="username">The username associated with the requests.</param>
        /// <returns>The result of the requests retrieval by username operation.</returns>
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