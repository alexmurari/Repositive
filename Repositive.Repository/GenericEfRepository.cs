namespace Repositive.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repositive.Domain.Contracts;
    using Repositive.Domain.Contracts.Repository;
    using Repositive.Repository.Extensions.Internal;
    using Repositive.Shared.Extensions;

    /// <summary>
    ///     Provides a generic repository implementation for querying and saving instances of
    ///     <typeparamref name="TEntity"/> with <see cref="Microsoft.EntityFrameworkCore"/> as the ORM.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that the repository queries and saves.
    /// </typeparam>
    /// <typeparam name="TContext">
    ///     The type that the repository uses as the database context.
    ///     Must be of <see cref="Microsoft.EntityFrameworkCore.DbContext"/> type or derive from it.
    /// </typeparam>
    public abstract class GenericEfRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericEfRepository{TEntity, TContext}" /> class.
        /// </summary>
        /// <param name="context">
        ///     The database context.
        /// </param>
        protected GenericEfRepository(TContext context)
        {
            DbContext = context.ThrowIfNull(nameof(context));
            DbSet = context.Set<TEntity>();
        }

        /// <summary>
        ///     Gets the database context for this repository.
        /// </summary>
        protected TContext DbContext { get; }

        /// <summary>
        ///     Gets the database set used to query and save instances of <typeparamref name="TEntity" />.
        /// </summary>
        protected DbSet<TEntity> DbSet { get; }

        /// <summary>
        ///     Adds an entity of the provided type to the database repository.
        /// </summary>
        /// <param name="entity">The object to be added.</param>
        /// <returns>The added object.</returns>
        public TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        /// <summary>
        ///     Asynchronously adds an entity of the provided type to the database repository.
        /// </summary>
        /// <param name="entity">The object to be added.</param>
        /// <returns>
        ///     A task that represents the asynchronous add operation.
        ///     The task result contains the added object.
        /// </returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await DbSet.AddAsync(entity).ConfigureAwait(false)).Entity;
        }

        /// <summary>
        ///     Adds an collection of entities of the provided type to the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be added.</param>
        public void AddRange(IEnumerable<TEntity> entityCollection)
        {
            DbSet.AddRange(entityCollection);
        }

        /// <summary>
        ///     Asynchronously adds an collection of entities of the provided type to the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be added.</param>
        /// <returns>A task that represents the asynchronous add range operation.</returns>
        public Task AddRangeAsync(IEnumerable<TEntity> entityCollection)
        {
            return DbSet.AddRangeAsync(entityCollection);
        }

        /// <summary>
        ///     Determines whether the database repository contains any entities of the provided type.
        /// </summary>
        /// <returns>
        ///     True if the database repository contains any entities; otherwise, false.
        /// </returns>
        public bool Any()
        {
            return DbSet.Any();
        }

        /// <summary>
        ///     Determines whether the database repository contains any entities of the provided type that match the predicate condition.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     True if the database repository contains any entities that match the predicate condition; otherwise, false.
        /// </returns>
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        /// <summary>
        ///     Asynchronously determines whether the database repository contains any entities of the provided type.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the value indicating whether database repository contains any entities.
        /// </returns>
        public Task<bool> AnyAsync()
        {
            return DbSet.AnyAsync();
        }

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
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AnyAsync(predicate);
        }

        /// <summary>
        ///     Returns the number of entities of the provided type from the database repository.
        /// </summary>
        /// <returns>
        ///     The number of entities in the database repository.
        /// </returns>
        public int Count()
        {
            return DbSet.Count();
        }

        /// <summary>
        ///     Returns the number of entities of the provided type from the database repository that match the predicate condition.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     The number of entities in the database repository that match the predicate condition.
        /// </returns>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        /// <summary>
        ///     Asynchronously returns the number of entities of the provided type from the database repository.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the number of entities in the database repository.
        /// </returns>
        public Task<int> CountAsync()
        {
            return DbSet.CountAsync();
        }

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
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.CountAsync(predicate);
        }

        /// <summary>
        ///     Deletes the entity of the provided type from the database repository.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <param name="deleteRelated">The value that indicates whether or not related entities reachable from the provided entity should be deleted in the operation.</param>
        public void Delete(TEntity entity, bool deleteRelated = false)
        {
            if (deleteRelated)
                DbSet.Remove(entity);
            else
                DbContext.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        ///     Deletes the entities of the provided type from the database repository.
        /// </summary>
        /// <param name="entities">The collection of entities to be deleted.</param>
        /// <param name="deleteRelated">The value that indicates whether or not related entities reachable from the provided entity should be deleted in the operation.</param>
        public void Delete(IEnumerable<TEntity> entities, bool deleteRelated = false)
        {
            if (deleteRelated)
                DbSet.RemoveRange(entities);
            else
                foreach (var entity in entities)
                    DbContext.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        ///     Asynchronously deletes the entity of the provided type from the database repository.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <param name="deleteRelated">The value that indicates whether or not related entities reachable from the provided entity should be deleted in the operation.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        public Task DeleteAsync(TEntity entity, bool deleteRelated = false)
        {
            if (deleteRelated)
                DbSet.Remove(entity);
            else
                DbContext.Entry(entity).State = EntityState.Deleted;

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Asynchronously deletes the entities of the provided type from the database repository.
        /// </summary>
        /// <param name="entities">The collection of entities to be deleted.</param>
        /// <param name="deleteRelated">The value that indicates whether or not related entities reachable from the provided entity should be deleted in the operation.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        public Task DeleteAsync(IEnumerable<TEntity> entities, bool deleteRelated = false)
        {
            if (deleteRelated)
                DbSet.RemoveRange(entities);
            else
                foreach (var entity in entities)
                    DbContext.Entry(entity).State = EntityState.Deleted;

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Finds an entity of the provided type with the given primary key value.
        /// </summary>
        /// <param name="key">The primary key.</param>
        /// <returns>The entity with the given primary key value.</returns>
        public TEntity Find(params object[] key)
        {
            return DbSet.Find(key);
        }

        /// <summary>
        ///     Asynchronously finds an entity of the provided type with the given primary key value.
        /// </summary>
        /// <param name="key">The primary key.</param>
        /// <returns>
        ///     A task that represents the asynchronous find operation.
        ///     The task result contains the entity with the given primary key value.
        /// </returns>
        public ValueTask<TEntity> FindAsync(params object[] key)
        {
            return DbSet.FindAsync(key);
        }

        /// <summary>
        ///     Gets the entities of the provided type from the database.
        /// </summary>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The collection of entities fetched from the database.</returns>
        public IEnumerable<TEntity> Get(QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes);

            var queryResult = query.ToList();

            return queryResult;
        }

        /// <summary>
        ///     Gets the entities of the provided type from the database. <br />
        ///     The result sequence is paginated and returned along with the total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A tuple with the paginated collection of entities fetched and total number of entities of the provided type in the database.
        /// </returns>
        public (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var count = DbSet.Count();

            var query = GetQuery(tracking).Include(includes).OrderBy(GetEntityPrimaryKeyNames()).Skip(skip).Take(take);

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Gets the entities of the provided type from the database that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     The collection of entities fetched from the database.
        /// </returns>
        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes).Where(predicate);

            var queryResult = query.ToList();

            return queryResult;
        }

        /// <summary>
        ///     Gets the entities of the provided type from the database that match the predicate condition. <br />
        ///     The result sequence is paginated and returned along with the total number of elements of the provided type from the database that match the predicate condition.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A tuple with the paginated collection of entities fetched and total number of entities of the provided type in the database.
        /// </returns>
        public (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var count = DbSet.Where(predicate).Count();

            var query = GetQuery(tracking).Include(includes).Where(predicate).OrderBy(GetEntityPrimaryKeyNames()).Skip(skip).Take(take);

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Gets the entities of the provided type from the database in an ordered collection.
        /// </summary>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The collection of entities fetched from the database.</returns>
        public IEnumerable<TEntity> Get(
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes).OrderBy(orderBy);

            var queryResult = query.ToList();

            return queryResult;
        }

        /// <summary>
        ///     Gets the entities of the provided type from the database. <br />
        ///     The result sequence is ordered, paginated and returned along with the total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A tuple with the paginated collection of entities fetched and total number of entities of the provided type in the database.
        /// </returns>
        public (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var count = DbSet.Count();

            var query = GetQuery(tracking).Include(includes).OrderBy(orderBy).Skip(skip).Take(take);

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Gets the entities of the provided type from the database that match the predicate condition in an ordered result sequence.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The collection of entities fetched from the database.</returns>
        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes).Where(predicate).OrderBy(orderBy);

            var queryResult = query.ToList();

            return queryResult;
        }

        /// <summary>
        ///     Gets the entities of the provided type from the database that match the predicate condition. <br />
        ///     The result sequence is ordered, paginated and returned along with the total number of elements of the provided type from the database that match the predicate condition.
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
        ///     A tuple with the paginated collection of entities fetched and total number of entities of the provided type in the database.
        /// </returns>
        public (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var count = DbSet.Where(predicate).Count();

            var query = GetQuery(tracking).Include(includes).Where(predicate).OrderBy(orderBy).Skip(skip).Take(take);

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Asynchronously gets the entities of the provided type from the database.
        /// </summary>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the collection of entities fetched from the database.
        /// </returns>
        public async Task<IEnumerable<TEntity>> GetAsync(QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return queryResult;
        }

        /// <summary>
        ///     Asynchronously gets the entities of the provided type from the database. <br />
        ///     The result sequence is paginated and returned along with the total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains a tuple with the paginated collection of entities fetched and total number of entities of the provided type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var count = await DbSet.CountAsync().ConfigureAwait(false);

            var query = GetQuery(tracking).Include(includes).OrderBy(GetEntityPrimaryKeyNames()).Skip(skip).Take(take);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Asynchronously gets the entities of the provided type from the database that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the collection of entities fetched from the database.
        /// </returns>
        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes).Where(predicate);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return queryResult;
        }

        /// <summary>
        ///     Asynchronously gets the entities of the provided type from the database that match the predicate condition. <br />
        ///     The result sequence is paginated and returned along with the total number of elements of the provided type from the database that match the predicate condition.
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
        ///     The task result contains a tuple with the paginated collection of entities fetched and total number of entities of the provided type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var count = await DbSet.Where(predicate).CountAsync().ConfigureAwait(false);

            var query = GetQuery(tracking).Include(includes).Where(predicate).OrderBy(GetEntityPrimaryKeyNames()).Skip(skip).Take(take);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Asynchronously gets the entities of the provided type from the database in an ordered collection.
        /// </summary>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the collection of entities fetched from the database.
        /// </returns>
        public async Task<IEnumerable<TEntity>> GetAsync(
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes).OrderBy(orderBy);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return queryResult;
        }

        /// <summary>
        ///     Asynchronously gets the entities of the provided type from the database. <br />
        ///     The result sequence is ordered, paginated and returned along with the total number of elements of the provided type from the database.
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
        ///     The task result contains a tuple with the paginated collection of entities fetched and total number of entities of the provided type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var count = await DbSet.CountAsync().ConfigureAwait(false);

            var query = GetQuery(tracking).Include(includes).OrderBy(orderBy).Skip(skip).Take(take);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Asynchronously gets the entities of the provided type from the database that match the predicate condition in an ordered result sequence.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the collection of entities fetched from the database.
        /// </returns>
        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes).Where(predicate).OrderBy(orderBy);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return queryResult;
        }

        /// <summary>
        ///     Asynchronously gets the entities of the provided type from the database that match the predicate condition. <br />
        ///     The result sequence is ordered, paginated and returned along with the total number of elements of the provided type from the database that match the predicate condition.
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
        ///     The task result contains a tuple with the paginated collection of entities fetched and total number of entities of the provided type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var count = await DbSet.Where(predicate).CountAsync().ConfigureAwait(false);

            var query = GetQuery(tracking).Include(includes).Where(predicate).OrderBy(orderBy).Skip(skip).Take(take);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Gets the first ordered entity of the provided type from the database.
        /// </summary>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entity returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The entity fetched from the database.</returns>
        public TEntity GetSingle(
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes).OrderBy(orderBy);

            var queryResult = query.FirstOrDefault();

            return queryResult;
        }

        /// <summary>
        ///     Gets the first ordered entity of the provided type from the database that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entity returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The entity fetched from the database.</returns>
        public TEntity GetSingle(
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes).Where(predicate).OrderBy(orderBy);

            var queryResult = query.FirstOrDefault();

            return queryResult;
        }

        /// <summary>
        ///     Asynchronously gets the first ordered entity of the provided type from the database.
        /// </summary>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entity returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the entity fetched from the database.
        /// </returns>
        public async Task<TEntity> GetSingleAsync(
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes).OrderBy(orderBy);

            var queryResult = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            return queryResult;
        }

        /// <summary>
        ///     Asynchronously gets the first ordered entity of the provided type from the database that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entity returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the entity fetched from the database.
        /// </returns>
        public async Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetQuery(tracking).Include(includes).Where(predicate).OrderBy(orderBy);

            var queryResult = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            return queryResult;
        }

        /// <summary>
        ///     Queries the database for the provided type and projects each element of the result sequence into a new form.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the query result.
        /// </typeparam>
        /// <param name="queryBuilder">
        ///     The function to configure the query.
        /// </param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">
        ///     The related entities to be included in the query.
        /// </param>
        /// <returns>
        ///     The collection of elements fetched from the database and projected into a new form.
        /// </returns>
        public IEnumerable<TResult> Query<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            queryBuilder.ThrowIfNull(nameof(queryBuilder));

            var query = GetQuery(tracking).Include(includes);

            return queryBuilder(query).ToList();
        }

        /// <summary>
        ///     Queries the database for the provided type and projects each element of the result sequence into a new form. <br/>
        ///     The result sequence is paginated and returned along with the total number of elements of the provided type from the database.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the query result.
        /// </typeparam>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="queryBuilder">
        ///     The function to configure the query.
        /// </param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A tuple with the paginated collection of elements fetched and projected into a new form and the total number of entities of the provided type in the database.
        /// </returns>
        public (IEnumerable<TResult> Entities, int Count) Query<TResult>(
            int skip,
            int take,
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            queryBuilder.ThrowIfNull(nameof(queryBuilder));

            var query = queryBuilder(GetQuery(tracking).Include(includes));

            var count = query.Count();

            var queryResult = query.Skip(skip).Take(take).ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Asynchronously queries the database for the provided type and projects each element of the result sequence into a new form.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the query result.
        /// </typeparam>
        /// <param name="queryBuilder">
        ///     The function to configure the query.
        /// </param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">
        ///     The related entities to be included in the query.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the collection of elements fetched from the database and projected to a new form.
        /// </returns>
        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            queryBuilder.ThrowIfNull(nameof(queryBuilder));

            var query = GetQuery(tracking).Include(includes);

            return await queryBuilder(query).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///     Asynchronously queries the database for the provided type and projects each element of the result sequence into a new form. <br/>
        ///     The result sequence is paginated and returned along with the total number of elements of the provided type from the database.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the query result.
        /// </typeparam>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="queryBuilder">
        ///     The function to configure the query.
        /// </param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains a tuple with the paginated collection of elements fetched and projected into a new form and the total number of entities of the provided type in the database.
        /// </returns>
        public async Task<(IEnumerable<TResult> Entities, int Count)> QueryAsync<TResult>(
            int skip,
            int take,
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            queryBuilder.ThrowIfNull(nameof(queryBuilder));

            var query = queryBuilder(GetQuery(tracking).Include(includes));

            var count = await query.CountAsync().ConfigureAwait(false);

            var queryResult = await query.Skip(skip).Take(take).ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Queries the database for the provided type and projects the first element of the result sequence into a new form.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the query result.
        /// </typeparam>
        /// <param name="queryBuilder">
        ///     The function to configure the query.
        /// </param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">
        ///     The related entities to be included in the query.
        /// </param>
        /// <returns>
        ///     The entity fetched from the database and projected into a new form.
        /// </returns>
        public TResult QuerySingle<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            queryBuilder.ThrowIfNull(nameof(queryBuilder));

            var query = GetQuery(tracking).Include(includes);

            return queryBuilder(query).FirstOrDefault();
        }

        /// <summary>
        ///     Asynchronously queries the database for the provided type and projects the first element of the result sequence into a new form.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the query result.
        /// </typeparam>
        /// <param name="queryBuilder">
        ///     The function to configure the query.
        /// </param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">
        ///     The related entities to be included in the query.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the entity fetched from the database and projected into a new form.
        /// </returns>
        public Task<TResult> QuerySingleAsync<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            queryBuilder.ThrowIfNull(nameof(queryBuilder));

            var query = GetQuery(tracking).Include(includes);

            return queryBuilder(query).FirstOrDefaultAsync();
        }

        /// <summary>
        ///     Updates an entity of the provided type in the database repository.
        /// </summary>
        /// <param name="entity">The object to be updated.</param>
        /// <param name="updateRelated">The value that indicates whether or not related entities reachable from the provided entity should be included in the update operation.</param>
        public void Update(TEntity entity, bool updateRelated = false)
        {
            if (updateRelated)
                DbSet.Update(entity);
            else
                DbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        ///     Asynchronously updates an entity of the provided type in the database repository.
        /// </summary>
        /// <param name="entity">The object to be updated.</param>
        /// <param name="updateRelated">The value that indicates whether or not related entities reachable from the provided entity should be included in the update operation.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        public Task UpdateAsync(TEntity entity, bool updateRelated = false)
        {
            if (updateRelated)
                DbSet.Update(entity);
            else
                DbContext.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Updates an collection of entities of the provided type in the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be updated.</param>
        /// <param name="updateRelated">The value that indicates whether or not related entities reachable from the provided entity should be included in the update operation.</param>
        public void UpdateRange(IEnumerable<TEntity> entityCollection, bool updateRelated = false)
        {
            if (updateRelated)
                DbSet.UpdateRange(entityCollection);
            else
                foreach (var entity in entityCollection)
                    DbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        ///     Asynchronously updates an collection of entities of the provided type in the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be updated.</param>
        /// <param name="updateRelated">The value that indicates whether or not related entities reachable from the provided entity should be included in the update operation.</param>
        /// <returns>A task that represents the asynchronous update range operation.</returns>
        public Task UpdateRangeAsync(IEnumerable<TEntity> entityCollection, bool updateRelated = false)
        {
            if (updateRelated)
                DbSet.UpdateRange(entityCollection);
            else
                foreach (var entity in entityCollection)
                    DbContext.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of affected rows in the database.</returns>
        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        /// <summary>
        ///     Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        ///     A task that represents the asynchronous save operation.
        ///     The task result contains the number of affected rows in the database.
        /// </returns>
        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Returns a new query with a configured change tracker.
        /// </summary>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <returns>
        ///     A new <see cref="IQueryable{T}" /> with the configured change tracker behavior.
        /// </returns>
        private IQueryable<TEntity> GetQuery(QueryTracking tracking)
        {
            switch (tracking)
            {
                case QueryTracking.Default:
                    return DbSet.AsQueryable();

                case QueryTracking.NoTracking:
                    return DbSet.AsNoTracking();

                case QueryTracking.TrackAll:
                    return DbSet.AsTracking();

                default:
                    throw new ArgumentOutOfRangeException(nameof(tracking), tracking, null);
            }
        }

        /// <summary>
        ///     Gets the names of the properties that make up <typeparamref name="TEntity" /> primary key.
        /// </summary>
        /// <returns>
        ///     A collection with the names of the properties that make up <typeparamref name="TEntity" /> the primary key.
        /// </returns>
        private IEnumerable<string> GetEntityPrimaryKeyNames()
        {
            return DbContext.GetPrimaryKey<TEntity>().Select(t => t.Name);
        }
    }
}