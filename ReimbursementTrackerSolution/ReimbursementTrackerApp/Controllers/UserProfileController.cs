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
    /// Controller for managing user profiles through RESTful API.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("reactApp")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly ILogger<UserProfileController> _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileController"/> class.
        /// </summary>
        /// <param name="userProfileService">The service for managing user profiles.</param>
        /// <param name="logger">The logger for logging.</param>

        public UserProfileController(IUserProfileService userProfileService, ILogger<UserProfileController> logger)
        {
            _userProfileService = userProfileService;
            _logger = logger;
        }
        /// <summary>
        /// Adds a new user profile.
        /// </summary>
        /// <param name="userProfileDTO">The user profile data to add.</param>
        /// <returns>The result of the add operation.</returns>

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
        /// <summary>
        /// Removes a user profile by username.
        /// </summary>
        /// <param name="username">The username of the user profile to remove.</param>
        /// <returns>The result of the removal operation.</returns>

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
        /// <summary>
        /// Updates an existing user profile.
        /// </summary>
        /// <param name="userProfileDTO">The user profile data to update.</param>
        /// <returns>The result of the update operation.</returns>

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
        /// <summary>
        /// Gets a user profile by ID.
        /// </summary>
        /// <param name="userId">The ID of the user profile to retrieve.</param>
        /// <returns>The user profile with the given ID.</returns>

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
        /// <summary>
        /// Gets a user profile by username.
        /// </summary>
        /// <param name="username">The username of the user profile to retrieve.</param>
        /// <returns>The user profile with the given username.</returns>

        [HttpGet("username/{username}")]
        public IActionResult GetUserProfileByUsername(string username)
        {
            _logger.LogInformation($"Getting user profile by username: {username}.");

            try
            {
                var userProfileDTO = _userProfileService.GetUserProfileByUsername(username);

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
        /// <summary>
        /// Gets all user profiles.
        /// </summary>
        /// <returns>A list of all user profiles.</returns>

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
