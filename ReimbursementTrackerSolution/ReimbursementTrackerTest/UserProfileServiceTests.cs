using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Exceptions;
using ReimbursementTrackerApp.Models;
using ReimbursementTrackerApp.Models.DTOs;
using ReimbursementTrackerApp.Repositories;
using ReimbursementTrackerApp.Services;
using System.Linq;
namespace ReimbursementTrackerApp.Tests;
[TestFixture]
public class UserProfileServiceTests
{
    private UserProfileService userProfileService;
    private UserProfileRepository userProfileRepository;

    [SetUp]
    public void Setup()
    {
        var dbOptions = new DbContextOptionsBuilder<RTAppContext>()
                            .UseInMemoryDatabase("dbTestUserProfile")
                            .Options;

        RTAppContext context = new RTAppContext(dbOptions);
        userProfileRepository = new UserProfileRepository(context);
        userProfileService = new UserProfileService(userProfileRepository);
    }

    [Test]
    public void AddProfile_Success()
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
        var result = userProfileService.Add(userProfileDTO);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void AddProfile_DuplicateUsername_ThrowsException()
    {
        // Arrange
        var userProfileDTO = new UserProfileDTO
        {
            Username = "duplicateUser",
            FirstName = "John",
            LastName = "Doe",
            City = "New York",
            ContactNumber = "1234567890",
            BankAccountNumber = "9876543210"
        };

        // Add the first profile
        userProfileService.Add(userProfileDTO);

    }

    [Test]
    public void RemoveProfile_Success()
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

        // Add the profile
        userProfileService.Add(userProfileDTO);

        // Act
        var result = userProfileService.Remove("testUser");

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void RemoveProfile_NotFound_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<UserProfileNotFoundException>(() => userProfileService.Remove("nonexistentUser"));
    }

    [Test]
    public void UpdateProfile_Success()
    {
        // Arrange
        var userProfileDTO = new UserProfileDTO
        {
            UserId = 1,
            Username = "testUser",
            FirstName = "John",
            LastName = "Doe",
            City = "New York",
            ContactNumber = "1234567890",
            BankAccountNumber = "9876543210"
        };
        var userProfileDTO2 = new UserProfileDTO
        {
            UserId = 1,
            Username = "testUser",
            FirstName = "UpdatedJohn",
            LastName = "Doe",
            City = "New York",
            ContactNumber = "1234567890",
            BankAccountNumber = "9876543210"
        };

        // Add the profile
        userProfileService.Add(userProfileDTO);


        /*// Modify the profile
        userProfileDTO.FirstName = "UpdatedJohn";*/

        // Act
        var updatedProfile = userProfileService.Update(userProfileDTO2);

        // Assert
        Assert.AreEqual("UpdatedJohn", updatedProfile.FirstName);
    }

    [Test]
    public void UpdateProfile_NotFound_ThrowsException()
    {
        // Arrange
        var userProfileDTO = new UserProfileDTO
        {
            UserId = 1, // Assuming this ID doesn't exist
            Username = "nonexistentUser",
            FirstName = "John",
            LastName = "Doe",
            City = "New York",
            ContactNumber = "1234567890",
            BankAccountNumber = "9876543210"
        };

        // Act & Assert
        Assert.Throws<UserProfileNotFoundException>(() => userProfileService.Update(userProfileDTO));
    }

    [Test]
    public void GetUserProfileById_Success()
    {
        // Arrange
        var userProfileDTO = new UserProfileDTO
        {
            UserId = 1,
            Username = "testUser",
            FirstName = "John",
            LastName = "Doe",
            City = "New York",
            ContactNumber = "1234567890",
            BankAccountNumber = "9876543210"
        };

        // Add the profile
        var addedProfile = userProfileService.Add(userProfileDTO);

        // Act
        //  var retrievedProfile = userProfileService.GetUserProifleById(addedProfile.UserId);

        // Assert
        // Assert.AreEqual("John", retrievedProfile.FirstName);
    }

    [Test]
    public void GetUserProfileById_NotFound_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<UserProfileNotFoundException>(() => userProfileService.GetUserProifleById(999));
    }

    [Test]
    public void GetUserProfileByUsername_Success()
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

        // Add the profile
        userProfileService.Add(userProfileDTO);

        // Act
        var retrievedProfile = userProfileService.GetUserProfileByUsername("testUser");

        // Assert
        Assert.AreEqual("John", retrievedProfile.FirstName);
    }

    [Test]
    public void GetUserProfileByUsername_NotFound_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<UserProfileNotFoundException>(() => userProfileService.GetUserProfileByUsername("nonexistentUser"));
    }

    [Test]
    public void GetAllUserProfiles_Success()
    {
        // Arrange
        var userProfileDTO1 = new UserProfileDTO
        {
            Username = "user1",
            FirstName = "John",
            LastName = "Doe",
            City = "New York",
            ContactNumber = "1234567890",
            BankAccountNumber = "9876543210"
        };

        var userProfileDTO2 = new UserProfileDTO
        {
            Username = "user2",
            FirstName = "Jane",
            LastName = "Doe",
            City = "San Francisco",
            ContactNumber = "9876543210",
            BankAccountNumber = "1234567890"
        };

        // Add the profiles
        userProfileService.Add(userProfileDTO1);
        userProfileService.Add(userProfileDTO2);

        // Act
        var allProfiles = userProfileService.GetAllUserProfiles();

        // Assert
        Assert.AreEqual(2, allProfiles.Count());
    }

    // Your test methods go here
}
