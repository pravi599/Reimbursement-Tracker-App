using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Exceptions;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Models.DTOs;
using ReimbursementTrackerApp.Repositories;
using ReimbursementTrackerApp.Services;

namespace ReimbursementTrackerApp.Tests
{
    [TestFixture]
    public class PaymentDetailsServiceTests
    {
        private PaymentDetailsService _paymentDetailsService;
        private DbContextOptions<RTAppContext> _dbContextOptions;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<RTAppContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique database name
                .Options;

            var dbContext = new RTAppContext(_dbContextOptions);
            var paymentDetailsRepository = new PaymentDetailsRepository(dbContext);
            _paymentDetailsService = new PaymentDetailsService(paymentDetailsRepository);
        }

        [Test]
        public void Add_PaymentDetails_Successful()
        {
            // Arrange
            var paymentDetailsDTO = new PaymentDetailsDTO
            {
                PaymentId = 1,
                RequestId = 100,
                PaymentAmount = 5000,
                PaymentDate = DateTime.Now,
                CardNumber = "1234567812345678",
                ExpiryDate = "12/25",
                CVV = "123"
            };

            // Act
            var result = _paymentDetailsService.Add(paymentDetailsDTO);

            // Assert
            Assert.IsTrue(result);

            // Validate the state of the database after the addition
            using (var context = new RTAppContext(_dbContextOptions))
            {
                var paymentDetailsInDatabase = context.PaymentDetails.First();
                Assert.AreEqual(1, context.PaymentDetails.Count());
                Assert.AreEqual(paymentDetailsDTO.PaymentId, paymentDetailsInDatabase.PaymentId);
                Assert.AreEqual(paymentDetailsDTO.RequestId, paymentDetailsInDatabase.RequestId);
                Assert.AreEqual(paymentDetailsDTO.PaymentAmount, paymentDetailsInDatabase.PaymentAmount);
                Assert.AreEqual(paymentDetailsDTO.PaymentDate, paymentDetailsInDatabase.PaymentDate);
                Assert.AreEqual(paymentDetailsDTO.CardNumber, paymentDetailsInDatabase.CardNumber);
                Assert.AreEqual(paymentDetailsDTO.ExpiryDate, paymentDetailsInDatabase.ExpiryDate);
                Assert.AreEqual(paymentDetailsDTO.CVV, paymentDetailsInDatabase.CVV);
            }
        }

        [Test]
        public void Add_PaymentDetails_PaymentDetailsAlreadyExistsException()
        {
            // Arrange
            var paymentDetailsDTO = new PaymentDetailsDTO
            {
                PaymentId = 1,
                RequestId = 100,
                PaymentAmount = 5000,
                PaymentDate = DateTime.Now,
                CardNumber = "1234567812345678",
                ExpiryDate = "12/25",
                CVV = "123"
            };

            // Act
            _paymentDetailsService.Add(paymentDetailsDTO);

            // Assert
            Assert.Throws<PaymentDetailsAlreadyExistsException>(() => _paymentDetailsService.Add(paymentDetailsDTO));
        }

        // Add similar tests for Remove, Update, GetPaymentDetailsById, and GetAllPaymentDetails
        // ...

        [TearDown]
        public void TearDown()
        {
            // Clean up the database after each test
            using (var context = new RTAppContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}