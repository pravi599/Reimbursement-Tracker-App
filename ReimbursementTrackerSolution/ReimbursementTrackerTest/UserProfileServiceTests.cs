using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ReimbursementTrackerApp.Services;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Models.DTOs;
using ReimbursementTrackerApp.Exceptions;
using ReimbursementTrackerApp.Repositories;
using ReimbursementTrackerApp.Contexts;

namespace ReimbursementTrackerApp.Tests
{
    [TestFixture]
    public class UserProfileServiceTests
    {
        private UserProfileService _userProfileService;
        private DbContextOptions<RTAppContext> _dbContextOptions;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<RTAppContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                .Options;

            _userProfileService = new UserProfileService(new UserProfileRepository(new RTAppContext(_dbContextOptions)));
        }

        [Test]
        public void Add_NewUserProfile_Successful()
        {
            // Arrange
            var userProfileDTO = new UserProfileDTO
            {
                Username = "testUser",
                FirstName = "John",
                LastName = "Doe",
                City = "New York",
                ContactNumber = "1234567890",
                BankAccountNumber = "9876543210"
            };

            // Act
            var result = _userProfileService.Add(userProfileDTO);

            // Assert
            Assert.IsTrue(result);
            using (var context = new RTAppContext(_dbContextOptions))
            {
                Assert.AreEqual(1, context.UserProfiles.Count());
                Assert.AreEqual("testUser", context.UserProfiles.First().Username);
            }
        }

        // Similar tests for Remove, Update, GetUserProifleById, GetUserProifleByUsername, and GetAllUserProfiles
        // ...

        [Test]
        public void Remove_UserProfileFound_Successful()
        {
            // Arrange
            var userProfileDTO = new UserProfileDTO
            {
                Username = "testUser",
                FirstName = "John",
                LastName = "Doe",
                City = "New York",
                ContactNumber = "1234567890",
                BankAccountNumber = "9876543210"
            };

            // Act
            var result = _userProfileService.Add(userProfileDTO);


            // Act
            var resultt = _userProfileService.Remove("testUser");

            // Assert
            Assert.IsTrue(resultt);

            // Validate the state of the database after the removal
            using (var context = new RTAppContext(_dbContextOptions))
            {
                Assert.AreEqual(0, context.UserProfiles.Count());
            }
        }

    }
}


//[Test]
//        public void Remove_UserProfileNotFound_ExceptionThrown()
//        {
//            // Act & Assert
//            Assert.Throws<UserProfileNotFoundException>(() => _userProfileService.Remove("nonExistentUser"));
//        }
//    }
//}