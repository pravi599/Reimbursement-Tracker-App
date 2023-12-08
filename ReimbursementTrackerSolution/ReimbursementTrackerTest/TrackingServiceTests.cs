using Microsoft.EntityFrameworkCore;
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
        private TrackingService _trackingService;
        private DbContextOptions<RTAppContext> _dbContextOptions;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<RTAppContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                .Options;

            var dbContext = new RTAppContext(_dbContextOptions);
            _trackingService = new TrackingService(new TrackingRepository(new RTAppContext(_dbContextOptions)));
        }

        [Test]
        public void Add_Tracking_Successful()
        {
            // Arrange
            var trackingDTO = new TrackingDTO
            {
                RequestId = 1,
                TrackingStatus = "Approved",
                ApprovalDate = DateTime.Now,
                ReimbursementDate = DateTime.Now.AddDays(7)
            };

            // Act
            var result = _trackingService.Add(trackingDTO);

            // Assert
            Assert.IsTrue(result);

            // Validate the state of the database after the addition
            using (var context = new RTAppContext(_dbContextOptions))
            {
                var trackingInDatabase = context.Trackings.First();
                Assert.AreEqual(1, context.Trackings.Count());
                Assert.AreEqual(trackingDTO.RequestId, trackingInDatabase.RequestId);
                Assert.AreEqual(trackingDTO.TrackingStatus, trackingInDatabase.TrackingStatus);
                Assert.AreEqual(trackingDTO.ApprovalDate, trackingInDatabase.ApprovalDate);
                Assert.AreEqual(trackingDTO.ReimbursementDate, trackingInDatabase.ReimbursementDate);
            }
        }

        [Test]
        public void Remove_Tracking_Successful()
        {
            // Arrange
            using (var context = new RTAppContext(_dbContextOptions))
            {
                var tracking = new Tracking
                {
                    TrackingId = 1,
                    RequestId = 1,
                    TrackingStatus = "Approved",
                    ApprovalDate = DateTime.Now,
                    ReimbursementDate = DateTime.Now.AddDays(7)
                };

                context.Trackings.Add(tracking);
                context.SaveChanges();
            }

            // Act
            var result = _trackingService.Remove(1);

            // Assert
            Assert.IsTrue(result);

            // Validate the state of the database after the removal
            using (var context = new RTAppContext(_dbContextOptions))
            {
                Assert.AreEqual(0, context.Trackings.Count());
            }
        }

        // Add similar tests for Update, GetTrackingByRequestId, GetTrackingByTrackingId, and GetAllTrackings
        // ...

        [Test]
        public void Add_Tracking_TrackingNotFoundException()
        {
            // Arrange
            var trackingDTO = new TrackingDTO
            {
                RequestId = 1,
                TrackingStatus = "Approved",
                ApprovalDate = DateTime.Now,
                ReimbursementDate = DateTime.Now.AddDays(7)
            };

            // Act and Assert
            Assert.Throws<TrackingNotFoundException>(() => _trackingService.Add(trackingDTO));
        }

        // Add similar tests for Remove, Update, GetTrackingByRequestId, GetTrackingByTrackingId, and GetAllTrackings with exceptions
        // ...
    }
}