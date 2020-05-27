using System;
using System.Linq;
using System.Linq.Expressions;

namespace Legato.Contexts.Contracts
{
    /// <summary>
    ///     Contains the common CRUD operations for the database contexts
    /// </summary>
    /// <typeparam name="T">General database context</typeparam>
    public interface IRepositoryBase<T>
    {
        /// <summary>
        ///     Get every record in the provided context
        /// </summary>
        /// <returns>
        ///     Provides functionality to evaluate queries against a specific data source wherein the type of the data is not
        ///     specified.
        /// </returns>
        IQueryable<T> FindAll();

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
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        /// <summary>
        ///     Creates a new record in the database
        /// </summary>
        /// <param name="entity">Generic model class</param>
        void Create(T entity);

        /// <summary>
        ///     Update an existing record in the database
        /// </summary>
        /// <param name="entity">Generic model class</param>
        void Update(T entity);

        /// <summary>
        ///     Delete an existing record in the database
        /// </summary>
        /// <param name="entity">Generic model class</param>
        void Delete(T entity);
    }
}