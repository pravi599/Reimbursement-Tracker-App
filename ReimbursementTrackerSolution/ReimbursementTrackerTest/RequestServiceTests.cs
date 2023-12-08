/*using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
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
    public class RequestServiceTests
    {
        private IRequestService _requestService;
        private IRepository<int,Request> _repository;
        private IRepository<int,Tracking> _trackingRepository;
        private IRepository<string,User> _userRepository;
        private RTAppContext _dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RTAppContext>()
                .UseInMemoryDatabase(databaseName: "RTAppContext")
                .Options;

            _dbContext = new RTAppContext(options);

            _repository = new RequestRepository(_dbContext);
            _trackingRepository = new TrackingRepository(_dbContext); // Assuming Tracking uses int as the key
            _userRepository = new UserRepository(_dbContext);

            _requestService = new RequestService(_repository, _trackingRepository, _userRepository);
        }

        [Test]
        public void Add_RequestDTOIsValid_ShouldAddRequestAndTracking()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                ExpenseCategory = "Category",
                Amount = 100,
                Document = "Document",
                Description = "Description",
                Username = "User1"
            };

            // Act
            var result = _requestService.Add(requestDTO);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _requestService.GetAllRequests().Count());
            Assert.AreEqual(1, _requestService.GetRequestsByUsername("User1").Count());
        }

        [Test]
        public void Add_UserNotFound_ShouldThrowUserNotFoundException()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                Username = "NonExistingUser"
            };

            // Act & Assert
            Assert.Throws<UserNotFoundException>(() => _requestService.Add(requestDTO));
        }

        [Test]
        public void Remove_RequestIdExists_ShouldRemoveRequest()
        {
            // Arrange
            var requestId = 1;
            var requestDTO = new RequestDTO
            {
                ExpenseCategory = "Category",
                Amount = 100,
                Document = "Document",
                Description = "Description",
                Username = "User1"
            };

            _requestService.Add(requestDTO);

            // Act
            var result = _requestService.Remove(requestId);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _requestService.GetAllRequests().Count());
        }

        [Test]
        public void Remove_RequestIdNotExists_ShouldThrowRequestNotFoundException()*//**//*
        {
            // Arrange
            var nonExistingRequestId = 999;

            // Act & Assert
            Assert.Throws<RequestNotFoundException>(() => _requestService.Remove(nonExistingRequestId));
        }

        *//*[Test]
        public void Update_RequestDTOIsValid_ShouldUpdateRequest()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                ExpenseCategory = "Category",
                Amount = 100,
                Document = "Document",
                Description = "Description",
                Username = "User1"
            };

            var addedRequest = _requestService.Add(requestDTO);

            var updatedRequestDTO = new RequestDTO
            {
                RequestId = addedRequest.RequestId,
                ExpenseCategory = "UpdatedCategory",
                Amount = 200,
                Document = "UpdatedDocument",
                Description = "UpdatedDescription",
                Username = "User1"
            };

            // Act
            var updatedRequest = _requestService.Update(updatedRequestDTO);

            // Assert
            Assert.IsNotNull(updatedRequest);
            Assert.AreEqual("UpdatedCategory", updatedRequest.ExpenseCategory);
            Assert.AreEqual(200, updatedRequest.Amount);
            Assert.AreEqual("UpdatedDocument", updatedRequest.Document);
            Assert.AreEqual("UpdatedDescription", updatedRequest.Description);
        }
*//*
        [Test]
        public void Update_RequestIdNotExists_ShouldThrowRequestNotFoundException()
        {
            // Arrange
            var nonExistingRequestId = 999;
            var updatedRequestDTO = new RequestDTO
            {
                RequestId = nonExistingRequestId,
                ExpenseCategory = "UpdatedCategory",
                Amount = 200,
                Document = "UpdatedDocument",
                Description = "UpdatedDescription",
                Username = "User1"
            };

            // Act & Assert
            Assert.Throws<RequestNotFoundException>(() => _requestService.Update(updatedRequestDTO));
        }

        *//*[Test]
        public void GetRequestById_RequestIdExists_ShouldReturnRequestDTO()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                ExpenseCategory = "Category",
                Amount = 100,
                Document = "Document",
                Description = "Description",
                Username = "User1"
            };

            var addedRequest = _requestService.Add(requestDTO);

            // Act
            var result = _requestService.GetRequestById(addedRequest.RequestId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(requestDTO.ExpenseCategory, result.ExpenseCategory);
            Assert.AreEqual(requestDTO.Amount, result.Amount);
            Assert.AreEqual(requestDTO.Document, result.Document);
            Assert.AreEqual(requestDTO.Description, result.Description);
            Assert.AreEqual(requestDTO.Username, result.Username);
        }*//*

        [Test]
        public void GetRequestById_RequestIdNotExists_ShouldThrowRequestNotFoundException()
        {
            // Arrange
            var nonExistingRequestId = 999;

            // Act & Assert
            Assert.Throws<RequestNotFoundException>(() => _requestService.GetRequestById(nonExistingRequestId));
        }

        [Test]
        public void GetRequestByCategory_CategoryExists_ShouldReturnRequestDTO()
        {
            // Arrange
            var requestDTO = new RequestDTO
            {
                ExpenseCategory = "Category",
                Amount = 100,
                Document = "Document",
                Description = "Description",
                Username = "User1"
            };

            _requestService.Add(requestDTO);

            // Act
            var result = _requestService.GetRequestByCategory("Category");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(requestDTO.ExpenseCategory, result.ExpenseCategory);
            Assert.AreEqual(requestDTO.Amount, result.Amount);
            Assert.AreEqual(requestDTO.Document, result.Document);
            Assert.AreEqual(requestDTO.Description, result.Description);
            Assert.AreEqual(requestDTO.Username, result.Username);
        }

        [Test]
        public void GetRequestByCategory_CategoryNotExists_ShouldThrowRequestNotFoundException()
        {
            // Arrange
            var nonExistingCategory = "NonExistingCategory";

            // Act & Assert
            Assert.Throws<RequestNotFoundException>(() => _requestService.GetRequestByCategory(nonExistingCategory));
        }

        [Test]
        public void GetRequestsByUsername_UserExists_ShouldReturnListOfRequestDTOs()
        {
            // Arrange
            var requestDTO1 = new RequestDTO
            {
                ExpenseCategory = "Category1",
                Amount = 100,
                Document = "Document1",
                Description = "Description1",
                Username = "User1"
            };

            var requestDTO2 = new RequestDTO
            {
                ExpenseCategory = "Category2",
                Amount = 200,
                Document = "Document2",
                Description = "Description2",
                Username = "User1"
            };

            _requestService.Add(requestDTO1);
            _requestService.Add(requestDTO2);

            // Act
            var result = _requestService.GetRequestsByUsername("User1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(r => r.ExpenseCategory == "Category1"));
            Assert.IsTrue(result.Any(r => r.ExpenseCategory == "Category2"));
        }

        [Test]
        public void GetRequestsByUsername_UserNotExists_ShouldReturnEmptyList()
        {
            // Arrange
            var nonExistingUser = "NonExistingUser";

            // Act
            var result = _requestService.GetRequestsByUsername(nonExistingUser);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAllRequests_ShouldReturnListOfRequestDTOs()
        {
            // Arrange
            var requestDTO1 = new RequestDTO
            {
                ExpenseCategory = "Category1",
                Amount = 100,
                Document = "Document1",
                Description = "Description1",
                Username = "User1"
            };

            var requestDTO2 = new RequestDTO
            {
                ExpenseCategory = "Category2",
                Amount = 200,
                Document = "Document2",
                Description = "Description2",
                Username = "User2"
            };

            _requestService.Add(requestDTO1);
            _requestService.Add(requestDTO2);

            // Act
            var result = _requestService.GetAllRequests();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(r => r.ExpenseCategory == "Category1"));
            Assert.IsTrue(result.Any(r => r.ExpenseCategory == "Category2"));
        }
    }
}
*/