﻿namespace KraftCore.Domain.Contracts.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    ///     Provides methods for querying and saving instances of <typeparamref name="TEntity" />.
    /// </summary>
    /// <typeparam name="TEntity">The entity type that this repository queries and saves.</typeparam>
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
        ///     Determines whether the database repository contains any entities of the provided type.
        /// </summary>
        /// <returns>
        ///     True if the database repository contains any entities; otherwise, false.
        /// </returns>
        bool Any();

        /// <summary>
        ///     Determines whether the database repository contains any entities of the provided type that match the predicate condition.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     True if the database repository contains any entities that match the predicate condition; otherwise, false.
        /// </returns>
        bool Any(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously determines whether the database repository contains any entities of the provided type.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the value indicating whether database repository contains any entities.
        /// </returns>
        Task<bool> AnyAsync();

        /// <summary>
        ///     Asynchronously determines whether the database repository contains any entities of the provided type that match the predicate condition.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the value indicating whether database repository contains any entities that match the predicate condition.
        /// </returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Returns the number of entities of the provided type from the database repository.
        /// </summary>
        /// <returns>
        ///     The number of entities in the database repository.
        /// </returns>
        int Count();

        /// <summary>
        ///     Returns the number of entities of the provided type from the database repository that match the predicate condition.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     The number of entities in the database repository that match the predicate condition.
        /// </returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously returns the number of entities of the provided type from the database repository.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the number of entities in the database repository.
        /// </returns>
        Task<int> CountAsync();

        /// <summary>
        ///     Asynchronously returns the number of entities of the provided type from the database repository that match the predicate condition.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the number of entities in the database repository that match the predicate condition.
        /// </returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Deletes the entities of the provided type from the database repository that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the delete condition.</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously deletes the entities of the provided type from the database repository that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the delete condition.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Finds an entity of the provided type with the given primary key value.
        /// </summary>
        /// <param name="key">The primary key.</param>
        /// <returns>The entity with the given primary key value.</returns>
        TEntity Find(params object[] key);

        /// <summary>
        ///     Asynchronously finds an entity of the provided type with the given primary key value.
        /// </summary>
        /// <param name="key">The primary key.</param>
        /// <returns>
        ///     A task that represents the asynchronous find operation.
        ///     The task result contains the entity with the given primary key value.
        /// </returns>
        Task<TEntity> FindAsync(params object[] key);

        /// <summary>
        ///     Gets the entities and total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The list of entities fetched by the query and total number of entities of the given type in the database.</returns>
        (IEnumerable<TEntity> Entities, int Count) Get(QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Gets the entities in a paginated collection and total number of elements of the provided type from the database. <br />
        ///     The collection pagination is defined by the number of elements to bypass and return from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     The paginated list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        (IEnumerable<TEntity> Entities, int Count) Get(int skip, int take, QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Gets the entities and total number of elements of the provided type from the database that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     The list of entities fetched by the query and total number of entities of the given type in the database that match the predicate condition.
        /// </returns>
        (IEnumerable<TEntity> Entities, int Count) Get(
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Gets the entities in a paginated collection and total number of elements of the provided type from the database that match the predicate condition. <br />
        ///     The collection pagination is defined by the number of elements to bypass and return from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     The paginated list of entities fetched by the query and total number of entities of the given type in the database that match the predicate condition.
        /// </returns>
        (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Gets the entities and total number of elements of the provided type from the database. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The list of entities fetched by the query and total number of entities of the given type in the database.</returns>
        (IEnumerable<TEntity> Entities, int Count) Get(
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Gets the entities in a paginated collection and total number of elements of the provided type from the database. <br />
        ///     The collection pagination is defined by the number of elements to bypass and return from the database.
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     The paginated list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Gets the entities and total number of elements of the provided type from the database that match the predicate condition. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The list of entities fetched by the query.</returns>
        (IEnumerable<TEntity> Entities, int Count) Get(
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Gets the entities in a paginated collection and total number of elements of the provided type from the database that match the predicate condition. <br />
        ///     The collection pagination is defined by the number of elements to bypass and return from the database.
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     The paginated list of entities fetched by the query and total number of entities of the given type in the database that match the predicate condition.
        /// </returns>
        (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Asynchronously gets the entities and total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Asynchronously gets the entities in a paginated collection and total number of elements of the provided type from the database. <br />
        ///     The collection pagination is defined by the number of elements to bypass and return from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the paginated list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(int skip, int take, QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Asynchronously gets the entities and total number of elements of the provided type from the database that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database that match the predicate condition.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Asynchronously gets the entities in a paginated collection and total number of elements of the provided type from the database that match the predicate condition. <br />
        ///     The collection pagination is defined by the number of elements to bypass and return from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the paginated list of entities fetched by the query and total number of entities of the given type in the database that match the predicate condition.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Asynchronously gets the entities and total number of elements of the provided type from the database. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Asynchronously gets the entities in a paginated collection and total number of elements of the provided type from the database. <br />
        ///     The collection pagination is defined by the number of elements to bypass and return from the database.
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the paginated list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Asynchronously gets the entities and total number of elements of the provided type from the database that match the predicate condition. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database that match the predicate condition.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Asynchronously gets the entities in a paginated collection and total number of elements of the provided type from the database that match the predicate condition. <br />
        ///     The collection pagination is defined by the number of elements to bypass and return from the database.
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the paginated list of entities fetched by the query and total number of entities of the given type in the database that match the predicate condition.
        /// </returns>
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
    }
}