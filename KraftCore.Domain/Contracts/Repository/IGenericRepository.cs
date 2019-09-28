namespace KraftCore.Domain.Contracts.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    ///     Provides methods for querying and saving instances of <typeparamref name="TEntity" />.
    /// </summary>
    /// <typeparam name="TEntity">The entity type that this repository perform operations.</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
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

        /// <summary>
        ///     Deletes the entities of the provided type from the database repository that contemplates the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the delete condition.</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously deletes the entities of the provided type from the database repository that contemplates the
        ///     predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the delete condition.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Finds an entity of the provided type with the given primary key values.
        /// </summary>
        /// <param name="key">The primary key.</param>
        /// <returns>The entity with the given primary key value(s).</returns>
        TEntity Find(params object[] key);

        /// <summary>
        ///     Asynchronously finds an entity of the provided type with the given primary key values.
        /// </summary>
        /// <param name="key">The primary key.</param>
        /// <returns>
        ///     A task that represents the asynchronous find operation.
        ///     The task result contains the entity with the given primary key value(s).
        /// </returns>
        Task<TEntity> FindAsync(params object[] key);

        /// <summary>
        ///     Gets the entities and total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>The list of entities fetched by the query and total number of entities of the given type in the database.</returns>
        (IEnumerable<TEntity> Entities, int Count) Get(int skip, int take, Expression<Func<TEntity, object>>[] includes = null, bool noTracking = true);

        /// <summary>
        ///     Gets the entities that matches the predicate condition and total number of elements of the provided type from the
        ///     database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>The list of entities fetched by the query and total number of entities of the given type in the database.</returns>
        (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true);

        /// <summary>
        ///     Gets the entities and total number of elements of the provided type from the database. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>The list of entities fetched by the query and total number of entities of the given type in the database.</returns>
        (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true);

        /// <summary>
        ///     Gets the entities that matches the predicate condition and total number of elements of the provided type from the
        ///     database. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>The list of entities fetched by the query.</returns>
        (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true);

        /// <summary>
        ///     Asynchronously gets the entities and total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>
        ///     A task that represents the asynchronous get operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type
        ///     in the database.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(int skip, int take, Expression<Func<TEntity, object>>[] includes = null, bool noTracking = true);

        /// <summary>
        ///     Asynchronously gets the entities that matches the predicate condition and total number of elements of the provided
        ///     type from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>
        ///     A task that represents the asynchronous get operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type
        ///     in the database.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true);

        /// <summary>
        ///     Asynchronously gets the entities and total number of elements of the provided type from the database. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>
        ///     A task that represents the asynchronous get operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type
        ///     in the database.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true);

        /// <summary>
        ///     Asynchronously gets the entities that matches the predicate condition and total number of elements of the provided
        ///     type from the database. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>
        ///     A task that represents the asynchronous get operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type
        ///     in the database.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true);

        /// <summary>
        ///     Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of affected rows in the database.</returns>
        int SaveChanges();

        /// <summary>
        ///     Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous save operation.
        ///     The task result contains the number of affected rows in the database.
        /// </returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        ///     Updates an entity of the provided type in the database repository.
        /// </summary>
        /// <param name="entity">The object to be updated.</param>
        void Update(TEntity entity);

        /// <summary>
        ///     Asynchronously updates an entity of the provided type in the database repository.
        /// </summary>
        /// <param name="entity">The object to be updated.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        ///     Updates an collection of entities of the provided type in the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be updated.</param>
        void UpdateRange(IEnumerable<TEntity> entityCollection);

        /// <summary>
        ///     Asynchronously updates an collection of entities of the provided type in the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be updated.</param>
        /// <returns>A task that represents the asynchronous update range operation.</returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entityCollection);
    }
}