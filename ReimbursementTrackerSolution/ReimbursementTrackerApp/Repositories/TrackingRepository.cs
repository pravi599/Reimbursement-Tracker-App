using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Models;
using System.Collections.Generic;
using System.Linq;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Interfaces;

namespace ReimbursementTrackerApp.Repositories
{
    /// <summary>
    /// Repository for managing Tracking entities using Entity Framework Core.
    /// </summary>
    public class TrackingRepository : IRepository<int, Tracking>
    {
        private readonly RTAppContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TrackingRepository(RTAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a tracking entity from the database based on the tracking ID.
        /// </summary>
        /// <param name="trackingId">The ID of the tracking entity to be retrieved.</param>
        /// <returns>Returns the tracking entity if found; otherwise, returns null.</returns>
        public Tracking GetById(int trackingId)
        {
            return _context.Trackings
                .FirstOrDefault(t => t.TrackingId == trackingId);
        }

        /// <summary>
        /// Retrieves all tracking entities from the database.
        /// </summary>
        /// <returns>Returns a list of tracking entities.</returns>
        public IList<Tracking> GetAll()
        {
            return _context.Trackings
                .ToList();
        }

        /// <summary>
        /// Adds a new tracking entity to the database.
        /// </summary>
        /// <param name="tracking">The tracking entity to be added.</param>
        /// <returns>Returns the added tracking entity.</returns>
        public Tracking Add(Tracking tracking)
        {
            _context.Trackings.Add(tracking);
            _context.SaveChanges();
            return tracking;
        }

        /// <summary>
        /// Updates a tracking entity in the database.
        /// </summary>
        /// <param name="tracking">The updated tracking entity.</param>
        /// <returns>Returns the updated tracking entity.</returns>
        public Tracking Update(Tracking tracking)
        {
            _context.Trackings.Update(tracking);
            _context.SaveChanges();
            return tracking;
        }

        /// <summary>
        /// Deletes a tracking entity from the database based on the tracking ID.
        /// </summary>
        /// <param name="trackingId">The ID of the tracking entity to be deleted.</param>
        /// <returns>Returns the deleted tracking entity if found; otherwise, returns null.</returns>
        public Tracking Delete(int trackingId)
        {
            var tracking = _context.Trackings.Find(trackingId);
            if (tracking != null)
            {
                _context.Trackings.Remove(tracking);
                _context.SaveChanges();
            }
            return tracking;
        }
    }
}