using Humanizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using System.Text;

namespace ReimbursementTrackerApp.Tests
{
    [TestFixture]
    public class RequestServiceTests
    {
        private RequestService requestService;
        private RequestRepository requestRepository;
        private TrackingRepository trackingRepository;
        private IWebHostEnvironment hostingEnvironment;


        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<RTAppContext>()
                                .UseInMemoryDatabase("dbTestRequestService")
                                .Options;

            RTAppContext context = new RTAppContext(dbOptions);
            requestRepository = new RequestRepository(context);
            trackingRepository = new TrackingRepository(context);

            requestService = new RequestService(requestRepository, trackingRepository, hostingEnvironment);
        }

        [Test]
        public void AddRequest_Success()
        {
            // Arrange

            var requestDTO = new RequestDTO
            {
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Fake document")), 0, 0, "Document", "document.txt"),
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Act
            var result = requestService.Add(requestDTO);

            // Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void RemoveRequest_Success()
        {
            // Arrange

            var requestDTO = new RequestDTO
            {
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Add the request
            requestService.Add(requestDTO);

            // Act
            var result = requestService.Remove(1);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveRequest_NotFound_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<RequestNotFoundException>(() => requestService.Remove(999));
        }

        [Test]
        public void UpdateRequest_Success()
        {
            // Arrange

            var requestDTO = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            var updatedRequestDTO = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Electronics",
                Amount = 75.0f,
                Description = "Updated reimbursement request",
                RequestDate = DateTime.Now.AddDays(1)
            };

            // Add the request
            requestService.Add(requestDTO);

            // Act
            var updatedRequest = requestService.Update(updatedRequestDTO);

            // Assert
            Assert.AreEqual("Electronics", updatedRequest.ExpenseCategory);
            Assert.AreEqual(75.0f, updatedRequest.Amount);
            Assert.AreEqual("Updated reimbursement request", updatedRequest.Description);
        }

        [Test]
        public void UpdateRequest_NotFound_ThrowsException()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Act & Assert
            Assert.Throws<RequestNotFoundException>(() => requestService.Update(requestDTO));
        }

        [Test]
        public void GetRequestById_Success()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Add the request
            requestService.Add(requestDTO);

            // Act
            var retrievedRequest = requestService.GetRequestById(1);

            // Assert
            Assert.IsNotNull(retrievedRequest, "Failed to retrieve request details.");
            Assert.AreEqual("Office Supplies", retrievedRequest.ExpenseCategory);
        }

        [Test]
        public void GetRequestById_NotFound_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<RequestNotFoundException>(() => requestService.GetRequestById(999));
        }

        [Test]
        public void GetRequestsByCategory_Success()
        {
            // Arrange

            var requestDTO = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Add the request
            requestService.Add(requestDTO);

            // Act
            var retrievedRequest = requestService.GetRequestsByCategory("Office Supplies");

            // Assert
            Assert.IsNotNull(retrievedRequest, "Failed to retrieve request details.");
            Assert.AreEqual("Office Supplies", retrievedRequest.ExpenseCategory);
        }

        [Test]
        public void GetRequestsByCategory_NotFound_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<RequestNotFoundException>(() => requestService.GetRequestsByCategory("NonexistentCategory"));
        }

        [Test]
        public void GetRequestsByUsername_Success()
        {
            // Arrange

            var requestDTO = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Add the request
            requestService.Add(requestDTO);

            // Act
            var retrievedRequests = requestService.GetRequestsByUsername("testUser");

            // Assert
            Assert.IsNotNull(retrievedRequests, "Failed to retrieve request details.");
            Assert.AreEqual(1, retrievedRequests.Count());
        }


        [Test]
        public void GetAllRequests_Success()
        {
            // Arrange

            var requestDTO1 = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            var requestDTO2 = new RequestDTO
            {
                RequestId = 2,
                Username = "testUser",
                ExpenseCategory = "Electronics",
                Amount = 75.0f,
                Description = "Electronics reimbursement",
                RequestDate = DateTime.Now.AddDays(1)
            };

            // Add the requests
            requestService.Add(requestDTO1);
            requestService.Add(requestDTO2);

            // Act
            var allRequests = requestService.GetAllRequests();

            // Assert
            Assert.AreEqual(2, allRequests.Count());
        }
    }
}

