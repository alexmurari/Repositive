namespace Repositive.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///     Defines a repository contract for deleting entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that the repository deletes.
    /// </typeparam>
    public interface IDeletableRepository<in TEntity> where TEntity : class
    {
        /// <summary>
        ///     Deletes the entity of the provided type from the database repository.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <param name="deleteRelated">The value that indicates whether or not related entities reachable from the provided entity should be deleted in the operation.</param>
        void Delete(TEntity entity, bool deleteRelated = false);

        /// <summary>
        ///     Deletes the entities of the provided type from the database repository.
        /// </summary>
        /// <param name="entities">The collection of entities to be deleted.</param>
        /// <param name="deleteRelated">The value that indicates whether or not related entities reachable from the provided entity should be deleted in the operation.</param>
        void DeleteRange(IEnumerable<TEntity> entities, bool deleteRelated = false);

        /// <summary>
        ///     Asynchronously deletes the entity of the provided type from the database repository.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <param name="deleteRelated">The value that indicates whether or not related entities reachable from the provided entity should be deleted in the operation.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteAsync(TEntity entity, bool deleteRelated = false);

        /// <summary>
        ///     Asynchronously deletes the entities of the provided type from the database repository.
        /// </summary>
        /// <param name="entities">The collection of entities to be deleted.</param>
        /// <param name="deleteRelated">The value that indicates whether or not related entities reachable from the provided entity should be deleted in the operation.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool deleteRelated = false);
    }
}