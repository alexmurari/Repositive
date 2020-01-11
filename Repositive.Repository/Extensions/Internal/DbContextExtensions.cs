namespace Repositive.Repository.Extensions.Internal
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    /// <summary>
    ///     Provides extension methods to the <see cref="DbContext" /> class.
    /// </summary>
    internal static class DbContextExtensions
    {
        /// <summary>
        ///     Gets the properties that make up <typeparamref name="TEntity" /> primary key.
        /// </summary>
        /// <param name="context">
        ///     The <see cref="DbContext" /> on which to get the entity's primary key from the model metadata.
        /// </param>
        /// <typeparam name="TEntity">
        ///     The entity to get the primary key from.
        /// </typeparam>
        /// <returns>
        ///     A collection with the properties that make up <typeparamref name="TEntity"/> the primary key.
        /// </returns>
        internal static IReadOnlyList<IProperty> GetPrimaryKey<TEntity>(this DbContext context)
        {
            return context.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties;
        }
    }
}