using System.Threading.Tasks;
using Legato.Contexts.Contracts;

namespace Legato.Contexts.Repositories
{
    /// <summary>
    ///     Wraps the repositories
    /// </summary>
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppContext _context;

        /// <summary>
        ///     Constructor for the RepositoryWrapper class
        /// </summary>
        /// <param name="context">Provided AppContext via Dependency Injection</param>
        /// <param name="post">PostRepository class that contains methods to alter the post entity in the database</param>
        /// <param name="user">UserRepository class that contains methods to alter the user entity in the database</param>
        public RepositoryWrapper(AppContext context, IPostRepository post, IUserRepository user)
        {
            _context = context;
            Post = post;
            User = user;
        }

        /// <summary>
        ///     PostRepository class that contains methods to alter the post entity in the database
        /// </summary>
        public IPostRepository Post { get; }

        /// <summary>
        ///     UserRepository class that contains methods to alter the user entity in the database
        /// </summary>
        public IUserRepository User { get; }

        /// <summary>
        ///     Save changes in the database
        /// </summary>
        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}