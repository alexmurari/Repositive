namespace Repositive.Tests.Utilities
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Repositive.Tests.Utilities.Context;
    using Repositive.Tests.Utilities.Entities;

    /// <summary>
    ///     Provides methods for inserting fake data into the database for testing purposes.
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
            var persons = DataGenerator.GeneratePersons();

            _databaseContext.AddRange(persons);
            _databaseContext.SaveChanges();
        }

        /// <summary>
        ///     Gets all the <see cref="Person"/> entities from the database.
        /// </summary>
        /// <returns>
        ///     The collection of entities of type <see cref="Person"/>.
        /// </returns>
        public IList<Person> GetPersons()
        {
            return _databaseContext.Set<Person>().Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer).AsNoTracking().ToList();
        }
    }
}
