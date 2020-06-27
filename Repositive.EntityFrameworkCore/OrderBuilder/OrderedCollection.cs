namespace Repositive.EntityFrameworkCore
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Repositive.Abstractions;

    /// <summary>
    ///     Represents a sorted collection and provides methods for composing additional sorting operations.
    /// </summary>
    /// <typeparam name="T">
    ///     The data type of the collection.
    /// </typeparam>
    [ExcludeFromCodeCoverage]
    internal class OrderedCollection<T> : IOrderedCollection<T> where T : class
    {
        /// <summary>
        ///     The query to be sorted.
        /// </summary>
        private readonly IOrderedQueryable<T> _query;

        /// <summary>
        ///     Initializes a new instance of the <see cref="OrderedCollection{T}"/> class.
        /// </summary>
        /// <param name="query">
        ///     The query.
        /// </param>
        private OrderedCollection(IOrderedQueryable<T> query)
        {
            _query = query;
        }

        /// <summary>
        ///     Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <param name="keySelector">
        ///     The function to select the key on which to sort the collection.
        /// </param>
        /// <returns>
        ///     An <see cref="IOrderedCollection{T}"/> for composing additional sorting operations.
        /// </returns>
        public IOrderedCollection<T> ThenBy(Expression<Func<T, object>> keySelector) => From(_query.ThenBy(keySelector));

        /// <summary>
        ///     Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <param name="keySelector">
        ///     The function to select the key on which to sort the collection.
        /// </param>
        /// <returns>
        ///     An <see cref="IOrderedCollection{T}"/> for composing additional sorting operations.
        /// </returns>
        public IOrderedCollection<T> ThenByDescending(Expression<Func<T, object>> keySelector) => From(_query.ThenByDescending(keySelector));

        /// <summary>
        ///     Creates a new instance of the <see cref="OrderedCollection{T}"/> class with specified sorted collection.
        /// </summary>
        /// <param name="query">
        ///     The collection to be sorted.
        /// </param>
        /// <returns>
        ///     The <see cref="OrderedCollection{T}"/> instance.
        /// </returns>
        internal static IOrderedCollection<T> From(IOrderedQueryable<T> query) => new OrderedCollection<T>(query);

        /// <summary>
        ///     Returns the sorted collection.
        /// </summary>
        /// <returns>
        ///     The sorted collection.
        /// </returns>
        internal IQueryable<T> GetCollection() => _query;
    }
}
