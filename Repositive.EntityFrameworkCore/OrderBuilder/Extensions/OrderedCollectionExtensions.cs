namespace Repositive.EntityFrameworkCore
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Repositive.Abstractions;

    /// <summary>
    ///     Provides extension methods to the <see cref="IOrderedCollection{T}"/> interface.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class OrderedCollectionExtensions
    {
        /// <summary>
        ///     Returns the sorted collection.
        /// </summary>
        /// <typeparam name="T">
        ///     The data type of the collection.
        /// </typeparam>
        /// <param name="collection">
        ///     The sorted collection definition.
        /// </param>
        /// <returns>
        ///     The sorted collection.
        /// </returns>
        internal static IQueryable<T> GetCollection<T>(this IOrderedCollection<T> collection) where T : class
        {
            return ((OrderedCollection<T>)collection).GetCollection();
        }
    }
}