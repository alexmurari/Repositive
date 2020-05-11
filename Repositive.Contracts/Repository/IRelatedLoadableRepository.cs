namespace Repositive.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    ///     Provides repository methods for explicitly loading related entities referenced by navigation properties in instances of <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that has the navigation property references.
    /// </typeparam>
    public interface IRelatedLoadableRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Loads the entity referenced by the specified navigation property.
        /// </summary>
        /// <param name="entity">
        ///     The entity instance with the navigation property reference.
        /// </param>
        /// <param name="navigationProperty">
        ///     The navigation property to load.
        /// </param>
        /// <param name="includes">The related entities from the navigation property to be included in the query.</param>
        /// <typeparam name="TProperty">
        ///     The entity type referenced by the navigation property.
        /// </typeparam>
        /// <returns>
        ///     The entity with the loaded navigation property.
        /// </returns>
        TEntity LoadRelated<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty, params Expression<Func<TProperty, object>>[] includes) where TProperty : class;

        /// <summary>
        ///    Loads the entity referenced by the specified navigation property if it matches the predicate condition.
        /// </summary>
        /// <param name="entity">
        ///     The entity instance with the navigation property reference.
        /// </param>
        /// <param name="navigationProperty">
        ///     The navigation property to load.
        /// </param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <param name="includes">The related entities from the navigation property to be included in the query.</param>
        /// <typeparam name="TProperty">
        ///     The entity type referenced by the navigation property.
        /// </typeparam>
        /// <returns>
        ///     The entity with the loaded navigation property.
        /// </returns>
        TEntity LoadRelated<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty, Expression<Func<TProperty, bool>> predicate, params Expression<Func<TProperty, object>>[] includes) where TProperty : class;

        /// <summary>
        ///     Asynchronously loads the entity referenced by the specified navigation property.
        /// </summary>
        /// <param name="entity">
        ///     The entity instance with the navigation property reference.
        /// </param>
        /// <param name="navigationProperty">
        ///     The navigation property to load.
        /// </param>
        /// <param name="includes">The related entities from the navigation property to be included in the query.</param>
        /// <typeparam name="TProperty">
        ///     The entity type referenced by the navigation property.
        /// </typeparam>
        /// <returns>
        ///     The task that represents the asynchronous query operation.
        ///     The task result contains the entity with the loaded navigation property.
        /// </returns>
        Task<TEntity> LoadRelatedAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty, params Expression<Func<TProperty, object>>[] includes) where TProperty : class;

        /// <summary>
        ///     Asynchronously loads the entity referenced by the specified navigation property if it matches the predicate condition.
        /// </summary>
        /// <param name="entity">
        ///     The entity instance with the navigation property reference.
        /// </param>
        /// <param name="navigationProperty">
        ///     The navigation property to load.
        /// </param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <param name="includes">The related entities from the navigation property to be included in the query.</param>
        /// <typeparam name="TProperty">
        ///     The entity type referenced by the navigation property.
        /// </typeparam>
        /// <returns>
        ///     The task that represents the asynchronous query operation.
        ///     The task result contains the entity with the loaded navigation property.
        /// </returns>
        Task<TEntity> LoadRelatedAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty, Expression<Func<TProperty, bool>> predicate, params Expression<Func<TProperty, object>>[] includes) where TProperty : class;

        /// <summary>
        ///     Loads the collection of entities referenced by the specified navigation property.
        /// </summary>
        /// <param name="entity">
        ///     The entity instance with the navigation property reference.
        /// </param>
        /// <param name="navigationProperty">
        ///     The navigation property to be loaded.
        /// </param>
        /// <param name="includes">The related entities from the navigation property to be included in the query.</param>
        /// <typeparam name="TProperty">
        ///     The entity type referenced by the navigation property.
        /// </typeparam>
        /// <returns>
        ///     The entity with the loaded navigation property.
        /// </returns>
        TEntity LoadRelatedCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> navigationProperty, params Expression<Func<TProperty, object>>[] includes) where TProperty : class;

        /// <summary>
        ///     Loads the collection of entities referenced by the specified navigation property that match the predicate condition.
        /// </summary>
        /// <param name="entity">
        ///     The entity instance with the navigation property reference.
        /// </param>
        /// <param name="navigationProperty">
        ///     The navigation property to be loaded.
        /// </param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <param name="includes">The related entities from the navigation property to be included in the query.</param>
        /// <typeparam name="TProperty">
        ///     The entity type referenced by the navigation property.
        /// </typeparam>
        /// <returns>
        ///     The entity with the loaded navigation property.
        /// </returns>
        TEntity LoadRelatedCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> navigationProperty, Expression<Func<TProperty, bool>> predicate, params Expression<Func<TProperty, object>>[] includes) where TProperty : class;

        /// <summary>
        ///     Asynchronously loads the collection of entities referenced by the specified navigation property.
        /// </summary>
        /// <param name="entity">
        ///     The entity instance with the navigation property reference.
        /// </param>
        /// <param name="navigationProperty">
        ///     The navigation property to load.
        /// </param>
        /// <param name="includes">The related entities from the navigation property to be included in the query.</param>
        /// <typeparam name="TProperty">
        ///     The entity type referenced by the navigation property.
        /// </typeparam>
        /// <returns>
        ///     The task that represents the asynchronous query operation.
        ///     The task result contains the entity with the loaded navigation property.
        /// </returns>
        Task<TEntity> LoadRelatedCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> navigationProperty, params Expression<Func<TProperty, object>>[] includes) where TProperty : class;

        /// <summary>
        ///     Asynchronously loads the collection of entities referenced by the specified navigation property that match the predicate condition.
        /// </summary>
        /// <param name="entity">
        ///     The entity instance with the navigation property reference.
        /// </param>
        /// <param name="navigationProperty">
        ///     The navigation property to load.
        /// </param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <param name="includes">The related entities from the navigation property to be included in the query.</param>
        /// <typeparam name="TProperty">
        ///     The entity type referenced by the navigation property.
        /// </typeparam>
        /// <returns>
        ///     The task that represents the asynchronous query operation.
        ///     The task result contains the entity with the loaded navigation property.
        /// </returns>
        Task<TEntity> LoadRelatedCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> navigationProperty, Expression<Func<TProperty, bool>> predicate, params Expression<Func<TProperty, object>>[] includes) where TProperty : class;
    }
}