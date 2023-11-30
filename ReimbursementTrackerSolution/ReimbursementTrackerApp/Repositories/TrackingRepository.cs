using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Interfaces;

namespace ReimbursementTrackerApp.Repositories
{
    public class TrackingRepository : IRepository<int, Tracking>
    {
        private readonly RTAppContext _context;

        public TrackingRepository(RTAppContext context)
        {
            _context = context;
        }

        // Implementing IRepository interface methods

        public Tracking GetById(int trackingId)
        {
            return _context.Trackings
                .Include(t => t.Request)
                .FirstOrDefault(t => t.TrackingId == trackingId);
        }

        public IList<Tracking> GetAll()
        {
            return _context.Trackings
                .Include(t => t.Request)
                .ToList();
        }

        public Tracking Add(Tracking tracking)
        {
            _context.Trackings.Add(tracking);
            _context.SaveChanges();
            return tracking;
        }

        public Tracking Update(Tracking tracking)
        {
            _context.Trackings.Update(tracking);
            _context.SaveChanges();
            return tracking;
        }

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
