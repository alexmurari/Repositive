namespace Repositive.EntityFrameworkCore
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Repositive.Abstractions;

    /// <summary>
    ///     The collection order builder.
    /// </summary>
    /// <typeparam name="T">
    ///     The data type of the collection.
    /// </typeparam>
    [ExcludeFromCodeCoverage]
    internal class CollectionOrderer<T> : ICollectionOrderer<T> where T : class
    {
        /// <summary>
        ///     The query to be sorted.
        /// </summary>
        private readonly IQueryable<T> _query;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CollectionOrderer{T}"/> class.
        /// </summary>
        /// <param name="query">
        ///     The query.
        /// </param>
        private CollectionOrderer(IQueryable<T> query)
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
        public IOrderedCollection<T> OrderBy(Expression<Func<T, object>> keySelector) => OrderedCollection<T>.From(_query.OrderBy(keySelector));

        /// <summary>
        ///     Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <param name="keySelector">
        ///     The function to select the key on which to sort the collection.
        /// </param>
        /// <returns>
        ///     An <see cref="IOrderedCollection{T}"/> for composing additional sorting operations.
        /// </returns>
        public IOrderedCollection<T> OrderByDescending(Expression<Func<T, object>> keySelector) => OrderedCollection<T>.From(_query.OrderByDescending(keySelector));

        /// <summary>
        ///     Creates a new instance of the <see cref="CollectionOrderer{T}"/> class with specified collection to be sorted.
        /// </summary>
        /// <param name="query">
        ///     The collection to be sorted.
        /// </param>
        /// <returns>
        ///     The order builder instance.
        /// </returns>
        internal static ICollectionOrderer<T> From(IQueryable<T> query) => new CollectionOrderer<T>(query);
    }
}
