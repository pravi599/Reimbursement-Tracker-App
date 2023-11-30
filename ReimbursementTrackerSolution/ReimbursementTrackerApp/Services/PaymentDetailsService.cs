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
    public class PaymentDetailsService : IPaymentDetailsService
    {
        private readonly IRepository<int, PaymentDetails> _paymentDetailsRepository;

        public PaymentDetailsService(IRepository<int, PaymentDetails> paymentDetailsRepository)
        {
            _paymentDetailsRepository = paymentDetailsRepository;
        }

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
                    PaymentDate = paymentDetailsDTO.PaymentDate
                };

                _paymentDetailsRepository.Add(paymentDetails);

                return true;
            }

            throw new PaymentDetailsAlreadyExistsException();
        }

        public bool Remove(int paymentId)
        {
            var paymentDetails = _paymentDetailsRepository.Delete(paymentId);

            if (paymentDetails != null)
            {
                return true;
            }

            throw new PaymentDetailsNotFoundException();
        }

        public PaymentDetailsDTO Update(PaymentDetailsDTO paymentDetailsDTO)
        {
            var existingPaymentDetails = _paymentDetailsRepository.GetById(paymentDetailsDTO.PaymentId);

            if (existingPaymentDetails != null)
            {
                existingPaymentDetails.RequestId = paymentDetailsDTO.RequestId;
                existingPaymentDetails.PaymentAmount = paymentDetailsDTO.PaymentAmount;
                existingPaymentDetails.PaymentDate = paymentDetailsDTO.PaymentDate;

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

