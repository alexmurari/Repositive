namespace Repositive.EntityFrameworkCore.Tests.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Standard;
    using Xunit;

    /// <summary>
    ///     Tests the repository methods responsible for updating entities.
    /// </summary>
    public class ReadEntityTests
    {
        /// <summary>
        ///     The person repository.
        /// </summary>
        private readonly IPersonRepository _personRepository;

        /// <summary>
        ///     The database helper.
        /// </summary>
        private readonly DatabaseHelper _databaseHelper;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReadEntityTests"/> class.
        /// </summary>
        /// <param name="personRepository">
        ///     The person repository.
        /// </param>
        /// <param name="databaseHelper">
        ///     The database helper.
        /// </param>
        public ReadEntityTests(IPersonRepository personRepository, DatabaseHelper databaseHelper)
        {
            _personRepository = personRepository;
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Any()"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Any_Entity_Is_Successful()
        {
            // Arrange
            var any = _databaseHelper.Query<Person>().Any();

            // Act
            var result = _personRepository.Any();

            // Assert
            Assert.Equal(any, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Any(Expression{Func{TEntity, bool}})"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Any_Entity_With_Predicate_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.Query<Person>().ToList());

            // Act
            var result = _personRepository.Any(t => t.Name == person.Name);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Count()"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Count_Entity_Is_Successful()
        {
            // Arrange
            var count = _databaseHelper.Query<Person>().Count();

            // Act
            var result = _personRepository.Count();

            // Assert
            Assert.Equal(count, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Count(Expression{Func{TEntity, bool}})"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Count_Entity_With_Predicate_Is_Successful()
        {
            // Arrange
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Motorcycle);
            var count = _databaseHelper.Query<Person>().Where(predicate).Count();

            // Act
            var result = _personRepository.Count(predicate);

            // Assert
            Assert.Equal(count, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Find(object[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Find_Entity_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.Query<Person>().ToList());

            // Act
            var result = _personRepository.Find(person.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal(person.Name, result.Name);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Get_Entity_Is_Successful()
        {
            // Arrange
            var persons = _databaseHelper.Query<Person>().Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer).ToList();

            // Act
            var result = _personRepository.Get(QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer)).ToList();

            // Assert
            Assert.Equal(persons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(persons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(persons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(int, int, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Get_Entity_With_Pagination_Is_Successful()
        {
            // Arrange
            const int Skip = 10;
            const int Take = 10;

            var persons = _databaseHelper.Query<Person>().Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var totalPersons = persons.Count();
            var paginatedPersons = persons.OrderBy(t => t.Id).Skip(Skip).Take(Take).ToList();

            // Act
            var (result, count) = _personRepository.Get(Skip, Take, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer));
            result = result.ToList();

            // Assert
            Assert.Equal(paginatedPersons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(paginatedPersons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(paginatedPersons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
            Assert.Equal(totalPersons, count);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(Expression{Func{TEntity, bool}}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Get_Entity_With_Predicate_Is_Successful()
        {
            // Arrange
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);
            var persons = _databaseHelper.Query<Person>().Where(predicate).Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer).ToList();

            // Act
            var result = _personRepository.Get(predicate, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer)).ToList();

            // Assert
            Assert.Equal(persons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(persons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(persons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(int, int, Expression{Func{TEntity, bool}}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Get_Entity_With_Predicate_And_Pagination_Is_Successful()
        {
            // Arrange
            const int Skip = 10;
            const int Take = 10;
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);

            var persons = _databaseHelper.Query<Person>().Where(predicate).Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var totalPersons = persons.Count();
            var paginatedPersons = persons.OrderBy(t => t.Id).Skip(Skip).Take(Take).ToList();

            // Act
            var (result, count) = _personRepository.Get(Skip, Take, predicate, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer));
            result = result.ToList();

            // Assert
            Assert.Equal(paginatedPersons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(paginatedPersons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(paginatedPersons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
            Assert.Equal(totalPersons, count);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(ValueTuple{Expression{Func{TEntity, object}}, SortDirection}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Get_Entity_With_Ordering_Is_Successful()
        {
            // Arrange
            (Expression<Func<Person, object>> keySelector, SortDirection direction) orderBy = (t => t.Name, SortDirection.Descending);

            var persons = _databaseHelper.Query<Person>().Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var orderedPersons = orderBy.direction == SortDirection.Ascending ? persons.OrderBy(orderBy.keySelector).ToList() : persons.OrderByDescending(orderBy.keySelector).ToList();

            // Act
            var result = _personRepository.Get(orderBy, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer)).ToList();

            // Assert
            Assert.Equal(orderedPersons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(orderedPersons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(orderedPersons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(int, int, ValueTuple{Expression{Func{TEntity, object}}, SortDirection}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Get_Entity_With_Ordering_And_Pagination_Is_Successful()
        {
            // Arrange
            const int Skip = 10;
            const int Take = 10;
            (Expression<Func<Person, object>> keySelector, SortDirection direction) orderBy = (t => t.Name, SortDirection.Descending);

            var persons = _databaseHelper.Query<Person>().Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var totalPersons = persons.Count();
            var orderedPaginatedPersons = (orderBy.direction == SortDirection.Ascending ? persons.OrderBy(orderBy.keySelector) : persons.OrderByDescending(orderBy.keySelector)).Skip(Skip).Take(Take).ToList();

            // Act
            var (result, count) = _personRepository.Get(Skip, Take, orderBy, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer));
            result = result.ToList();

            // Assert
            Assert.Equal(orderedPaginatedPersons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(orderedPaginatedPersons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(orderedPaginatedPersons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
            Assert.Equal(totalPersons, count);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(ValueTuple{Expression{Func{TEntity, object}}, SortDirection}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Get_Entity_With_Predicate_And_Ordering_Is_Successful()
        {
            // Arrange
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);
            (Expression<Func<Person, object>> keySelector, SortDirection direction) orderBy = (t => t.Name, SortDirection.Descending);

            var persons = _databaseHelper.Query<Person>().Where(predicate).Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var orderedPersons = orderBy.direction == SortDirection.Ascending ? persons.OrderBy(orderBy.keySelector).ToList() : persons.OrderByDescending(orderBy.keySelector).ToList();

            // Act
            var result = _personRepository.Get(predicate, orderBy, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer)).ToList();

            // Assert
            Assert.Equal(orderedPersons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(orderedPersons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(orderedPersons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(int, int, ValueTuple{Expression{Func{TEntity, object}}, SortDirection}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Get_Entity_With_Predicate_Ordering_And_Pagination_Is_Successful()
        {
            // Arrange
            const int Skip = 10;
            const int Take = 10;
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);
            (Expression<Func<Person, object>> keySelector, SortDirection direction) orderBy = (t => t.Name, SortDirection.Descending);

            var persons = _databaseHelper.Query<Person>().Where(predicate).Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var totalPersons = persons.Count();
            var orderedPaginatedPersons = (orderBy.direction == SortDirection.Ascending ? persons.OrderBy(orderBy.keySelector) : persons.OrderByDescending(orderBy.keySelector)).Skip(Skip).Take(Take).ToList();

            // Act
            var (result, count) = _personRepository.Get(Skip, Take, predicate, orderBy, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer));
            result = result.ToList();

            // Assert
            Assert.Equal(orderedPaginatedPersons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(orderedPaginatedPersons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(orderedPaginatedPersons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
            Assert.Equal(totalPersons, count);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.GetSingle(Expression{Func{TEntity, bool}}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Asset_Get_Single_Entity_With_Predicate_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.Query<Person>().ToList());

            // Act
            var result = _personRepository.GetSingle(t => t.Id == person.Id && t.Name == person.Name, QueryTracking.TrackAll, t => t.Vehicles);

            // Assert
            Assert.NotNull(person);
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal(person.Name, result.Name);
            Assert.NotEmpty(result.Vehicles);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.GetSingle(ValueTuple{Expression{Func{TEntity, object}}, SortDirection}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Asset_Get_Single_Entity_With_Ordering_Is_Successful()
        {
            // Arrange
            var person = _databaseHelper.Query<Person>().OrderBy(t => t.Name).FirstOrDefault();

            // Act
            var result = _personRepository.GetSingle((t => t.Name, SortDirection.Ascending), QueryTracking.Default, t => t.Vehicles);

            // Assert
            Assert.NotNull(person);
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal(person.Name, result.Name);
            Assert.NotEmpty(result.Vehicles);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.GetSingle(Expression{Func{TEntity, bool}}, ValueTuple{Expression{Func{TEntity, object}}, SortDirection}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Asset_Get_Single_Entity_With_Predicate_And_Ordering_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.Query<Person>().OrderBy(t => t.Name).ToList());

            // Act
            var result = _personRepository.GetSingle(t => t.Id == person.Id && t.Name == person.Name, (t => t.Name, SortDirection.Ascending), QueryTracking.TrackAll, t => t.Vehicles);

            // Assert
            Assert.NotNull(person);
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal(person.Name, result.Name);
            Assert.NotEmpty(result.Vehicles);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Max{TResult}(Expression{Func{TEntity, TResult}})"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Max_Entity_Is_Successful()
        {
            // Arrange
            var maxValue = _databaseHelper.Query<Person>().Max(t => t.Name.Length);

            // Act
            var result = _personRepository.Max(t => t.Name.Length);

            // Assert
            Assert.Equal(maxValue, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Max{TResult}(Expression{Func{TEntity, TResult}}, Expression{Func{TEntity, bool}})"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Max_Entity_With_Predicate_Is_Successful()
        {
            // Arrange
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);
            var maxValue = _databaseHelper.Query<Person>().Where(predicate).Max(t => t.Name.Length);

            // Act
            var result = _personRepository.Max(t => t.Name.Length, predicate);

            // Assert
            Assert.Equal(maxValue, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Min{TResult}(Expression{Func{TEntity, TResult}})"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Min_Entity_Is_Successful()
        {
            // Arrange
            var minValue = _databaseHelper.Query<Person>().Min(t => t.Name.Length);

            // Act
            var result = _personRepository.Min(t => t.Name.Length);

            // Assert
            Assert.Equal(minValue, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Min{TResult}(Expression{Func{TEntity, TResult}}, Expression{Func{TEntity, bool}})"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Min_Entity_With_Predicate_Is_Successful()
        {
            // Arrange
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);
            var minValue = _databaseHelper.Query<Person>().Where(predicate).Min(t => t.Name.Length);

            // Act
            var result = _personRepository.Min(t => t.Name.Length, predicate);

            // Assert
            Assert.Equal(minValue, result);
        }
    }
}
