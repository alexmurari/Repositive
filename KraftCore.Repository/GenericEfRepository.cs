namespace KraftCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using KraftCore.Domain.Contracts;
    using KraftCore.Domain.Contracts.Repository;
    using KraftCore.Repository.Internal;
    using KraftCore.Shared.Extensions;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Base class for generic repositories that queries and saves instances of <typeparamref name="TEntity" />.
    ///     This repository uses <see cref="Microsoft.EntityFrameworkCore" /> as the ORM.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that this repository queries and saves.
    /// </typeparam>
    public abstract class GenericEfRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericEfRepository{TEntity}" /> class.
        /// </summary>
        /// <param name="context">
        ///     The database context.
        /// </param>
        protected GenericEfRepository(DbContext context)
        {
            DbContext = context.ThrowIfNull(nameof(context));
            DbSet = context.Set<TEntity>();
        }

        /// <summary>
        ///     Gets the database context for this repository.
        /// </summary>
        protected DbContext DbContext { get; }

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
        ///     Deletes the entities of the provided type from the database repository that contemplates the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the delete condition.</param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            DbSet.RemoveRange(DbSet.Where(predicate));
        }

        /// <summary>
        ///     Asynchronously deletes the entities of the provided type from the database repository that contemplates the
        ///     predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the delete condition.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            DbSet.RemoveRange(await DbSet.Where(predicate).ToListAsync().ConfigureAwait(false));
        }

        /// <summary>
        ///     Finds an entity of the provided type with the given primary key values.
        /// </summary>
        /// <param name="key">The primary key.</param>
        /// <returns>The entity with the given primary key value(s).</returns>
        public TEntity Find(params object[] key)
        {
            return DbSet.Find(key);
        }

        /// <summary>
        ///     Asynchronously finds an entity of the provided type with the given primary key values.
        /// </summary>
        /// <param name="key">The primary key.</param>
        /// <returns>
        ///     A task that represents the asynchronous find operation.
        ///     The task result contains the entity with the given primary key value(s).
        /// </returns>
        public Task<TEntity> FindAsync(params object[] key)
        {
            return DbSet.FindAsync();
        }

        /// <summary>
        ///     Gets the entities and total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>The list of entities fetched by the query and total number of entities of the given type in the database.</returns>
        public (IEnumerable<TEntity> Entities, int Count) Get(Expression<Func<TEntity, object>>[] includes = null, bool noTracking = true)
        {
            var count = DbSet.Count();

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).OrderBy(GetEntityPrimaryKeyNames());

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Gets the entities and total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>The list of entities fetched by the query and total number of entities of the given type in the database.</returns>
        public (IEnumerable<TEntity> Entities, int Count) Get(int skip, int take, Expression<Func<TEntity, object>>[] includes = null, bool noTracking = true)
        {
            var count = DbSet.Count();

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).OrderBy(GetEntityPrimaryKeyNames()).Skip(skip).Take(take);

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Asynchronously gets the entities and total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>
        ///     A task that represents the asynchronous get operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(Expression<Func<TEntity, object>>[] includes = null, bool noTracking = true)
        {
            var count = await DbSet.CountAsync().ConfigureAwait(false);

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).OrderBy(GetEntityPrimaryKeyNames());

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Asynchronously gets the entities and total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>
        ///     A task that represents the asynchronous get operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(int skip, int take, Expression<Func<TEntity, object>>[] includes = null, bool noTracking = true)
        {
            var count = await DbSet.CountAsync().ConfigureAwait(false);

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).OrderBy(GetEntityPrimaryKeyNames()).Skip(skip).Take(take);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Gets the entities that matches the predicate condition and total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>The list of entities fetched by the query and total number of entities of the given type in the database.</returns>
        public (IEnumerable<TEntity> Entities, int Count) Get(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes = null, bool noTracking = true)
        {
            var count = DbSet.Count();

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).Where(predicate).OrderBy(GetEntityPrimaryKeyNames());

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Gets the entities that matches the predicate condition and total number of elements of the provided type from the database.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>The list of entities fetched by the query and total number of entities of the given type in the database.</returns>
        public (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true)
        {
            var count = DbSet.Count();

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes)
                .Where(predicate)
                .OrderBy(GetEntityPrimaryKeyNames())
                .Skip(skip)
                .Take(take);

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Asynchronously gets the entities that matches the predicate condition and total number of elements of the provided
        ///     type from the database.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>
        ///     A task that represents the asynchronous get operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true)
        {
            var count = await DbSet.CountAsync().ConfigureAwait(false);

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes)
                .Where(predicate)
                .OrderBy(GetEntityPrimaryKeyNames());

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

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
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true)
        {
            var count = await DbSet.CountAsync().ConfigureAwait(false);

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes)
                .Where(predicate)
                .OrderBy(GetEntityPrimaryKeyNames())
                .Skip(skip)
                .Take(take);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Gets the entities and total number of elements of the provided type from the database. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>The list of entities fetched by the query and total number of entities of the given type in the database.</returns>
        public (IEnumerable<TEntity> Entities, int Count) Get(
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true)
        {
            var count = DbSet.Count();

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).OrderBy(orderBy);

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

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
        public (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true)
        {
            var count = DbSet.Count();

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).OrderBy(orderBy).Skip(skip).Take(take);

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Asynchronously gets the entities and total number of elements of the provided type from the database. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>
        ///     A task that represents the asynchronous get operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true)
        {
            var count = await DbSet.CountAsync().ConfigureAwait(false);

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).OrderBy(orderBy);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

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
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true)
        {
            var count = await DbSet.CountAsync().ConfigureAwait(false);

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).OrderBy(orderBy).Skip(skip).Take(take);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Gets the entities that matches the predicate condition and total number of elements of the provided type from the database. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>The list of entities fetched by the query.</returns>
        public (IEnumerable<TEntity> Entities, int Count) Get(
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true)
        {
            var count = DbSet.Count();

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).Where(predicate).OrderBy(orderBy);

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Gets the entities that matches the predicate condition and total number of elements of the provided type from the database. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="skip">The number of contiguous elements to be bypassed when querying the database.</param>
        /// <param name="take">The number of contiguous elements to be returned when querying the database.</param>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>The list of entities fetched by the query.</returns>
        public (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true)
        {
            var count = DbSet.Count();

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).Where(predicate).OrderBy(orderBy).Skip(skip).Take(take);

            var queryResult = query.ToList();

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Asynchronously gets the entities that matches the predicate condition and total number of elements of the provided type from the database. <br />
        ///     The elements are ordered by the specified key and direction.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <param name="noTracking">Informs whether the context should track the objects returned in this query.</param>
        /// <returns>
        ///     A task that represents the asynchronous get operation.
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true)
        {
            var count = await DbSet.CountAsync().ConfigureAwait(false);

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).Where(predicate).OrderBy(orderBy);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Asynchronously gets the entities that matches the predicate condition and total number of elements of the provided type from the database. <br />
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
        ///     The task result contains the list of entities fetched by the query and total number of entities of the given type in the database.
        /// </returns>
        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy,
            Expression<Func<TEntity, object>>[] includes = null,
            bool noTracking = true)
        {
            var count = await DbSet.CountAsync().ConfigureAwait(false);

            var query = (noTracking ? DbSet.AsNoTracking() : DbSet.AsTracking()).Include(includes).Where(predicate).OrderBy(orderBy).Skip(skip).Take(take);

            var queryResult = await query.ToListAsync().ConfigureAwait(false);

            return (Entities: queryResult, Count: count);
        }

        /// <summary>
        ///     Updates an entity of the provided type in the database repository.
        /// </summary>
        /// <param name="entity">The object to be updated.</param>
        public void Update(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                DbSet.Update(entity);
        }

        /// <summary>
        ///     Asynchronously updates an entity of the provided type in the database repository.
        /// </summary>
        /// <param name="entity">The object to be updated.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        public Task UpdateAsync(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                DbSet.Update(entity);

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Updates an collection of entities of the provided type in the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be updated.</param>
        public void UpdateRange(IEnumerable<TEntity> entityCollection)
        {
            DbSet.UpdateRange(entityCollection.Where(t => DbContext.Entry(t).State == EntityState.Detached));
        }

        /// <summary>
        ///     Asynchronously updates an collection of entities of the provided type in the database repository.
        /// </summary>
        /// <param name="entityCollection">The collection of objects to be updated.</param>
        /// <returns>A task that represents the asynchronous update range operation.</returns>
        public Task UpdateRangeAsync(IEnumerable<TEntity> entityCollection)
        {
            DbSet.UpdateRange(entityCollection.Where(t => DbContext.Entry(t).State == EntityState.Detached));

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
        ///     Gets the names of properties that make up <typeparamref name="TEntity" /> primary key.
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