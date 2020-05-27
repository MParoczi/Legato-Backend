using System;
using System.Linq;
using System.Linq.Expressions;
using Legato.Contexts.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Legato.Contexts.Repositories
{
    /// <summary>
    ///     Implementation of the IRepositoryBase interface
    /// </summary>
    /// <typeparam name="T">General database entity</typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        /// <summary>
        ///     Constructor for the RepositoryBase class
        /// </summary>
        /// <param name="appContext">Provided context for the RepositoryBase via Dependency Injection</param>
        public RepositoryBase(AppContext appContext)
        {
            AppContext = appContext;
        }

        /// <summary>
        ///     Provided context for the RepositoryBase via Dependency Injection
        /// </summary>
        public AppContext AppContext { get; set; }

        /// <summary>
        ///     Get every record in the provided context
        /// </summary>
        /// <returns>
        ///     Provides functionality to evaluate queries against a specific data source wherein the type of the data is not
        ///     specified.
        /// </returns>
        public IQueryable<T> FindAll()
        {
            return AppContext.Set<T>().AsNoTracking();
        }

        /// <summary>
        ///     Get every record that satisfies the expression
        /// </summary>
        /// <param name="expression">
        ///     Represents a strongly typed lambda expression as a data structure in the form of an expression
        ///     tree.
        /// </param>
        /// <returns>
        ///     Provides functionality to evaluate queries against a specific data source wherein the type of the data is not
        ///     specified.
        /// </returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return AppContext.Set<T>().Where(expression).AsNoTracking();
        }

        /// <summary>
        ///     Creates a new record in the database
        /// </summary>
        /// <param name="entity">Generic model class</param>
        public void Create(T entity)
        {
            AppContext.Set<T>().Add(entity);
        }

        /// <summary>
        ///     Update an existing record in the database
        /// </summary>
        /// <param name="entity">Generic model class</param>
        public void Update(T entity)
        {
            AppContext.Set<T>().Update(entity);
        }

        /// <summary>
        ///     Delete an existing record in the database
        /// </summary>
        /// <param name="entity">Generic model class</param>
        public void Delete(T entity)
        {
            AppContext.Set<T>().Remove(entity);
        }
    }
}