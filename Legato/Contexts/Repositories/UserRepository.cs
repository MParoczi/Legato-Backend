using Legato.Contexts.Contracts;
using Legato.Models;

namespace Legato.Contexts.Repositories
{
    /// <summary>
    ///     Contains database operations for the AppUser model
    /// </summary>
    public class UserRepository : RepositoryBase<AppUser>, IUserRepository
    {
        /// <summary>
        ///     Constructor for the UserRepository class
        /// </summary>
        /// <param name="appContext">Provided AppContext via Dependency Injection</param>
        public UserRepository(AppContext appContext) : base(appContext)
        {
        }
    }
}