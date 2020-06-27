namespace Repositive.Abstractions
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    ///     Provides methods for composing additional ordering to elements of a collection.
    /// </summary>
    /// <typeparam name="T">
    ///     The data type of the collection.
    /// </typeparam>
    public interface IOrderedCollection<T> where T : class
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
        IOrderedCollection<T> ThenBy(Expression<Func<T, object>> keySelector);

        /// <summary>
        ///     Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <param name="keySelector">
        ///     The function to select the key on which to sort the collection.
        /// </param>
        /// <returns>
        ///     An <see cref="IOrderedCollection{T}"/> for composing additional sorting operations.
        /// </returns>
        IOrderedCollection<T> ThenByDescending(Expression<Func<T, object>> keySelector);
    }
}