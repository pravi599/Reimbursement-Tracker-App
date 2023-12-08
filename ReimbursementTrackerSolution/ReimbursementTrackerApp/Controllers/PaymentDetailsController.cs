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
    /// API controller for managing payment details.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("reactApp")]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly IPaymentDetailsService _paymentDetailsService;
        private readonly ILogger<PaymentDetailsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentDetailsController"/> class.
        /// </summary>
        /// <param name="paymentDetailsService">The payment details service.</param>
        /// <param name="logger">The logger.</param>
        public PaymentDetailsController(IPaymentDetailsService paymentDetailsService, ILogger<PaymentDetailsController> logger)
        {
            _paymentDetailsService = paymentDetailsService;
            _logger = logger;
        }

        /// <summary>
        /// Adds payment details.
        /// </summary>
        /// <param name="paymentDetailsDTO">The payment details DTO.</param>
        /// <returns>Returns the result of the operation.</returns>
        [HttpPost]
        public IActionResult AddPaymentDetails([FromBody] PaymentDetailsDTO paymentDetailsDTO)
        {
            _logger.LogInformation($"Adding payment details for Request ID {paymentDetailsDTO.RequestId}.");

            try
            {
                var result = _paymentDetailsService.Add(paymentDetailsDTO);
                return Ok(result);
            }
            catch (PaymentDetailsAlreadyExistsException ex)
            {
                _logger.LogError(ex, $"Failed to add payment details. {ex.Message}");
                return Conflict($"Failed to add payment details. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding payment details.");
                return BadRequest("Failed to add payment details");
            }
        }

        /// <summary>
        /// Removes payment details.
        /// </summary>
        /// <param name="paymentId">The ID of the payment details to be removed.</param>
        /// <returns>Returns the result of the operation.</returns>
        [HttpDelete("{paymentId}")]
        public ActionResult RemovePaymentDetails(int paymentId)
        {
            _logger.LogInformation($"Removing payment details with ID {paymentId}.");

            try
            {
                var success = _paymentDetailsService.Remove(paymentId);

                if (success)
                {
                    _logger.LogInformation("Payment details deleted");
                    return Ok("Payment details deleted successfully");
                }

                return NotFound("Payment details not found");
            }
            catch (PaymentDetailsNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to remove payment details.");
                return NotFound($"Failed to remove payment details. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing payment details.");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates payment details.
        /// </summary>
        /// <param name="paymentDetailsDTO">The updated payment details DTO.</param>
        /// <returns>Returns the result of the operation.</returns>
        [HttpPut]
        public IActionResult UpdatePaymentDetails([FromBody] PaymentDetailsDTO paymentDetailsDTO)
        {
            _logger.LogInformation($"Updating payment details for ID {paymentDetailsDTO.PaymentId}.");

            try
            {
                var result = _paymentDetailsService.Update(paymentDetailsDTO);

                if (result != null)
                {
                    _logger.LogInformation("Payment details updated successfully");
                    return Ok(result);
                }

                return NotFound("Payment details not found");
            }
            catch (PaymentDetailsNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to update payment details.");
                return NotFound($"Failed to update payment details. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payment details.");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets payment details by ID.
        /// </summary>
        /// <param name="paymentId">The ID of the payment details to be retrieved.</param>
        /// <returns>Returns the payment details DTO if found; otherwise, returns an error message.</returns>
        [HttpGet("{paymentId}")]
        public IActionResult GetPaymentDetailsById(int paymentId)
        {
            _logger.LogInformation($"Getting payment details with ID {paymentId}.");

            try
            {
                var paymentDetailsDTO = _paymentDetailsService.GetPaymentDetailsById(paymentId);

                if (paymentDetailsDTO != null)
                {
                    _logger.LogInformation("Payment details listed with given ID");
                    return Ok(paymentDetailsDTO);
                }

                return NotFound("Payment details not found");
            }
            catch (PaymentDetailsNotFoundException ex)
            {
                _logger.LogError(ex, $"Failed to get payment details by ID.");
                return NotFound($"Failed to get payment details by ID. {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting payment details by ID.");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets all payment details.
        /// </summary>
        /// <returns>Returns a list of payment details DTOs if available; otherwise, returns an error message.</returns>
        [HttpGet]
        public IActionResult GetAllPaymentDetails()
        {
            _logger.LogInformation("Getting all payment details.");

            try
            {
                var paymentDetailsDTOs = _paymentDetailsService.GetAllPaymentDetails();
                _logger.LogInformation("All payment details listed");
                return Ok(paymentDetailsDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all payment details.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}