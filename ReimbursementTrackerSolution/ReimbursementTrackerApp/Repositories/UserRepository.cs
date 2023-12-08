using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReimbursementTrackerApp.Repositories
{
    /// <summary>
    /// Repository for managing User entities using Entity Framework Core.
    /// </summary>
    public class UserRepository : IRepository<string, User>
    {
        private readonly RTAppContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UserRepository(RTAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new user entity to the database.
        /// </summary>
        /// <param name="entity">The user entity to be added.</param>
        /// <returns>Returns the added user entity.</returns>
        public User Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Deletes a user entity from the database based on the username.
        /// </summary>
        /// <param name="key">The username of the user to be deleted.</param>
        /// <returns>Returns the deleted user entity if found; otherwise, returns null.</returns>
        public User Delete(string key)
        {
            var user = GetById(key);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return user;
            }
            return null;
        }

        /// <summary>
        /// Retrieves all user entities from the database.
        /// </summary>
        /// <returns>Returns a list of user entities; null if no users are found.</returns>
        public IList<User> GetAll()
        {
            if (_context.Users.Count() == 0)
                return null;
            return _context.Users.ToList();
        }

        /// <summary>
        /// Retrieves a user entity from the database based on the username.
        /// </summary>
        /// <param name="key">The username of the user to be retrieved.</param>
        /// <returns>Returns the user entity if found; otherwise, returns null.</returns>
        public User GetById(string key)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == key);
            return user;
        }

        /// <summary>
        /// Updates a user entity in the database.
        /// </summary>
        /// <param name="entity">The updated user entity.</param>
        /// <returns>Returns the updated user entity if found; otherwise, returns null.</returns>
        public User Update(User entity)
        {
            var user = GetById(entity.Username);
            if (user != null)
            {
                _context.Entry<User>(user).State = EntityState.Modified;
                _context.SaveChanges();
                return user;
            }
            return null;
        }
    }
}