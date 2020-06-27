namespace Repositive.Abstractions
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    ///     Provides methods for sorting elements of a collection.
    /// </summary>
    /// <typeparam name="T">
    ///     The data type of the collection.
    /// </typeparam>
    public interface ICollectionOrderer<T> where T : class
    {
        /// <summary>
        ///     Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <param name="keySelector">
        ///     The function to select the key on which to sort the collection.
        /// </param>
        /// <returns>
        ///     An <see cref="IOrderedCollection{T}"/> for composing additional sorting operations.
        /// </returns>
        IOrderedCollection<T> OrderBy(Expression<Func<T, object>> keySelector);

        /// <summary>
        ///     Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <param name="keySelector">
        ///     The function to select the key on which to sort the collection.
        /// </param>
        /// <returns>
        ///     An <see cref="IOrderedCollection{T}"/> for composing additional sorting operations.
        /// </returns>
        IOrderedCollection<T> OrderByDescending(Expression<Func<T, object>> keySelector);
    }
}