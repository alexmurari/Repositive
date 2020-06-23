namespace Repositive.EntityFrameworkCore.Tests.Utilities
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Context;

    /// <summary>
    ///     Provides methods for saving and querying fake data from the database for testing purposes.
    /// </summary>
    public class DatabaseHelper
    {
        /// <summary>
        ///     The database context.
        /// </summary>
        private readonly RepositiveContext _databaseContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatabaseHelper"/> class.
        /// </summary>
        /// <param name="databaseContext">
        ///     The database Context.
        /// </param>
        public DatabaseHelper(RepositiveContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <summary>
        ///     Inserts data into the database.
        /// </summary>
        public void InitDatabaseWithData()
        {
            var persons = DataGenerator.GeneratePersons(250);

            _databaseContext.AddRange(persons);
            _databaseContext.SaveChanges();
        }

        /// <summary>
        ///     Gets an empty query for selecting entities of the provided type.
        /// </summary>
        /// <param name="noTracking">
        ///     The value that indicates whether the entities returned from this query should be tracked by the context.
        /// </param>
        /// <typeparam name="TEntity">
        ///     The entity type.
        /// </typeparam>
        /// <returns>
        ///     The database query for selecting instances of <typeparamref name="TEntity"/>.
        /// </returns>
        public IQueryable<TEntity> Query<TEntity>(bool noTracking = true) where TEntity : class => noTracking ? _databaseContext.Set<TEntity>().AsNoTracking() : _databaseContext.Set<TEntity>();
    }
}
