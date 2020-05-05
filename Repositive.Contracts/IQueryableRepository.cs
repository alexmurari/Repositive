namespace Repositive.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    ///     Provides methods for querying instances of <typeparamref name="TEntity"/> using the <see cref="IQueryable{T}"/> interface.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that the repository queries.
    /// </typeparam>
    public interface IQueryableRepository<TEntity> where TEntity : class
    {
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
        IEnumerable<TResult> Query<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        (IEnumerable<TResult> Entities, int Count) Query<TResult>(
            int skip,
            int take,
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<IEnumerable<TResult>> QueryAsync<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<(IEnumerable<TResult> Entities, int Count)> QueryAsync<TResult>(
            int skip,
            int take,
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        TResult QuerySingle<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<TResult> QuerySingleAsync<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);
    }
}
