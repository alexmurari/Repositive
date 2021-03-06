﻿namespace Repositive.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    ///     Defines a repository contract for reading entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that the repository reads.
    /// </typeparam>
    public interface IReadableRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Determines whether the database repository contains any entities of the provided type.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if the database repository contains any entities; otherwise, <see langword="false"/>.
        /// </returns>
        bool Any();

        /// <summary>
        ///     Determines whether the database repository contains any entities of the provided type that match the predicate condition.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the database repository contains any entities that match the predicate condition; otherwise, <see langword="false"/>.
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
        ///     Computes the average of a sequence of <see cref="int"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>The average of the sequence of values.</returns>
        double Average(Expression<Func<TEntity, int>> selector);

        /// <summary>
        ///     Computes the average of a sequence of <see cref="int"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>The average of the sequence of values.</returns>
        double Average(Expression<Func<TEntity, int>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Computes the average of a sequence of nullable <see cref="int"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
        double? Average(Expression<Func<TEntity, int?>> selector);

        /// <summary>
        ///     Computes the average of a sequence of nullable <see cref="int"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
        double? Average(Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Computes the average of a sequence of <see cref="float"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>The average of the sequence of values.</returns>
        float Average(Expression<Func<TEntity, float>> selector);

        /// <summary>
        ///     Computes the average of a sequence of <see cref="float"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>The average of the sequence of values.</returns>
        float Average(Expression<Func<TEntity, float>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Computes the average of a sequence of nullable <see cref="float"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
        float? Average(Expression<Func<TEntity, float?>> selector);

        /// <summary>
        ///     Computes the average of a sequence of nullable <see cref="float"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
        float? Average(Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Computes the average of a sequence of <see cref="long"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>The average of the sequence of values.</returns>
        double Average(Expression<Func<TEntity, long>> selector);

        /// <summary>
        ///     Computes the average of a sequence of <see cref="long"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>The average of the sequence of values.</returns>
        double Average(Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Computes the average of a sequence of nullable <see cref="long"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
        double? Average(Expression<Func<TEntity, long?>> selector);

        /// <summary>
        ///     Computes the average of a sequence of nullable <see cref="long"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
        double? Average(Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Computes the average of a sequence of <see cref="double"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>The average of the sequence of values.</returns>
        double Average(Expression<Func<TEntity, double>> selector);

        /// <summary>
        ///     Computes the average of a sequence of <see cref="double"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>The average of the sequence of values.</returns>
        double Average(Expression<Func<TEntity, double>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Computes the average of a sequence of nullable <see cref="double"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
        double? Average(Expression<Func<TEntity, double?>> selector);

        /// <summary>
        ///     Computes the average of a sequence of nullable <see cref="double"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
        double? Average(Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Computes the average of a sequence of <see cref="decimal"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>The average of the sequence of values.</returns>
        decimal Average(Expression<Func<TEntity, decimal>> selector);

        /// <summary>
        ///     Computes the average of a sequence of <see cref="decimal"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>The average of the sequence of values.</returns>
        decimal Average(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Computes the average of a sequence of nullable <see cref="decimal"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
        decimal? Average(Expression<Func<TEntity, decimal?>> selector);

        /// <summary>
        ///     Computes the average of a sequence of nullable <see cref="decimal"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
        decimal? Average(Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of <see cref="int"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        Task<double> AverageAsync(Expression<Func<TEntity, int>> selector);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of <see cref="int"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        Task<double> AverageAsync(Expression<Func<TEntity, int>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of nullable <see cref="int"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values, or null if the source sequence is empty or contains only null values.
        /// </returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, int?>> selector);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of nullable <see cref="int"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values, or null if the source sequence is empty or contains only null values.
        /// </returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, int?>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of <see cref="float"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        Task<float> AverageAsync(Expression<Func<TEntity, float>> selector);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of <see cref="float"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        Task<float> AverageAsync(Expression<Func<TEntity, float>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of nullable <see cref="float"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values, or null if the source sequence is empty or contains only null values.
        /// </returns>
        Task<float?> AverageAsync(Expression<Func<TEntity, float?>> selector);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of nullable <see cref="float"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values, or null if the source sequence is empty or contains only null values.
        /// </returns>
        Task<float?> AverageAsync(Expression<Func<TEntity, float?>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of <see cref="long"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        Task<double> AverageAsync(Expression<Func<TEntity, long>> selector);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of <see cref="long"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        Task<double> AverageAsync(Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of nullable <see cref="long"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values, or null if the source sequence is empty or contains only null values.
        /// </returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, long?>> selector);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of nullable <see cref="long"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values, or null if the source sequence is empty or contains only null values.
        /// </returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, long?>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of <see cref="double"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        Task<double> AverageAsync(Expression<Func<TEntity, double>> selector);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of <see cref="double"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        Task<double> AverageAsync(Expression<Func<TEntity, double>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of nullable <see cref="double"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values, or null if the source sequence is empty or contains only null values.
        /// </returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, double?>> selector);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of nullable <see cref="double"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values, or null if the source sequence is empty or contains only null values.
        /// </returns>
        Task<double?> AverageAsync(Expression<Func<TEntity, double?>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of <see cref="decimal"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of <see cref="decimal"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values.
        /// </returns>
        Task<decimal> AverageAsync(Expression<Func<TEntity, decimal>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of nullable <see cref="decimal"/> values that is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values, or null if the source sequence is empty or contains only null values.
        /// </returns>
        Task<decimal?> AverageAsync(Expression<Func<TEntity, decimal?>> selector);

        /// <summary>
        ///     Asynchronously computes the average of a sequence of nullable <see cref="decimal"/> values that match the predicate condition and is obtained by invoking a projection function on each element of the input sequence.
        /// </summary>
        /// <param name="selector">The projection function to apply to each element.</param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the average of the sequence of values, or null if the source sequence is empty or contains only null values.
        /// </returns>
        Task<decimal?> AverageAsync(Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate);

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
        ValueTask<TEntity> FindAsync(params object[] key);

        /// <summary>
        ///     Gets the entities of the provided type from the database.
        /// </summary>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The collection of entities fetched from the database.</returns>
        IEnumerable<TEntity> Get(QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes);

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
        (IEnumerable<TEntity> Entities, int Count) Get(int skip, int take, QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes);

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
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes);

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
        (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Gets the entities of the provided type from the database in an ordered collection.
        /// </summary>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entities returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The collection of entities fetched from the database.</returns>
        IEnumerable<TEntity> Get(
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> predicate,
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        (IEnumerable<TEntity> Entities, int Count) Get(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<IEnumerable<TEntity>> GetAsync(QueryTracking tracking = QueryTracking.Default, params Expression<Func<TEntity, object>>[] includes);

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
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<IEnumerable<TEntity>> GetAsync(
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAsync(
            int skip,
            int take,
            Expression<Func<TEntity, bool>> predicate,
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Gets the first entity of the provided type from the database that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entity returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The entity fetched from the database.</returns>
        TEntity GetSingle(
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Gets the first ordered entity of the provided type from the database.
        /// </summary>
        /// <param name="orderBy">The key and direction to sort the elements.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entity returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>The entity fetched from the database.</returns>
        TEntity GetSingle(
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        TEntity GetSingle(
            Expression<Func<TEntity, bool>> predicate,
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Asynchronously gets the first entity of the provided type from the database that match the predicate condition.
        /// </summary>
        /// <param name="predicate">The predicate with the query condition.</param>
        /// <param name="tracking">
        ///     The query tracking behavior that defines whether or not the entity returned from the query should be tracked by the database context.
        /// </param>
        /// <param name="includes">The related entities to be included in the query.</param>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the entity fetched from the database.
        /// </returns>
        Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> predicate,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<TEntity> GetSingleAsync(
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

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
        Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<ICollectionOrderer<TEntity>, IOrderedCollection<TEntity>> orderBy,
            QueryTracking tracking = QueryTracking.Default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        ///     Invokes a projection function on each entity of the database and returns the maximum resulting value.
        /// </summary>
        /// <param name="selector">
        ///     The transform function to apply to each entity.
        /// </param>
        /// <typeparam name="TResult">
        ///     The resulting type of the projection.
        /// </typeparam>
        /// <returns>
        ///     The maximum resulting value.
        /// </returns>
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector);

        /// <summary>
        ///     Invokes a projection function on each entity of the database that matches the predicate condition and returns the maximum resulting value.
        /// </summary>
        /// <param name="selector">
        ///     The transform function to apply to each entity.
        /// </param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <typeparam name="TResult">
        ///     The resulting type of the projection.
        /// </typeparam>
        /// <returns>
        ///     The maximum resulting value.
        /// </returns>
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously invokes a projection function on each entity of the database and returns the maximum resulting value.
        /// </summary>
        /// <param name="selector">
        ///     The transform function to apply to each entity.
        /// </param>
        /// <typeparam name="TResult">
        ///     The resulting type of the projection.
        /// </typeparam>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The query result contains the maximum resulting value.
        /// </returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector);

        /// <summary>
        ///     Asynchronously invokes a projection function on each entity of the database that matches the predicate condition and returns the maximum resulting value.
        /// </summary>
        /// <param name="selector">
        ///     The transform function to apply to each entity.
        /// </param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <typeparam name="TResult">
        ///     The resulting type of the projection.
        /// </typeparam>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the maximum resulting value.
        /// </returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Invokes a projection function on each entity of the database and returns the minimum resulting value.
        /// </summary>
        /// <param name="selector">
        ///     The transform function to apply to each entity.
        /// </param>
        /// <typeparam name="TResult">
        ///     The resulting type of the projection.
        /// </typeparam>
        /// <returns>
        ///     The minimum resulting value.
        /// </returns>
        TResult Min<TResult>(Expression<Func<TEntity, TResult>> selector);

        /// <summary>
        ///     Invokes a projection function on each entity of the database that matches the predicate condition and returns the minimum resulting value.
        /// </summary>
        /// <param name="selector">
        ///     The transform function to apply to each entity.
        /// </param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <typeparam name="TResult">
        ///     The resulting type of the projection.
        /// </typeparam>
        /// <returns>
        ///     The minimum resulting value.
        /// </returns>
        TResult Min<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///     Asynchronously invokes a projection function on each entity of the database and returns the minimum resulting value.
        /// </summary>
        /// <param name="selector">
        ///     The transform function to apply to each entity.
        /// </param>
        /// <typeparam name="TResult">
        ///     The resulting type of the projection.
        /// </typeparam>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the minimum resulting value.
        /// </returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector);

        /// <summary>
        ///     Asynchronously invokes a projection function on each entity of the database that matches the predicate condition and returns the minimum resulting value.
        /// </summary>
        /// <param name="selector">
        ///     The transform function to apply to each entity.
        /// </param>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <typeparam name="TResult">
        ///     The resulting type of the projection.
        /// </typeparam>
        /// <returns>
        ///     A task that represents the asynchronous query operation.
        ///     The task result contains the minimum resulting value.
        /// </returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate);
    }
}