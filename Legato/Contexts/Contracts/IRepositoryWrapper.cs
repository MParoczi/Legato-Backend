﻿namespace Legato.Contexts.Contracts
{
    /// <summary>
    ///     Wraps the repositories
    /// </summary>
    public interface IRepositoryWrapper
    {
        /// <summary>
        ///     UserRepository class that contains methods to alter the user entity in the database
        /// </summary>
        IUserRepository User { get; }

        /// <summary>
        ///     Save changes in the database
        /// </summary>
        void Save();
    }
}