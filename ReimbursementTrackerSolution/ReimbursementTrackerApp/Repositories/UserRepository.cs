using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Interfaces;
using ReimbursementTrackerApp.Models;

namespace ReimbursementTrackerApp.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly RTAppContext _context;

        public UserRepository(RTAppContext context)
        {
            _context = context;
        }
        public User Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }
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
        public IList<User> GetAll()
        {
            if (_context.Users.Count() == 0)
                return null;
            return _context.Users.ToList();
        }
        public User GetById(string key)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == key);
            return user;
        }
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
