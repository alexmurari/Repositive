namespace KraftCore.Repository.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Provides extension methods to the <see cref="DbContext"/> class.
    /// </summary>
    internal static class DbContextExtensions
    {
        /// <summary>
        /// Gets the names of the properties that make up <typeparamref name="TEntity"/> primary key.
        /// </summary>
        /// <param name="context">
        /// The <see cref="DbContext"/> on which to get the entity's primary key from the model metadata.
        /// </param>
        /// <typeparam name="TEntity">
        /// The entity to get the primary key from.
        /// </typeparam>
        /// <returns>
        /// A collection with the names of the properties that make up the primary key.
        /// </returns>
        internal static IEnumerable<string> GetPrimaryKeyNames<TEntity>(this DbContext context)
        {
            return context.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties.Select(t => t.Name);
        }
    }
}
