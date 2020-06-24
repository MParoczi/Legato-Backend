using Legato.Contexts.Configuration;
using Legato.Models.PostModel;
using Legato.Models.UserModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Legato.Contexts
{
    /// <summary>
    ///     A DbContext instance represents a combination of the Unit Of Work and Repository patterns such that it can be used
    ///     to query from a database and group together changes that will then be written back to the store as a unit.
    /// </summary>
    public class AppContext : IdentityDbContext<AppUser, AppRole, int>
    {
        /// <summary>
        ///     Constructor for AppContext
        /// </summary>
        /// <param name="options">The options to be used by a DbContext.</param>
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        /// <summary>
        ///     DbSet that represents the post persisted in the database
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        ///     Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
            builder.ApplyConfiguration(new PostEntityTypeConfiguration());
        }
    }
}