using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Models;
using System.Collections.Generic;
using System.Linq;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Interfaces;

namespace ReimbursementTrackerApp.Repositories
{
    /// <summary>
    /// Repository for managing Request entities using Entity Framework Core.
    /// </summary>
    public class RequestRepository : IRepository<int, Request>
    {
        private readonly RTAppContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RequestRepository(RTAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a request entity from the database based on the request ID.
        /// </summary>
        /// <param name="requestId">The ID of the request entity to be retrieved.</param>
        /// <returns>Returns the request entity if found; otherwise, returns null.</returns>
        public Request GetById(int requestId)
        {
            return _context.Requests
                .Include(r => r.User)
                .FirstOrDefault(r => r.RequestId == requestId);
        }

        /// <summary>
        /// Retrieves all request entities from the database.
        /// </summary>
        /// <returns>Returns a list of request entities.</returns>
        public IList<Request> GetAll()
        {
            return _context.Requests
                .Include(r => r.User)
                .ToList();
        }

        /// <summary>
        /// Adds a new request entity to the database.
        /// </summary>
        /// <param name="request">The request entity to be added.</param>
        /// <returns>Returns the added request entity.</returns>
        public Request Add(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
            return request;
        }

        /// <summary>
        /// Updates a request entity in the database.
        /// </summary>
        /// <param name="request">The updated request entity.</param>
        /// <returns>Returns the updated request entity.</returns>
        public Request Update(Request request)
        {
            _context.Requests.Update(request);
            _context.SaveChanges();
            return request;
        }

        /// <summary>
        /// Deletes a request entity from the database based on the request ID.
        /// </summary>
        /// <param name="requestId">The ID of the request entity to be deleted.</param>
        /// <returns>Returns the deleted request entity if found; otherwise, returns null.</returns>
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