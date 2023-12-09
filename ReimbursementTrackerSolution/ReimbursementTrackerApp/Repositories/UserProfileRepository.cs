using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Models;
using System.Collections.Generic;
using System.Linq;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Interfaces;

namespace ReimbursementTrackerApp.Repositories
{
    /// <summary>
    /// Repository for managing UserProfile entities using Entity Framework Core.
    /// </summary>
    public class UserProfileRepository : IRepository<int, UserProfile>
    {
        private readonly RTAppContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UserProfileRepository(RTAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a user profile entity from the database based on the user ID.
        /// </summary>
        /// <param name="userId">The ID of the user profile to be retrieved.</param>
        /// <returns>Returns the user profile entity if found; otherwise, returns null.</returns>
        public UserProfile GetById(int userId)
        {
            return _context.UserProfiles
                .FirstOrDefault(u => u.UserId == userId);
        }

        /// <summary>
        /// Retrieves all user profile entities from the database.
        /// </summary>
        /// <returns>Returns a list of user profile entities.</returns>
        public IList<UserProfile> GetAll()
        {
            return _context.UserProfiles
                .ToList();
        }

        /// <summary>
        /// Adds a new user profile entity to the database.
        /// </summary>
        /// <param name="userProfile">The user profile entity to be added.</param>
        /// <returns>Returns the added user profile entity.</returns>
        public UserProfile Add(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            _context.SaveChanges();
            return userProfile;
        }

        /// <summary>
        /// Updates a user profile entity in the database.
        /// </summary>
        /// <param name="userProfile">The updated user profile entity.</param>
        /// <returns>Returns the updated user profile entity.</returns>
        public UserProfile Update(UserProfile userProfile)
        {
            _context.UserProfiles.Update(userProfile);
            _context.SaveChanges();
            return userProfile;
        }

        /// <summary>
        /// Deletes a user profile entity from the database based on the user ID.
        /// </summary>
        /// <param name="userId">The ID of the user profile to be deleted.</param>
        /// <returns>Returns the deleted user profile entity if found; otherwise, returns null.</returns>
        public UserProfile Delete(int userId)
        {
            var userProfile = _context.UserProfiles.Find(userId);
            if (userProfile != null)
            {
                _context.UserProfiles.Remove(userProfile);
                _context.SaveChanges();
            }
            return userProfile;
        }
    }
}