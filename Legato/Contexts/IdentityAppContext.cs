using Legato.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Legato.Contexts
{
    /// <summary>
    ///     A DbContext instance represents a combination of the Unit Of Work and Repository patterns such that it can be used
    ///     to query from a database and group together changes that will then be written back to the store as a unit.
    /// </summary>
    public class IdentityAppContext : IdentityDbContext<AppUser, AppRole, int>
    {
        /// <summary>
        ///     Constructor for IdentityAppContext
        /// </summary>
        /// <param name="options">The options to be used by a DbContext.</param>
        public IdentityAppContext(DbContextOptions<IdentityAppContext> options) : base(options)
        {
        }
    }
}