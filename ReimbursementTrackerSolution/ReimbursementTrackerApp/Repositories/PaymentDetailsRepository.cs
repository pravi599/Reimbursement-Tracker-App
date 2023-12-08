using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Models;
using System.Collections.Generic;
using System.Linq;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Interfaces;

namespace ReimbursementTrackerApp.Repositories
{
    /// <summary>
    /// Repository for managing PaymentDetails entities using Entity Framework Core.
    /// </summary>
    public class PaymentDetailsRepository : IRepository<int, PaymentDetails>
    {
        private readonly RTAppContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentDetailsRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PaymentDetailsRepository(RTAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a payment details entity from the database based on the payment ID.
        /// </summary>
        /// <param name="paymentId">The ID of the payment details entity to be retrieved.</param>
        /// <returns>Returns the payment details entity if found; otherwise, returns null.</returns>
        public PaymentDetails GetById(int paymentId)
        {
            return _context.PaymentDetails
                .Include(pd => pd.Request)
                .FirstOrDefault(pd => pd.PaymentId == paymentId);
        }

        /// <summary>
        /// Retrieves all payment details entities from the database.
        /// </summary>
        /// <returns>Returns a list of payment details entities.</returns>
        public IList<PaymentDetails> GetAll()
        {
            return _context.PaymentDetails
                .Include(pd => pd.Request)
                .ToList();
        }

        /// <summary>
        /// Adds a new payment details entity to the database.
        /// </summary>
        /// <param name="paymentDetails">The payment details entity to be added.</param>
        /// <returns>Returns the added payment details entity.</returns>
        public PaymentDetails Add(PaymentDetails paymentDetails)
        {
            _context.PaymentDetails.Add(paymentDetails);
            _context.SaveChanges();
            return paymentDetails;
        }

        /// <summary>
        /// Updates a payment details entity in the database.
        /// </summary>
        /// <param name="paymentDetails">The updated payment details entity.</param>
        /// <returns>Returns the updated payment details entity.</returns>
        public PaymentDetails Update(PaymentDetails paymentDetails)
        {
            _context.PaymentDetails.Update(paymentDetails);
            _context.SaveChanges();
            return paymentDetails;
        }

        /// <summary>
        /// Deletes a payment details entity from the database based on the payment ID.
        /// </summary>
        /// <param name="paymentId">The ID of the payment details entity to be deleted.</param>
        /// <returns>Returns the deleted payment details entity if found; otherwise, returns null.</returns>
        public PaymentDetails Delete(int paymentId)
        {
            var paymentDetails = _context.PaymentDetails.Find(paymentId);
            if (paymentDetails != null)
            {
                _context.PaymentDetails.Remove(paymentDetails);
                _context.SaveChanges();
            }
            return paymentDetails;
        }
    }
}