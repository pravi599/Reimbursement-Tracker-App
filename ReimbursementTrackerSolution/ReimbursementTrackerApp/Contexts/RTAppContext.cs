using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Models;

namespace ReimbursementTrackerApp.Contexts
{
    public class RTAppContext : DbContext
    {
        public RTAppContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Tracking>  Trackings{ get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
    }
}
