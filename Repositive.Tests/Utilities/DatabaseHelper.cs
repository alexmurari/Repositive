namespace Repositive.Tests.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Repositive.Tests.Utilities.Context;
    using Repositive.Tests.Utilities.Entities;

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
        ///     Gets all the <see cref="Person"/> entities from the database.
        /// </summary>
        /// <returns>
        ///     The collection of entities of type <see cref="Person"/>.
        /// </returns>
        public IList<Person> GetPersons()
        {
            return _databaseContext.Set<Person>().Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer).AsNoTracking().ToList();
        }

        /// <summary>
        ///     Gets all the <see cref="Person"/> entities from the database without it's related entities.
        /// </summary>
        /// <returns>
        ///     The collection of entities of type <see cref="Person"/>.
        /// </returns>
        public IList<Person> GetPersonsWithoutRelated()
        {
            return _databaseContext.Set<Person>().AsNoTracking().ToList();
        }

        /// <summary>
        ///     Gets all the <see cref="Person"/> entities that satisfies the predicate condition from the database without it's related entities.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     The collection of entities of type <see cref="Person"/>.
        /// </returns>
        public IList<Person> GetPersonsWithoutRelated(Expression<Func<Person, bool>> predicate)
        {
            return _databaseContext.Set<Person>().AsNoTracking().Where(predicate).ToList();
        }

        /// <summary>
        ///     Gets all the <see cref="Person"/> entities from the database.
        /// </summary>
        /// <returns>
        ///     The collection of entities of type <see cref="Person"/>.
        /// </returns>
        public IList<Vehicle> GetVehicles()
        {
            return _databaseContext.Set<Vehicle>().Include(t => t.Manufacturer).AsNoTracking().ToList();
        }

        /// <summary>
        ///     Gets all the <see cref="Vehicle"/> entities from the database without it's related entities.
        /// </summary>
        /// <returns>
        ///     The collection of entities of type <see cref="Person"/>.
        /// </returns>
        public IList<Vehicle> GetVehiclesWithoutRelated()
        {
            return _databaseContext.Set<Vehicle>().AsNoTracking().ToList();
        }

        /// <summary>
        ///     Gets all the <see cref="Vehicle"/> entities that satisfies the predicate condition from the database without it's related entities.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate with the query condition.
        /// </param>
        /// <returns>
        ///     The collection of entities of type <see cref="Person"/>.
        /// </returns>
        public IList<Vehicle> GetVehiclesWithoutRelated(Expression<Func<Vehicle, bool>> predicate)
        {
            return _databaseContext.Set<Vehicle>().AsNoTracking().Where(predicate).ToList();
        }
    }
}
