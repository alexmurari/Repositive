namespace Repositive.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///     Defines a repository contract for updating entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that the repository updates.
    /// </typeparam>
    public interface IUpdateableRepository<in TEntity> where TEntity : class
    {
        /// <summary>
        ///     Updates an entity of the provided type in the database repository.
        /// </summary>
        /// <param name="entity">The object to be updated.</param>
        /// <param name="updateRelated">The value that indicates whether or not related entities reachable from the provided entity should be included in the update operation.</param>
        void Update(TEntity entity, bool updateRelated = false);

        /// <summary>
        ///     Asynchronously updates an entity of the provided type in the database repository.
        /// </summary>
        /// <param name="entity">The object to be updated.</param>
        /// <param name="updateRelated">The value that indicates whether or not related entities reachable from the provided entity should be included in the update operation.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateAsync(TEntity entity, bool updateRelated = false);

        /// <summary>
        ///     Updates an collection of entities of the provided type in the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be updated.</param>
        /// <param name="updateRelated">The value that indicates whether or not related entities reachable from the provided entity should be included in the update operation.</param>
        void UpdateRange(IEnumerable<TEntity> entityCollection, bool updateRelated = false);

        /// <summary>
        ///     Asynchronously updates an collection of entities of the provided type in the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be updated.</param>
        /// <param name="updateRelated">The value that indicates whether or not related entities reachable from the provided entity should be included in the update operation.</param>
        /// <returns>A task that represents the asynchronous update range operation.</returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entityCollection, bool updateRelated = false);
    }
}