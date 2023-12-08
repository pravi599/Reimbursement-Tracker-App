using ReimbursementTrackerApp.Models.DTOs;

namespace ReimbursementTrackerApp.Interfaces
{
    /// <summary>
    /// Interface for payment details service, providing methods for CRUD operations on payment details.
    /// </summary>
    public interface IPaymentDetailsService
    {
        /// <summary>
        /// Adds a new payment details entry.
        /// </summary>
        /// <param name="paymentDetailsDTO">The payment details DTO to be added.</param>
        /// <returns>True if the addition was successful, false otherwise.</returns>
        bool Add(PaymentDetailsDTO paymentDetailsDTO);

        /// <summary>
        /// Removes a payment details entry by its ID.
        /// </summary>
        /// <param name="paymentId">The ID of the payment details to be removed.</param>
        /// <returns>True if the removal was successful, false otherwise.</returns>
        bool Remove(int paymentId);

        /// <summary>
        /// Updates an existing payment details entry.
        /// </summary>
        /// <param name="paymentDetailsDTO">The payment details DTO to be updated.</param>
        /// <returns>The updated payment details DTO.</returns>
        PaymentDetailsDTO Update(PaymentDetailsDTO paymentDetailsDTO);

        /// <summary>
        /// Gets payment details by its ID.
        /// </summary>
        /// <param name="paymentId">The ID of the payment details to retrieve.</param>
        /// <returns>The payment details DTO with the specified ID, or null if not found.</returns>
        PaymentDetailsDTO GetPaymentDetailsById(int paymentId);

        /// <summary>
        /// Gets all payment details.
        /// </summary>
        /// <returns>An enumerable of payment details DTOs.</returns>
        IEnumerable<PaymentDetailsDTO> GetAllPaymentDetails();
    }
}