﻿using System.Threading.Tasks;
using Legato.Contexts.Contracts;

namespace Legato.Contexts.Repositories
{
    /// <summary>
    ///     Wraps the repositories
    /// </summary>
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppContext _context;
        private IPostRepository _post;
        private IUserRepository _user;

        /// <summary>
        ///     Constructor for the RepositoryWrapper class
        /// </summary>
        /// <param name="context">Provided AppContext via Dependency Injection</param>
        public RepositoryWrapper(AppContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     UserRepository class that contains methods to alter the user entity in the database
        /// </summary>
        public IUserRepository User => _user ??= new UserRepository(_context);

        /// <summary>
        ///     PostRepository class that contains methods to alter the post entity in the database
        /// </summary>
        public IPostRepository Post => _post ??= new PostRepository(_context);

        /// <summary>
        ///     Save changes in the database
        /// </summary>
        public Task Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}