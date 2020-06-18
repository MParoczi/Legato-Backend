using Legato.Models.UserModels;

namespace Legato.Contexts.Contracts
{
    /// <summary>
    ///     Contains database operations for the AppUser model
    /// </summary>
    public interface IUserRepository : IRepositoryBase<AppUser>
    {
    }
}