using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Exceptions;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Models.DTOs;
using ReimbursementTrackerApp.Repositories;
using ReimbursementTrackerApp.Services;
using System;
using System.Linq;

namespace ReimbursementTrackerApp.Tests
{
    [TestFixture]
    public class PaymentDetailsServiceTests
    {
        private PaymentDetailsService paymentDetailsService;
        private PaymentDetailsRepository paymentDetailsRepository;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<RTAppContext>()
                                .UseInMemoryDatabase("dbTestPaymentDetails")
                                .Options;

            RTAppContext context = new RTAppContext(dbOptions);
            paymentDetailsRepository = new PaymentDetailsRepository(context);
            paymentDetailsService = new PaymentDetailsService(paymentDetailsRepository);
        }

        [Test]
        public void AddPaymentDetails_Success()
        {
            // Arrange
            var paymentDetailsDTO = new PaymentDetailsDTO
            {
                RequestId = 1,
                PaymentId = 1,
                CardNumber = "1234567812345678",
                ExpiryDate = "12/25",
                CVV = "123",
                PaymentAmount = 100.00f,
                PaymentDate = DateTime.Now
            };

            // Act
            var result = paymentDetailsService.Add(paymentDetailsDTO);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddPaymentDetails_DuplicatePaymentId_ThrowsException()
        {
            // Arrange
            var paymentDetailsDTO = new PaymentDetailsDTO
            {
                RequestId = 1,
                PaymentId = 1,
                CardNumber = "1234567812345678",
                ExpiryDate = "12/25",
                CVV = "123",
                PaymentAmount = 100.00f,
                PaymentDate = DateTime.Now
            };

            // Add the first payment details
            paymentDetailsService.Add(paymentDetailsDTO);

            // Act & Assert
            Assert.Throws<PaymentDetailsAlreadyExistsException>(() => paymentDetailsService.Add(paymentDetailsDTO));
        }

        [Test]
        public void RemovePaymentDetails_Success()
        {
            // Arrange
            var paymentDetailsDTO = new PaymentDetailsDTO
            {
                RequestId = 1,
                PaymentId = 1,
                CardNumber = "1234567812345678",
                ExpiryDate = "12/25",
                CVV = "123",
                PaymentAmount = 100.00f,
                PaymentDate = DateTime.Now
            };

            // Add the payment details
            paymentDetailsService.Add(paymentDetailsDTO);

            // Act
            var result = paymentDetailsService.Remove(1);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void RemovePaymentDetails_NotFound_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<PaymentDetailsNotFoundException>(() => paymentDetailsService.Remove(999));
        }

        [Test]
        public void UpdatePaymentDetails_Success()
        {
            // Arrange
            var paymentDetailsDTO = new PaymentDetailsDTO
            {
                RequestId = 1,
                PaymentId = 1,
                CardNumber = "1234567812345678",
                ExpiryDate = "12/25",
                CVV = "123",
                PaymentAmount = 100.00f,
                PaymentDate = DateTime.Now
            };

            var updatedPaymentDetailsDTO = new PaymentDetailsDTO
            {
                RequestId = 1,
                PaymentId = 1,
                CardNumber = "5678123456781234",
                ExpiryDate = "01/26",
                CVV = "456",
                PaymentAmount = 150.00f,
                PaymentDate = DateTime.Now.AddDays(1)
            };

            // Add the payment details
            paymentDetailsService.Add(paymentDetailsDTO);

            // Act
            var updatedPaymentDetails = paymentDetailsService.Update(updatedPaymentDetailsDTO);

            // Assert
            Assert.AreEqual(150.00f, updatedPaymentDetails.PaymentAmount);
            Assert.AreEqual("5678123456781234", updatedPaymentDetails.CardNumber);
        }

        [Test]
        public void UpdatePaymentDetails_NotFound_ThrowsException()
        {
            // Arrange
            var paymentDetailsDTO = new PaymentDetailsDTO
            {
                RequestId = 1,
                PaymentId = 1,
                CardNumber = "1234567812345678",
                ExpiryDate = "12/25",
                CVV = "123",
                PaymentAmount = 100.00f,
                PaymentDate = DateTime.Now
            };

            // Act & Assert
            Assert.Throws<PaymentDetailsNotFoundException>(() => paymentDetailsService.Update(paymentDetailsDTO));
        }

        [Test]
        public void GetPaymentDetailsById_Success()
        {
            // Arrange
            var paymentDetailsDTO = new PaymentDetailsDTO
            {
                RequestId = 1,
                PaymentId = 1,
                CardNumber = "1234567812345678",
                ExpiryDate = "12/25",
                CVV = "123",
                PaymentAmount = 100.00f,
                PaymentDate = DateTime.Now
            };

            // Add the payment details
            paymentDetailsService.Add(paymentDetailsDTO);

            // Act
            var retrievedPaymentDetails = paymentDetailsService.GetPaymentDetailsById(paymentDetailsDTO.PaymentId);

            // Assert
            Assert.IsNotNull(retrievedPaymentDetails, "Failed to retrieve payment details.");
            Assert.AreEqual("1234567812345678", retrievedPaymentDetails.CardNumber);
        }



        [Test]
        public void GetPaymentDetailsById_NotFound_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<PaymentDetailsNotFoundException>(() => paymentDetailsService.GetPaymentDetailsById(999));
        }

        [Test]
        public void GetAllPaymentDetails_Success()
        {
            // Arrange
            var paymentDetailsDTO1 = new PaymentDetailsDTO
            {
                RequestId = 1,
                PaymentId = 1,
                CardNumber = "1234567812345678",
                ExpiryDate = "12/25",
                CVV = "123",
                PaymentAmount = 100.00f,
                PaymentDate = DateTime.Now
            };

            var paymentDetailsDTO2 = new PaymentDetailsDTO
            {
                RequestId = 2,
                PaymentId = 2,
                CardNumber = "5678123456781234",
                ExpiryDate = "01/26",
                CVV = "456",
                PaymentAmount = 150.00f,
                PaymentDate = DateTime.Now.AddDays(1)
            };

            // Add the payment details
            paymentDetailsService.Add(paymentDetailsDTO1);
            paymentDetailsService.Add(paymentDetailsDTO2);

            // Act
            var allPaymentDetails = paymentDetailsService.GetAllPaymentDetails();

            // Assert
            Assert.AreEqual(2, allPaymentDetails.Count());
        }
    }
}
