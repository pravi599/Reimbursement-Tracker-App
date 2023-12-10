using Microsoft.AspNetCore.Mvc;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
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
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _trackingService;
        private readonly ILogger<TrackingController> _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingController"/> class.
        /// </summary>
        /// <param name="trackingService">The service for managing tracking-related operations.</param>
        /// <param name="logger">The logger for logging events in the controller.</param>
        public TrackingController(ITrackingService trackingService, ILogger<TrackingController> logger)
        {
            _trackingService = trackingService;
            _logger = logger;
        }


        /// <summary>
        /// Adds tracking information.
        /// </summary>
        /// <param name="trackingDTO">The data for tracking information.</param>
        /// <returns>The result of the tracking addition operation.</returns>

        [HttpPost]
        public IActionResult AddTracking([FromBody] TrackingDTO trackingDTO)
        {
            _logger.LogInformation("Adding tracking.");

            try
            {
                var result = _trackingService.Add(trackingDTO);
                return Ok(result);
            }
            catch (TrackingNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to add tracking due to tracking information not found.");
                return NotFound($"Failed to add tracking. {ex.Message}");
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error adding tracking.");
                return BadRequest($"Failed to add tracking. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding tracking.");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Removes tracking information by ID.
        /// </summary>
        /// <param name="trackingId">The ID of the tracking information to be removed.</param>
        /// <returns>The result of the tracking removal operation.</returns>

        [HttpDelete("{trackingId}")]
        public ActionResult RemoveTracking(int trackingId)
        {
            _logger.LogInformation($"Removing tracking with ID {trackingId}.");

            try
            {
                var success = _trackingService.Remove(trackingId);

                if (success)
                {
                    _logger.LogInformation("Tracking deleted");
                    return Ok("Tracking deleted successfully");
                }

                return NotFound("Tracking not found");
            }
            catch (TrackingNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to remove tracking.");
                return NotFound($"Failed to remove tracking. {ex.Message}");
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error removing tracking.");
                return StatusCode(500, $"Failed to remove tracking. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while removing tracking.");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Updates tracking information.
        /// </summary>
        /// <param name="trackingDTO">The data for updating tracking information.</param>
        /// <returns>The result of the tracking update operation.</returns>

        [HttpPut]
        public IActionResult UpdateTracking([FromBody] TrackingDTO trackingDTO)
        {
            _logger.LogInformation($"Updating tracking with ID {trackingDTO.TrackingId}.");

            try
            {
                var result = _trackingService.Update(trackingDTO);

                if (result != null)
                {
                    _logger.LogInformation("Tracking updated successfully");
                    return Ok(result);
                }

                return NotFound("Tracking not found");
            }
            catch (TrackingNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to update tracking.");
                return NotFound($"Failed to update tracking. {ex.Message}");
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error updating tracking.");
                return BadRequest($"Failed to update tracking. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating tracking.");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Gets tracking information by ID.
        /// </summary>
        /// <param name="trackingId">The ID of the tracking information to be retrieved.</param>
        /// <returns>The result of the tracking retrieval operation.</returns>

        [HttpGet("{trackingId}")]
        public IActionResult GetTrackingById(int trackingId)
        {
            _logger.LogInformation($"Getting tracking with ID {trackingId}.");

            try
            {
                var trackingDTO = _trackingService.GetTrackingByTrackingId(trackingId);

                if (trackingDTO != null)
                {
                    _logger.LogInformation("Tracking listed with given ID");
                    return Ok(trackingDTO);
                }

                return NotFound("Tracking not found");
            }
            catch (TrackingNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to get tracking by ID.");
                return NotFound($"Failed to get tracking by ID. {ex.Message}");
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error getting tracking by ID.");
                return StatusCode(500, $"Failed to get tracking by ID. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting tracking by ID.");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Gets all tracking information.
        /// </summary>
        /// <returns>The result of the operation to get all tracking information.</returns>

        [HttpGet]
        public IActionResult GetAllTrackings()
        {
            _logger.LogInformation("Getting all trackings.");

            try
            {
                var trackingDTOs = _trackingService.GetAllTrackings();
                _logger.LogInformation("All trackings listed");
                return Ok(trackingDTOs);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting all trackings.");
                return StatusCode(500, $"Failed to get all trackings. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting all trackings.");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Updates the tracking status for a specific request.
        /// </summary>
        /// <param name="requestId">The ID of the request to update the tracking status for.</param>
        /// <param name="trackingStatus">The new tracking status.</param>
        /// <returns>The result of the tracking status update operation.</returns>

        [HttpPut("{requestId}/{trackingStatus}")]
        public IActionResult UpdateTrackingStatus(int requestId, string trackingStatus)
        {
            _logger.LogInformation($"Updating tracking status for request with ID {requestId} to {trackingStatus}.");

            try
            {
                var result = _trackingService.Update(requestId, trackingStatus);
                return Ok(result);
            }
            catch (TrackingNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to update tracking status.");
                return NotFound($"Failed to update tracking status. {ex.Message}");
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error updating tracking status.");
                return BadRequest($"Failed to update tracking status. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating tracking status.");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Gets tracking information by request ID.
        /// </summary>
        /// <param name="requestId">The ID of the request associated with the tracking information.</param>
        /// <returns>The result of the tracking retrieval by request ID operation.</returns>

        [HttpGet("request/{requestId}")]
        public IActionResult GetTrackingByRequestId(int requestId)
        {
            _logger.LogInformation($"Getting tracking by request ID: {requestId}.");

            try
            {
                var trackingDTO = _trackingService.GetTrackingByRequestId(requestId);

                if (trackingDTO != null)
                {
                    _logger.LogInformation($"Tracking listed with request ID: {requestId}");
                    return Ok(trackingDTO);
                }

                return NotFound($"Tracking not found with request ID: {requestId}");
            }
            catch (TrackingNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to get tracking by request ID.");
                return NotFound($"Failed to get tracking by request ID. {ex.Message}");
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error getting tracking by request ID.");
                return StatusCode(500, $"Failed to get tracking by request ID. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting tracking by request ID.");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Gets tracking information for a particular user by username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The result of the tracking retrieval by username operation.</returns>
        [HttpGet("user/{username}")]
        public IActionResult GetTrackingsByUsername(string username)
        {
            _logger.LogInformation($"Getting trackings for user: {username}.");

            try
            {
                var trackingDTOs = _trackingService.GetTrackingsByUsername(username);

                if (trackingDTOs != null && trackingDTOs.Any())
                {
                    _logger.LogInformation($"Trackings listed for user: {username}");
                    return Ok(trackingDTOs);
                }

                return NotFound($"No trackings found for user: {username}");
            }
            catch (TrackingNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to get trackings by username.");
                return NotFound($"Failed to get trackings by username. {ex.Message}");
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error getting trackings by username.");
                return StatusCode(500, $"Failed to get trackings by username. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting trackings by username.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}