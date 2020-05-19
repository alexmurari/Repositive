namespace Repositive.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///     Defines a repository contract for creating entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that the repository creates.
    /// </typeparam>
    public interface ICreatableRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Adds an entity of the provided type to the database repository.
        /// </summary>
        /// <param name="entity">The object to be added.</param>
        /// <returns>The added object.</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        ///     Asynchronously adds an entity of the provided type to the database repository.
        /// </summary>
        /// <param name="entity">The object to be added.</param>
        /// <returns>
        ///     A task that represents the asynchronous add operation.
        ///     The task result contains the added object.
        /// </returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        ///     Adds an collection of entities of the provided type to the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be added.</param>
        void AddRange(IEnumerable<TEntity> entityCollection);

        /// <summary>
        ///     Asynchronously adds an collection of entities of the provided type to the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be added.</param>
        /// <returns>A task that represents the asynchronous add range operation.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entityCollection);
    }
}