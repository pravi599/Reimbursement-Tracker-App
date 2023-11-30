using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Interfaces;

namespace ReimbursementTrackerApp.Repositories
{
    public class UserProfileRepository : IRepository<int, UserProfile>
    {
        private readonly RTAppContext _context;

        public UserProfileRepository(RTAppContext context)
        {
            _context = context;
        }

        // Implementing IRepository interface methods

        public UserProfile GetById(int userId)
        {
            return _context.UserProfiles
                .Include(u => u.User)
                .FirstOrDefault(u => u.UserId == userId);
        }

        public IList<UserProfile> GetAll()
        {
            return _context.UserProfiles
                .Include(u => u.User)
                .ToList();
        }

        public UserProfile Add(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            _context.SaveChanges();
            return userProfile;
        }

        public UserProfile Update(UserProfile userProfile)
        {
            _context.UserProfiles.Update(userProfile);
            _context.SaveChanges();
            return userProfile;
        }

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
