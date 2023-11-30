using Microsoft.AspNetCore.Mvc;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models.DTOs;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using ReimbursementTrackerApp.Exceptions;

namespace ReimbursementTrackerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("reactApp")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(IUserProfileService userProfileService, ILogger<UserProfileController> logger)
        {
            _userProfileService = userProfileService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddUserProfile([FromBody] UserProfileDTO userProfileDTO)
        {
            _logger.LogInformation($"Adding user profile for {userProfileDTO.Username}.");

            try
            {
                var result = _userProfileService.Add(userProfileDTO);
                return Ok(result);
            }
            catch (UserProfileAlreadyExistsException ex)
            {
                _logger.LogError(ex, $"Failed to add user profile. {ex.Message}");
                return Conflict($"Failed to add user profile. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user profile.");
                return BadRequest("Failed to add user profile");
            }
        }

        [HttpDelete("{username}")]
        public ActionResult RemoveUserProfile(string username)
        {
            _logger.LogInformation($"Removing user profile for {username}.");

            try
            {
                var success = _userProfileService.Remove(username);

                if (success)
                {
                    _logger.LogInformation("User profile deleted");
                    return Ok("User profile deleted successfully");
                }

                return NotFound("User profile not found");
            }
            catch (UserProfileNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to remove user profile.");
                return NotFound($"Failed to remove user profile. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing user profile.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult UpdateUserProfile([FromBody] UserProfileDTO userProfileDTO)
        {
            _logger.LogInformation($"Updating user profile for {userProfileDTO.Username}.");

            try
            {
                var result = _userProfileService.Update(userProfileDTO);

                if (result != null)
                {
                    _logger.LogInformation("User profile updated successfully");
                    return Ok(result);
                }

                return NotFound("User profile not found");
            }
            catch (UserProfileNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to update user profile.");
                return NotFound($"Failed to update user profile. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user profile.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserProfileById(int userId)
        {
            _logger.LogInformation($"Getting user profile with ID {userId}.");

            try
            {
                var userProfileDTO = _userProfileService.GetUserProifleById(userId);

                if (userProfileDTO != null)
                {
                    _logger.LogInformation("User profile listed with given ID");
                    return Ok(userProfileDTO);
                }

                return NotFound("User profile not found");
            }
            catch (UserProfileNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to get user profile by ID.");
                return NotFound($"Failed to get user profile by ID. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user profile by ID.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("username/{username}")]
        public IActionResult GetUserProfileByUsername(string username)
        {
            _logger.LogInformation($"Getting user profile by username: {username}.");

            try
            {
                var userProfileDTO = _userProfileService.GetUserProifleByUsername(username);

                if (userProfileDTO != null)
                {
                    _logger.LogInformation($"User profile listed with username: {username}");
                    return Ok(userProfileDTO);
                }

                return NotFound($"User profile not found with username: {username}");
            }
            catch (UserProfileNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to get user profile by username.");
                return NotFound($"Failed to get user profile by username. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user profile by username.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult GetAllUserProfiles()
        {
            _logger.LogInformation("Getting all user profiles.");

            try
            {
                var userProfileDTOs = _userProfileService.GetAllUserProfiles();
                _logger.LogInformation("All user profiles listed");
                return Ok(userProfileDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all user profiles.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

