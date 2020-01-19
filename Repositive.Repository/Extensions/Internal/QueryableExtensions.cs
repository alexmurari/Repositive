namespace Repositive.Repository.Extensions.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Exprelsior.ExpressionBuilder;
    using Microsoft.EntityFrameworkCore;
    using Repositive.Domain.Contracts;
    using Repositive.Shared.Extensions;

    /// <summary>
    ///     Extension methods for <see cref="IQueryable" /> interface.
    /// </summary>
    internal static class QueryableExtensions
    {
        /// <summary>
        ///     Specifies related entities to include in the query result.
        /// </summary>
        /// <remarks>
        ///     The path expression must be composed of simple property access expressions together with calls to
        ///     <code>Select</code> for composing additional includes after including a collection property.
        /// </remarks>
        /// <example>
        ///     Examples of possible include paths are:
        ///     To include a single reference:
        ///         <c>query.Include(e => e.Level1Reference)</c>
        ///     To include a single collection:
        ///         <c>query.Include(e => e.Level1Collection)</c>
        ///     To include a reference and then a reference one level down:
        ///         <c>query.Include(e => e.Level1Reference.Level2Reference)</c>
        ///     To include a reference and then a collection one level down:
        ///         <c>query.Include(e => e.Level1Reference.Level2Collection)</c>
        ///     To include a collection and then a reference one level down:
        ///         <c>query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference))</c>
        ///     To include a collection and then a collection one level down:
        ///         <c>query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection))</c>
        ///     To include a collection and then a reference one level down:
        ///         <c>query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference))</c>
        ///     To include a collection and then a collection one level down:
        ///         <c>query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection))</c>
        ///     To include a collection, a reference, and a reference two levels down:
        ///         <c>query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference.Level3Reference))</c>
        ///     To include a collection, a collection, and a reference two levels down:
        ///         <c>query.Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection.Select(l2 => l2.Level3Reference)))</c>
        /// </example>
        /// <typeparam name="T">
        ///     The type of entity being queried.
        /// </typeparam>
        /// <param name="source">
        ///     The source <see cref="IQueryable{T}" /> on which to call Include.
        /// </param>
        /// <param name="properties">
        ///     The property access expressions representing the entities to include.
        /// </param>
        /// <returns>
        ///     A new <see cref="IQueryable{T}" /> with the included entities.
        /// </returns>
        internal static IQueryable<T> Include<T>(this IQueryable<T> source, params Expression<Func<T, object>>[] properties) where T : class
        {
            if (properties != null)
                source = properties.Aggregate(source, (current, include) => current.Include(include.AsPath()));

            return source;
        }

        /// <summary>
        ///     Sorts the elements of a sequence according to the specified key and order.
        /// </summary>
        /// <param name="query">
        ///     The source <see cref="IQueryable{T}" /> on which to apply the sorting operation.
        /// </param>
        /// <param name="orderBy">
        ///     The key and direction to sort the elements.
        /// </param>
        /// <typeparam name="TEntity">
        ///     The type of entity being queried.
        /// </typeparam>
        /// <returns>
        ///     A new <see cref="IQueryable{T}" /> with the defined sorting operation.
        /// </returns>
        internal static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, (Expression<Func<TEntity, object>> keySelector, SortDirection direction) orderBy)
        {
            return orderBy.direction == SortDirection.Ascending ? query.OrderBy(orderBy.keySelector) : query.OrderByDescending(orderBy.keySelector);
        }

        /// <summary>
        ///     Sorts the elements of a sequence in ascending order according to a collection of keys.
        /// </summary>
        /// <param name="query">
        ///     The source <see cref="IQueryable{T}" /> on which to apply the sorting operation.
        /// </param>
        /// <param name="keys">
        ///     The collection of property names that the sorting operation uses as the key.
        /// </param>
        /// <typeparam name="TEntity">
        ///     The type of entity being queried.
        /// </typeparam>
        /// <returns>
        ///     A new <see cref="IQueryable{T}" /> with the defined sorting operation.
        /// </returns>
        internal static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, IEnumerable<string> keys)
        {
            if (keys == null)
                return query;

            IOrderedQueryable<TEntity> orderedQuery = null;

            foreach (var key in keys)
            {
                var keySelector = ExpressionBuilder.CreateAccessor<TEntity, object>(key);
                orderedQuery = orderedQuery == null ? query.OrderBy(keySelector) : orderedQuery.ThenBy(keySelector);
            }

            return orderedQuery ?? query;
        }

        /// <summary>
        ///     Sorts the elements of a sequence in descending order according to a collection of keys.
        /// </summary>
        /// <param name="query">
        ///     The source <see cref="IQueryable{T}" /> on which to apply the sorting operation.
        /// </param>
        /// <param name="keys">
        ///     The collection of property names that the sorting operation is using as the key.
        /// </param>
        /// <typeparam name="TEntity">
        ///     The type of entity being queried.
        /// </typeparam>
        /// <returns>
        ///     A new <see cref="IQueryable{T}" /> with the defined sorting operation.
        /// </returns>
        internal static IQueryable<TEntity> OrderByDescending<TEntity>(this IQueryable<TEntity> query, IEnumerable<string> keys)
        {
            if (keys == null)
                return query;

            IOrderedQueryable<TEntity> orderedQuery = null;

            foreach (var key in keys)
            {
                var keySelector = ExpressionBuilder.CreateAccessor<TEntity, object>(key);
                orderedQuery = orderedQuery == null ? query.OrderByDescending(keySelector) : orderedQuery.ThenByDescending(keySelector);
            }

            return orderedQuery ?? query;
        }
    }
}