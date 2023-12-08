using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models.DTOs;

namespace ReimbursementTrackerApp.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations through RESTful API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("reactApp")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The service for managing user-related operations.</param>

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="viewModel">The data for user registration.</param>
        /// <returns>The result of the registration operation.</returns>

        [HttpPost]
        public ActionResult Register(UserDTO viewModel)
        {
            string message = "";
            try
            {
                var user = _userService.Register(viewModel);
                if (user != null)
                {
                    return Ok(user);
                }
            }
            catch (DbUpdateException exp)
            {
                message = "Duplicate username";
            }
            catch (Exception)
            {

            }


            return BadRequest(message);
        }
        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="userDTO">The data for user login.</param>
        /// <returns>The result of the login operation.</returns>

        [HttpPost]
        [Route("Login")]//attribute based routing
        public ActionResult Login(UserDTO userDTO)
        {
            var result = _userService.Login(userDTO);
            if (result != null)
            {
                return Ok(result);
            }
            return Unauthorized("Invalid username or password");
        }
    }

}
