// PaymentDetailsService.cs
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Models.DTOs;
using ReimbursementTrackerApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReimbursementTrackerApp.Services
{
    /// <summary>
    /// Service class for managing payment details related to reimbursement requests.
    /// </summary>
    public class PaymentDetailsService : IPaymentDetailsService
    {
        private readonly IRepository<int, PaymentDetails> _paymentDetailsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentDetailsService"/> class.
        /// </summary>
        /// <param name="paymentDetailsRepository">The repository for payment details.</param>
        public PaymentDetailsService(IRepository<int, PaymentDetails> paymentDetailsRepository)
        {
            _paymentDetailsRepository = paymentDetailsRepository;
        }

        /// <summary>
        /// Adds a new payment detail entry.
        /// </summary>
        /// <param name="paymentDetailsDTO">The payment details to be added.</param>
        /// <returns>Returns true if the addition is successful; otherwise, throws a PaymentDetailsAlreadyExistsException.</returns>
        public bool Add(PaymentDetailsDTO paymentDetailsDTO)
        {
            var existingPaymentDetails = _paymentDetailsRepository.GetAll()
                .FirstOrDefault(pd => pd.PaymentId == paymentDetailsDTO.PaymentId);

            if (existingPaymentDetails == null)
            {
                var paymentDetails = new PaymentDetails
                {
                    RequestId = paymentDetailsDTO.RequestId,
                    PaymentAmount = paymentDetailsDTO.PaymentAmount,
                    PaymentDate = paymentDetailsDTO.PaymentDate,
                    CardNumber = paymentDetailsDTO.CardNumber,
                    ExpiryDate = paymentDetailsDTO.ExpiryDate,
                    CVV = paymentDetailsDTO.CVV
                };

                _paymentDetailsRepository.Add(paymentDetails);

                return true;
            }

            throw new PaymentDetailsAlreadyExistsException();
        }

        /// <summary>
        /// Removes a payment detail entry based on the payment ID.
        /// </summary>
        /// <param name="paymentId">The ID of the payment details to be removed.</param>
        /// <returns>Returns true if the removal is successful; otherwise, throws a PaymentDetailsNotFoundException.</returns>
        public bool Remove(int paymentId)
        {
            var paymentDetails = _paymentDetailsRepository.Delete(paymentId);

            if (paymentDetails != null)
            {
                return true;
            }

            throw new PaymentDetailsNotFoundException();
        }

        /// <summary>
        /// Updates a payment detail entry based on the provided PaymentDetailsDTO.
        /// </summary>
        /// <param name="paymentDetailsDTO">Updated payment details information.</param>
        /// <returns>Returns the updated PaymentDetailsDTO if the update is successful; otherwise, throws a PaymentDetailsNotFoundException.</returns>
        public PaymentDetailsDTO Update(PaymentDetailsDTO paymentDetailsDTO)
        {
            var existingPaymentDetails = _paymentDetailsRepository.GetById(paymentDetailsDTO.PaymentId);

            if (existingPaymentDetails != null)
            {
                existingPaymentDetails.RequestId = paymentDetailsDTO.RequestId;
                existingPaymentDetails.PaymentAmount = paymentDetailsDTO.PaymentAmount;
                existingPaymentDetails.PaymentDate = paymentDetailsDTO.PaymentDate;
                existingPaymentDetails.CardNumber = paymentDetailsDTO.CardNumber;
                existingPaymentDetails.ExpiryDate = paymentDetailsDTO.ExpiryDate;
                existingPaymentDetails.CVV = paymentDetailsDTO.CVV;

                _paymentDetailsRepository.Update(existingPaymentDetails);

                return new PaymentDetailsDTO
                {
                    PaymentId = existingPaymentDetails.PaymentId,
                    RequestId = existingPaymentDetails.RequestId,
                    PaymentAmount = existingPaymentDetails.PaymentAmount,
                    CardNumber = existingPaymentDetails.CardNumber,
                    ExpiryDate = existingPaymentDetails.ExpiryDate,
                    CVV = existingPaymentDetails.CVV,
                    PaymentDate = existingPaymentDetails.PaymentDate
                };
            }

            throw new PaymentDetailsNotFoundException();
        }

        /// <summary>
        /// Gets payment details based on the payment ID.
        /// </summary>
        /// <param name="paymentId">The ID of the payment details to retrieve.</param>
        /// <returns>Returns the PaymentDetailsDTO if the payment details are found; otherwise, throws a PaymentDetailsNotFoundException.</returns>
        public PaymentDetailsDTO GetPaymentDetailsById(int paymentId)
        {
            var paymentDetails = _paymentDetailsRepository.GetById(paymentId);

            if (paymentDetails != null)
            {
                return new PaymentDetailsDTO
                {
                    PaymentId = paymentDetails.PaymentId,
                    RequestId = paymentDetails.RequestId,
                    PaymentAmount = paymentDetails.PaymentAmount,
                    CardNumber = paymentDetails.CardNumber,
                    ExpiryDate = paymentDetails.ExpiryDate,
                    CVV = paymentDetails.CVV,
                    PaymentDate = paymentDetails.PaymentDate
                };
            }

            throw new PaymentDetailsNotFoundException();
        }

        /// <summary>
        /// Gets all payment details.
        /// </summary>
        /// <returns>Returns a collection of PaymentDetailsDTO representing all payment details.</returns>
        public IEnumerable<PaymentDetailsDTO> GetAllPaymentDetails()
        {
            var paymentDetails = _paymentDetailsRepository.GetAll();

            return paymentDetails.Select(pd => new PaymentDetailsDTO
            {
                PaymentId = pd.PaymentId,
                RequestId = pd.RequestId,
                PaymentAmount = pd.PaymentAmount,
                CardNumber = pd.CardNumber,
                ExpiryDate = pd.ExpiryDate,
                CVV = pd.CVV,
                PaymentDate = pd.PaymentDate
            });
        }
    }
}