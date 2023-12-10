using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ReimbursementTrackerApp.Tests
{
    [TestFixture]
    public class TrackingServiceTests
    {
        private TrackingService trackingService;
        private TrackingRepository trackingRepository;
        private RequestService requestService;
        private RequestRepository requestRepository;
        private IWebHostEnvironment hostingEnvironment;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<RTAppContext>()
                                .UseInMemoryDatabase("dbTestTracking")
                                .Options;

            RTAppContext context = new RTAppContext(dbOptions);
            trackingRepository = new TrackingRepository(context);
            trackingService = new TrackingService(trackingRepository, requestRepository);

            requestRepository = new RequestRepository(context);
            requestService = new RequestService(requestRepository, trackingRepository, hostingEnvironment);
        }

        [Test]
        public void AddTracking_Success()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Fake document")), 0, 0, "Document", "document.txt"),
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Add the request
            requestService.Add(requestDTO);

            var trackingDTO = new TrackingDTO
            {
                RequestId = 1,
                TrackingStatus = "Pending",
                ApprovalDate = null,
                ReimbursementDate = null
            };

            // Act
            var isAdded = trackingService.Add(trackingDTO);

            // Assert
            Assert.IsTrue(isAdded, "Failed to add tracking information.");
        }

        [Test]
        public void AddTracking_RequestNotFound_ThrowsException()
        {
            // Arrange
            var trackingDTO = new TrackingDTO
            {
                RequestId = 999, // Assuming this ID doesn't exist
                TrackingStatus = "Approved",
                ApprovalDate = DateTime.Now,
                ReimbursementDate = DateTime.Now.AddDays(1)
            };

            // Act & Assert
            Assert.Throws<ServiceException>(() => trackingService.Add(trackingDTO));
        }

        [Test]
        public void RemoveTracking_Success()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Fake document")), 0, 0, "Document", "document.txt"),
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Add the request
            requestService.Add(requestDTO);

            var trackingDTO = new TrackingDTO
            {
                RequestId = 1,
                TrackingStatus = "Approved",
                ApprovalDate = DateTime.Now,
                ReimbursementDate = DateTime.Now.AddDays(1)
            };

            // Add the tracking information
            trackingService.Add(trackingDTO);

            // Act
            var isRemoved = trackingService.Remove(1);

            // Assert
            Assert.IsTrue(isRemoved, "Failed to remove tracking information.");
        }

        [Test]
        public void RemoveTracking_NotFound_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ServiceException>(() => trackingService.Remove(999));
        }

        [Test]
        public void UpdateTracking_Success()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Fake document")), 0, 0, "Document", "document.txt"),
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Add the request
            requestService.Add(requestDTO);

            var trackingDTO = new TrackingDTO
            {
                TrackingId = 1,
                RequestId = 1,
                TrackingStatus = "Pending",
                ApprovalDate = DateTime.Now,
                ReimbursementDate = null
            };

            // Add the tracking information
            trackingService.Add(trackingDTO);

            var updatedTrackingDTO = new TrackingDTO
            {
                TrackingId = 1,
                RequestId = 1,
                TrackingStatus = "Approved",
                ApprovalDate = DateTime.Now,
                ReimbursementDate = DateTime.Now.AddDays(1)
            };

            // Act
            var updatedTracking = trackingService.Update(updatedTrackingDTO);

            // Assert
            Assert.AreEqual("Approved", updatedTracking.TrackingStatus);
        }

        [Test]
        public void UpdateTracking_NotFound_ThrowsException()
        {
            // Arrange
            var trackingDTO = new TrackingDTO
            {   TrackingId = 1,
                RequestId = 1, // Assuming this ID doesn't exist
                TrackingStatus = "Pending",
                ApprovalDate = DateTime.Now,
                ReimbursementDate = null
            };

            // Act & Assert
            Assert.Throws<ServiceException>(() => trackingService.Update(trackingDTO));
        }

        [Test]
        public void GetTrackingByRequestId_Success()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Fake document")), 0, 0, "Document", "document.txt"),
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Add the request
            requestService.Add(requestDTO);

            var trackingDTO = new TrackingDTO
            {
                RequestId = 1,
                TrackingStatus = "Pending",
                ApprovalDate = DateTime.Now,
                ReimbursementDate = DateTime.Now.AddDays(1)
            };

            // Add the tracking information
            trackingService.Add(trackingDTO);

            // Act
            var retrievedTracking = trackingService.GetTrackingByRequestId(1);

            // Assert
            Assert.IsNotNull(retrievedTracking, "Failed to retrieve tracking information.");
            Assert.AreEqual("Pending", retrievedTracking.TrackingStatus);
        }

        [Test]
        public void GetTrackingByRequestId_NotFound_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ServiceException>(() => trackingService.GetTrackingByRequestId(999));
        }

        [Test]
        public void GetTrackingByTrackingId_Success()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Fake document")), 0, 0, "Document", "document.txt"),
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Add the request
            requestService.Add(requestDTO);

            var trackingDTO = new TrackingDTO
            {   TrackingId = 1,
                RequestId = 1,
                TrackingStatus = "Pending",
                ApprovalDate = DateTime.Now,
                ReimbursementDate = DateTime.Now.AddDays(1)
            };

            // Add the tracking information
            trackingService.Add(trackingDTO);

            // Act
            var retrievedTracking = trackingService.GetTrackingByTrackingId(1);

            // Assert
            Assert.IsNotNull(retrievedTracking, "Failed to retrieve tracking information.");
            Assert.AreEqual("Pending", retrievedTracking.TrackingStatus);
        }

        [Test]
        public void GetTrackingByTrackingId_NotFound_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ServiceException>(() => trackingService.GetTrackingByTrackingId(999));
        }

        [Test]
        public void GetAllTrackings_Success()
        {
            // Arrange
            var requestDTO1 = new RequestDTO
            {
                RequestId = 1,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Fake document")), 0, 0, "Document", "document.txt"),
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            var requestDTO2 = new RequestDTO
            {
                RequestId = 2,
                Username = "testUser",
                ExpenseCategory = "Office Supplies",
                Amount = 50.0f,
                Document = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Fake document")), 0, 0, "Document", "document.txt"),
                Description = "Office supplies reimbursement",
                RequestDate = DateTime.Now
            };

            // Add the requests
            requestService.Add(requestDTO1);
            requestService.Add(requestDTO2);

            // Act
            var allTrackings = trackingService.GetAllTrackings();

            // Assert
            Assert.AreEqual(2, allTrackings.Count());
        }
    }
}
