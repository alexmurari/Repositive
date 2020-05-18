namespace Repositive.EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Repositive.Contracts;
    using Repositive.EntityFrameworkCore.Extensions;

    /// <summary>
    ///     Provides a generic repository for querying and saving instances of <typeparamref name="TEntity"/> with <see cref="Microsoft.EntityFrameworkCore"/> as the ORM.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that the repository queries and saves.
    /// </typeparam>
    /// <typeparam name="TContext">
    ///     The type that the repository uses as the database context.
    ///     Must be of <see cref="Microsoft.EntityFrameworkCore.DbContext"/> type or derive from it.
    /// </typeparam>
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>, IQueryableRepository<TEntity>, IRelatedLoadableRepository<TEntity>, ISaveableRepository where TEntity : class where TContext : DbContext
    {
        /// <summary>
        ///     The unit of work. Provides commit synchronization for this and other repositories.
        /// </summary>
        private readonly UnitOfWork<TContext> _unitOfWork;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Repository{TEntity,TContext}" /> class.
        /// </summary>
        /// <param name="context">
        ///     The database context.
        /// </param>
        protected Repository(TContext context)
        {
            DbContext = context.ThrowIfNull(nameof(context));
            DbSet = context.Set<TEntity>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Repository{TEntity,TContext}"/> class with the provided <see cref="IUnitOfWork"/> instance providing commit synchronization between repositories.
        /// </summary>
        /// <param name="unitOfWork">
        ///     The unit of work that provides commit synchronization between repositories.
        /// </param>
        /// <exception cref="InvalidOperationException">Thrown when an invalid <see cref="IUnitOfWork"/> instance is provided as the parameter.</exception>
        protected Repository(IUnitOfWork unitOfWork)
        {
            unitOfWork.ThrowIfNull(nameof(unitOfWork));

            if (!(unitOfWork is UnitOfWork<TContext> uow))
                throw new InvalidOperationException($"The provided {nameof(IUnitOfWork)} instance doesn't match the required instance of {nameof(UnitOfWork<TContext>)} with context of type {typeof(TContext).Name}.");

            var context = uow.GetDbContext();

            DbContext = context;
            DbSet = context.Set<TEntity>();

            uow.AddRepository(GetType().Name);

            _unitOfWork = uow;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="Repository{TEntity,TContext}"/> class. 
        /// </summary>
        ~Repository()
        {
            if (IsUsingUnitOfWork())
                _unitOfWork.RemoveRepository(GetType().Name);
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
        public virtual TEntity Add(TEntity entity)
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
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await DbSet.AddAsync(entity).ConfigureAwait(false)).Entity;
        }

        /// <summary>
        ///     Adds an collection of entities of the provided type to the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be added.</param>
        public virtual void AddRange(IEnumerable<TEntity> entityCollection)
        {
            DbSet.AddRange(entityCollection);
        }

        /// <summary>
        ///     Asynchronously adds an collection of entities of the provided type to the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be added.</param>
        /// <returns>A task that represents the asynchronous add range operation.</returns>
        public virtual Task AddRangeAsync(IEnumerable<TEntity> entityCollection)
        {
            return DbSet.AddRangeAsync(entityCollection);
        }

        /// <summary>
        ///     Determines whether the database repository contains any entities of the provided type.
        /// </summary>
        /// <returns>
        ///     True if the database repository contains any entities; otherwise, false.
        /// </returns>
        public virtual bool Any()
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
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
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
        public virtual Task<bool> AnyAsync()
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
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AnyAsync(predicate);
        }

        /// <summary>
        ///     Returns the number of entities of the provided type from the database repository.
        /// </summary>
        /// <returns>
        ///     The number of entities in the database repository.
        /// </returns>
        public virtual int Count()
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
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
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
        public virtual Task<int> CountAsync()
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
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.CountAsync(predicate);
        }

        /// <summary>
        ///     Deletes the entity of the provided type from the database repository.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <param name="deleteRelated">The value that indicates whether or not related entities reachable from the provided entity should be deleted in the operation.</param>
        public virtual void Delete(TEntity entity, bool deleteRelated = false)
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
        public virtual void Delete(IEnumerable<TEntity> entities, bool deleteRelated = false)
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
        public virtual Task DeleteAsync(TEntity entity, bool deleteRelated = false)
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
        public virtual Task DeleteAsync(IEnumerable<TEntity> entities, bool deleteRelated = false)
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
        public virtual TEntity Find(params object[] key)
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
        public virtual ValueTask<TEntity> FindAsync(params object[] key)
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
        public virtual IEnumerable<TEntity> Get(QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes)
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
        public virtual (IEnumerable<TEntity> Entities, int Count) Get(
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
        public virtual IEnumerable<TEntity> Get(
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
        public virtual (IEnumerable<TEntity> Entities, int Count) Get(
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
        public virtual IEnumerable<TEntity> Get(
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
        public virtual (IEnumerable<TEntity> Entities, int Count) Get(
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
        public virtual IEnumerable<TEntity> Get(
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
        public virtual (IEnumerable<TEntity> Entities, int Count) Get(
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
        public virtual async Task<IEnumerable<TEntity>> GetAsync(QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes)
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
        public virtual async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
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
        public virtual async Task<IEnumerable<TEntity>> GetAsync(
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
        public virtual async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
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
        public virtual async Task<IEnumerable<TEntity>> GetAsync(
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
        public virtual async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
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
        public virtual async Task<IEnumerable<TEntity>> GetAsync(
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
        public virtual async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
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
        public virtual TEntity GetSingle(
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
        public virtual TEntity GetSingle(
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
        public virtual async Task<TEntity> GetSingleAsync(
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
        public virtual async Task<TEntity> GetSingleAsync(
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
        public virtual TEntity LoadRelated<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty, params Expression<Func<TProperty, object>>[] includes)
            where TProperty : class
        {
            var entry = GetEntityEntry(entity);

            BeginEntityTracking(entity);

            entry.Reference(navigationProperty).Query().Include(includes).Load();

            return entry.Entity;
        }

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
        public virtual TEntity LoadRelated<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> navigationProperty,
            Expression<Func<TProperty, bool>> predicate,
            params Expression<Func<TProperty, object>>[] includes) where TProperty : class
        {
            var entry = GetEntityEntry(entity);

            BeginEntityTracking(entity);

            entry.Reference(navigationProperty).Query().Include(includes).Where(predicate).Load();

            return entry.Entity;
        }

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
        public virtual async Task<TEntity> LoadRelatedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> navigationProperty,
            params Expression<Func<TProperty, object>>[] includes) where TProperty : class
        {
            var entry = GetEntityEntry(entity);

            BeginEntityTracking(entity);

            await entry.Reference(navigationProperty).Query().Include(includes).LoadAsync();

            return entry.Entity;
        }

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
        public virtual async Task<TEntity> LoadRelatedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> navigationProperty,
            Expression<Func<TProperty, bool>> predicate,
            params Expression<Func<TProperty, object>>[] includes) where TProperty : class
        {
            var entry = GetEntityEntry(entity);

            BeginEntityTracking(entity);

            await entry.Reference(navigationProperty).Query().Include(includes).Where(predicate).LoadAsync();

            return entry.Entity;
        }

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
        public virtual TEntity LoadRelatedCollection<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> navigationProperty,
            params Expression<Func<TProperty, object>>[] includes) where TProperty : class
        {
            var entry = GetEntityEntry(entity);

            BeginEntityTracking(entity);

            entry.Collection(navigationProperty).Query().Include(includes).Load();

            return entry.Entity;
        }

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
        public virtual TEntity LoadRelatedCollection<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> navigationProperty,
            Expression<Func<TProperty, bool>> predicate,
            params Expression<Func<TProperty, object>>[] includes) where TProperty : class
        {
            var entry = GetEntityEntry(entity);

            BeginEntityTracking(entity);

            entry.Collection(navigationProperty).Query().Include(includes).Where(predicate).Load();

            return entry.Entity;
        }

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
        public virtual async Task<TEntity> LoadRelatedCollectionAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> navigationProperty,
            params Expression<Func<TProperty, object>>[] includes) where TProperty : class
        {
            var entry = GetEntityEntry(entity);

            BeginEntityTracking(entity);

            await entry.Collection(navigationProperty).Query().Include(includes).LoadAsync();

            return entry.Entity;
        }

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
        public virtual async Task<TEntity> LoadRelatedCollectionAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> navigationProperty,
            Expression<Func<TProperty, bool>> predicate,
            params Expression<Func<TProperty, object>>[] includes) where TProperty : class
        {
            var entry = GetEntityEntry(entity);

            BeginEntityTracking(entity);

            await entry.Collection(navigationProperty).Query().Include(includes).Where(predicate).LoadAsync();

            return entry.Entity;
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
        public virtual IEnumerable<TResult> Query<TResult>(
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
        public virtual (IEnumerable<TResult> Entities, int Count) Query<TResult>(
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
        public virtual async Task<IEnumerable<TResult>> QueryAsync<TResult>(
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
        public virtual async Task<(IEnumerable<TResult> Entities, int Count)> QueryAsync<TResult>(
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
        public virtual TResult QuerySingle<TResult>(
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
        public virtual Task<TResult> QuerySingleAsync<TResult>(
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
        public virtual void Update(TEntity entity, bool updateRelated = false)
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
        public virtual Task UpdateAsync(TEntity entity, bool updateRelated = false)
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
        public virtual void UpdateRange(IEnumerable<TEntity> entityCollection, bool updateRelated = false)
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
        public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entityCollection, bool updateRelated = false)
        {
            if (updateRelated)
                DbSet.UpdateRange(entityCollection);
            else
                foreach (var entity in entityCollection)
                    DbContext.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Saves all changes made in this repository to the database.
        /// </summary>
        /// <returns>The number of affected rows in the database.</returns>
        public virtual int SaveChanges()
        {
            if (IsUsingUnitOfWork())
                throw new InvalidOperationException(
                    $"Cannot commit changes directly from repositories that use unit of work. Use the associated {nameof(IUnitOfWork)} instance for commiting changes.");

            return DbContext.SaveChanges();
        }

        /// <summary>
        ///     Asynchronously saves all changes made in this repository to the database.
        /// </summary>
        /// <param name="cancellationToken">
        ///     The token that propagates a cancellation request to interrupt the operation.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous save operation.
        ///     The task result contains the number of affected rows in the database.
        /// </returns>
        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (IsUsingUnitOfWork())
                throw new InvalidOperationException(
                    $"Cannot commit changes directly from repositories that use unit of work. Use the associated {nameof(IUnitOfWork)} instance for commiting changes.");

            return DbContext.SaveChangesAsync(cancellationToken);
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
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the argument passed as the tracking behavior is invalid/out of range.</exception>
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
        ///     Gets the database context entry for the provided entity.
        /// </summary>
        /// <param name="entity">
        ///     The entity to get the context entry.
        /// </param>
        /// <returns>
        ///     The context's entry of the provided entity.
        /// </returns>
        private EntityEntry<TEntity> GetEntityEntry(TEntity entity)
        {
            return DbContext.Entry(entity);
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

        /// <summary>
        ///     Begins tracking the provided entity, if not already being tracked by the database context.
        /// </summary>
        /// <param name="entity">
        ///     The entity to be tracked by the context.
        /// </param>
        private void BeginEntityTracking(TEntity entity)
        {
            var entry = GetEntityEntry(entity);

            if (entry.State == EntityState.Detached)
                entry.State = EntityState.Unchanged;
        }

        /// <summary>
        ///     Gets whether this repository is using unit of work for commit synchronization.
        /// </summary>
        /// <returns>
        ///     True if this repository is using unit of work; otherwise, false.
        /// </returns>
        private bool IsUsingUnitOfWork() => _unitOfWork != null;
    }
}