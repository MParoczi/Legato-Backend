using Legato.Models.PostModel;

namespace Legato.Contexts.Contracts
{
    /// <summary>
    ///     Contains database operations for the Post model
    /// </summary>
    public interface IPostRepository : IRepositoryBase<Post>
    {
    }
}