﻿using Legato.Models;

namespace Legato.Contexts.Contracts
{
    /// <summary>
    ///     Contains database operations for the AppUser model
    /// </summary>
    public interface IUserRepository : IRepositoryBase<AppUser>
    {
    }
}