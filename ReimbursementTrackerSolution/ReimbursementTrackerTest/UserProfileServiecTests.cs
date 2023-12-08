using NUnit.Framework;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Exceptions;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Models.DTOs;
using ReimbursementTrackerApp.Repositories;
using ReimbursementTrackerApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ReimbursementTrackerApp.Tests
{
    [TestFixture]
    public class UserProfileServiceTests
    {
        private static RTAppContext _dbContext;
        private UserProfileService _userProfileService;
        private UserProfileRepository _userProfileRepository;

        [OneTimeSetUp]
        public static void OneTimeSetup()
        {
            // Set up an in-memory database context for testing
            var options = new DbContextOptionsBuilder<RTAppContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                .Options;

            _dbContext = new RTAppContext(options);
        }

        [SetUp]
        public void Initialize()
        {
            // Use the same context for all tests
            _userProfileRepository = new UserProfileRepository(_dbContext);
            _userProfileService = new UserProfileService(_userProfileRepository);
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            // Clean up the database after all tests
            _dbContext.Dispose();
        }


        [Test]
        public void Add_NewUserProfile_ReturnsTrue()
        {
            // Arrange
            var userProfileDTO = new UserProfileDTO
            {
                UserId = 1,
                Username = "existingUser",
                FirstName = "John",
                LastName = "Doe",
                City = "City",
                ContactNumber = "123456789",
                BankAccountNumber = "987654321"
            };

            // Act
            var result = _userProfileService.Add(userProfileDTO);

            // Assert
            Assert.IsTrue(result);
        }

        



        [Test]
        public void Remove_NonExistingUserProfile_ThrowsException()
        {
            // Arrange
            var usernameToRemove = "nonExistingUser";

            // Act & Assert
            Assert.Throws<UserProfileNotFoundException>(() => _userProfileService.Remove(usernameToRemove));
        }


        [Test]
        public void Update_NonExistingUserProfile_ThrowsException()
        {
            // Arrange
            var userProfileDTO = new UserProfileDTO
            {
                UserId = 1,
                Username = "nonExistingUser",
                FirstName = "John",
                LastName = "Doe",
                City = "City",
                ContactNumber = "123456789",
                BankAccountNumber = "987654321"
            };

            // Act & Assert
            Assert.Throws<UserProfileNotFoundException>(() => _userProfileService.Update(userProfileDTO));
        }

        

        [Test]
        public void GetUserProifleById_NonExistingUserId_ThrowsException()
        {
            // Arrange
            var nonExistingUserId = 999;

            // Act & Assert
            Assert.Throws<UserProfileNotFoundException>(() => _userProfileService.GetUserProifleById(nonExistingUserId));
        }

        
        [Test]
        public void GetUserProfileByUsername_NonExistingUsername_ThrowsException()
        {
            // Arrange
            var nonExistingUsername = "nonExistingUser";

            // Act & Assert
            Assert.Throws<UserProfileNotFoundException>(() => _userProfileService.GetUserProfileByUsername(nonExistingUsername));
        }

        
    }
}
