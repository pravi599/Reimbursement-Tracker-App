using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Models;

namespace ReimbursementTrackerApp.Contexts
{
    /// <summary>
    /// Represents the database context for the Reimbursement Tracker application.
    /// </summary>
    public class RTAppContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RTAppContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the context.</param>
        public RTAppContext(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// Gets or sets the DbSet for User entities in the database.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for UserProfile entities in the database.
        /// </summary>
        public DbSet<UserProfile> UserProfiles { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Tracking entities in the database.
        /// </summary>
        public DbSet<Tracking> Trackings { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Request entities in the database.
        /// </summary>
        public DbSet<Request> Requests { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for PaymentDetails entities in the database.
        /// </summary>
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
    }
}
