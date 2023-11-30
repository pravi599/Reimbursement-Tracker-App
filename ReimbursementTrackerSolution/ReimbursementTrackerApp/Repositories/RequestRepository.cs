using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Interfaces;

namespace ReimbursementTrackerApp.Repositories
{
    public class RequestRepository : IRepository<int, Request>
    {
        private readonly RTAppContext _context;

        public RequestRepository(RTAppContext context)
        {
            _context = context;
        }

        // Implementing IRepository interface methods

        public Request GetById(int requestId)
        {
            return _context.Requests
                .Include(r => r.User)
                .FirstOrDefault(r => r.RequestId == requestId);
        }

        public IList<Request> GetAll()
        {
            return _context.Requests
                .Include(r => r.User)
                .ToList();
        }

        public Request Add(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
            return request;
        }

        public Request Update(Request request)
        {
            _context.Requests.Update(request);
            _context.SaveChanges();
            return request;
        }

        public Request Delete(int requestId)
        {
            var request = _context.Requests.Find(requestId);
            if (request != null)
            {
                // Delete related tracking
                var tracking = _context.Trackings.FirstOrDefault(t => t.RequestId == requestId);
                if (tracking != null)
                {
                    _context.Trackings.Remove(tracking);
                }

                _context.Requests.Remove(request);
                _context.SaveChanges();
            }
            return request;
        }
    }
}
