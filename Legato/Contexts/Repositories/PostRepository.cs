using Legato.Contexts.Contracts;
using Legato.Models.PostModel;

namespace Legato.Contexts.Repositories
{
    /// <summary>
    ///     Contains database operations for the Post model
    /// </summary>
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        /// <summary>
        ///     Constructor for the PostRepository class
        /// </summary>
        /// <param name="appContext">Provided AppContext via Dependency Injection</param>
        public PostRepository(AppContext appContext) : base(appContext)
        {
        }
    }
}