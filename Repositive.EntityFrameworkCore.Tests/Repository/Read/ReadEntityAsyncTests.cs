namespace Repositive.EntityFrameworkCore.Tests.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities.Enums;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;
    using Xunit;

    /// <summary>
    ///     Tests the asynchronous repository methods responsible for updating entities.
    /// </summary>
    public class ReadEntityAsyncTests
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
        ///     Initializes a new instance of the <see cref="ReadEntityAsyncTests"/> class.
        /// </summary>
        /// <param name="personRepository">
        ///     The person repository.
        /// </param>
        /// <param name="databaseHelper">
        ///     The database helper.
        /// </param>
        public ReadEntityAsyncTests(IPersonRepository personRepository, DatabaseHelper databaseHelper)
        {
            _personRepository = personRepository;
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Any()"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Any_Entity_Async_Is_Successful()
        {
            // Arrange
            var any = _databaseHelper.Query<Person>().Any();

            // Act
            var result = await _personRepository.AnyAsync();

            // Assert
            Assert.Equal(any, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Any(Expression{Func{TEntity, bool}})"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Any_Entity_With_Predicate_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(await _databaseHelper.Query<Person>().ToListAsync());

            // Act
            var result = await _personRepository.AnyAsync(t => t.Name == person.Name);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Count()"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Count_Entity_Async_Is_Successful()
        {
            // Arrange
            var count = await _databaseHelper.Query<Person>().CountAsync();

            // Act
            var result = await _personRepository.CountAsync();

            // Assert
            Assert.Equal(count, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Count(Expression{Func{TEntity, bool}})"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Count_Entity_With_Predicate_Async_Is_Successful()
        {
            // Arrange
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Motorcycle);
            var count = await _databaseHelper.Query<Person>().Where(predicate).CountAsync();

            // Act
            var result = await _personRepository.CountAsync(predicate);

            // Assert
            Assert.Equal(count, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Find(object[])"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Find_Entity_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(await _databaseHelper.Query<Person>().ToListAsync());

            // Act
            var result = await _personRepository.FindAsync(person.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal(person.Name, result.Name);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Get_Entity_Is_Successful()
        {
            // Arrange
            var persons = await _databaseHelper.Query<Person>().Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer).ToListAsync();

            // Act
            var result = (await _personRepository.GetAsync(QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer))).ToList();

            // Assert
            Assert.Equal(persons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(persons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(persons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(int, int, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Get_Entity_With_Pagination_Is_Successful()
        {
            // Arrange
            const int Skip = 10;
            const int Take = 10;

            var persons = _databaseHelper.Query<Person>().Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var totalPersons = await persons.CountAsync();
            var paginatedPersons = await persons.OrderBy(t => t.Id).Skip(Skip).Take(Take).ToListAsync();

            // Act
            var (result, count) = await _personRepository.GetAsync(Skip, Take, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer));
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
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Get_Entity_With_Predicate_Is_Successful()
        {
            // Arrange
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);
            var persons = await _databaseHelper.Query<Person>().Where(predicate).Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer).ToListAsync();

            // Act
            var result = (await _personRepository.GetAsync(predicate, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer))).ToList();

            // Assert
            Assert.Equal(persons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(persons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(persons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(int, int, Expression{Func{TEntity, bool}}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Get_Entity_With_Predicate_And_Pagination_Is_Successful()
        {
            // Arrange
            const int Skip = 10;
            const int Take = 10;
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);

            var persons = _databaseHelper.Query<Person>().Where(predicate).Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var totalPersons = await persons.CountAsync();
            var paginatedPersons = await persons.OrderBy(t => t.Id).Skip(Skip).Take(Take).ToListAsync();

            // Act
            var (result, count) = await _personRepository.GetAsync(Skip, Take, predicate, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer));
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
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Get_Entity_With_Ordering_Is_Successful()
        {
            // Arrange
            (Expression<Func<Person, object>> keySelector, SortDirection direction) orderBy = (t => t.Name, SortDirection.Descending);

            var persons = _databaseHelper.Query<Person>().Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var orderedPersons = orderBy.direction == SortDirection.Ascending ? await persons.OrderBy(orderBy.keySelector).ToListAsync() : await persons.OrderByDescending(orderBy.keySelector).ToListAsync();

            // Act
            var result = (await _personRepository.GetAsync(orderBy, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer))).ToList();

            // Assert
            Assert.Equal(orderedPersons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(orderedPersons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(orderedPersons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(int, int, ValueTuple{Expression{Func{TEntity, object}}, SortDirection}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Get_Entity_With_Ordering_And_Pagination_Is_Successful()
        {
            // Arrange
            const int Skip = 10;
            const int Take = 10;
            (Expression<Func<Person, object>> keySelector, SortDirection direction) orderBy = (t => t.Name, SortDirection.Descending);

            var persons = _databaseHelper.Query<Person>().Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var totalPersons = await persons.CountAsync();
            var orderedPaginatedPersons = await(orderBy.direction == SortDirection.Ascending ? persons.OrderBy(orderBy.keySelector) : persons.OrderByDescending(orderBy.keySelector)).Skip(Skip).Take(Take).ToListAsync();

            // Act
            var (result, count) = await _personRepository.GetAsync(Skip, Take, orderBy, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer));
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
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Get_Entity_With_Predicate_And_Ordering_Is_Successful()
        {
            // Arrange
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);
            (Expression<Func<Person, object>> keySelector, SortDirection direction) orderBy = (t => t.Name, SortDirection.Descending);

            var persons = _databaseHelper.Query<Person>().Where(predicate).Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var orderedPersons = orderBy.direction == SortDirection.Ascending ? await persons.OrderBy(orderBy.keySelector).ToListAsync() : await persons.OrderByDescending(orderBy.keySelector).ToListAsync();

            // Act
            var result = (await _personRepository.GetAsync(predicate, orderBy, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer))).ToList();

            // Assert
            Assert.Equal(orderedPersons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(orderedPersons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(orderedPersons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Get(int, int, ValueTuple{Expression{Func{TEntity, object}}, SortDirection}, QueryTracking, Expression{Func{TEntity, object}}[])"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Get_Entity_With_Predicate_Ordering_And_Pagination_Is_Successful()
        {
            // Arrange
            const int Skip = 10;
            const int Take = 10;
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);
            (Expression<Func<Person, object>> keySelector, SortDirection direction) orderBy = (t => t.Name, SortDirection.Descending);

            var persons = _databaseHelper.Query<Person>().Where(predicate).Include(t => t.Vehicles).ThenInclude(t => t.Manufacturer);
            var totalPersons = await persons.CountAsync();
            var orderedPaginatedPersons = await(orderBy.direction == SortDirection.Ascending ? persons.OrderBy(orderBy.keySelector) : persons.OrderByDescending(orderBy.keySelector)).Skip(Skip).Take(Take).ToListAsync();

            // Act
            var (result, count) = await _personRepository.GetAsync(Skip, Take, predicate, orderBy, QueryTracking.NoTracking, t => t.Vehicles.Select(x => x.Manufacturer));
            result = result.ToList();

            // Assert
            Assert.Equal(orderedPaginatedPersons.Select(t => t.Id), result.Select(t => t.Id));
            Assert.Equal(orderedPaginatedPersons.SelectMany(t => t.Vehicles).Select(t => t.Id), result.SelectMany(t => t.Vehicles).Select(t => t.Id));
            Assert.Equal(orderedPaginatedPersons.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id), result.SelectMany(t => t.Vehicles.Select(x => x.Manufacturer)).Select(t => t.Id));
            Assert.Equal(totalPersons, count);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Max{TResult}(Expression{Func{TEntity, TResult}})"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Max_Entity_Is_Successful()
        {
            // Arrange
            var maxValue = await _databaseHelper.Query<Person>().MaxAsync(t => t.Name.Length);

            // Act
            var result = await _personRepository.MaxAsync(t => t.Name.Length);

            // Assert
            Assert.Equal(maxValue, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Max{TResult}(Expression{Func{TEntity, TResult}}, Expression{Func{TEntity, bool}})"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Max_Entity_With_Predicate_Is_Successful()
        {
            // Arrange
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);
            var maxValue = await _databaseHelper.Query<Person>().Where(predicate).MaxAsync(t => t.Name.Length);

            // Act
            var result = await _personRepository.MaxAsync(t => t.Name.Length, predicate);

            // Assert
            Assert.Equal(maxValue, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Min{TResult}(Expression{Func{TEntity, TResult}})"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Min_Entity_Is_Successful()
        {
            // Arrange
            var minValue = await _databaseHelper.Query<Person>().MinAsync(t => t.Name.Length);

            // Act
            var result = await _personRepository.MinAsync(t => t.Name.Length);

            // Assert
            Assert.Equal(minValue, result);
        }

        /// <summary>
        ///     Asserts that the <see cref="IReadableRepository{TEntity}.Min{TResult}(Expression{Func{TEntity, TResult}}, Expression{Func{TEntity, bool}})"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Min_Entity_With_Predicate_Is_Successful()
        {
            // Arrange
            Expression<Func<Person, bool>> predicate = t => t.Vehicles.Any(x => x.Type == VehicleType.Car);
            var minValue = await _databaseHelper.Query<Person>().Where(predicate).MinAsync(t => t.Name.Length);

            // Act
            var result = await _personRepository.MinAsync(t => t.Name.Length, predicate);

            // Assert
            Assert.Equal(minValue, result);
        }
    }
}
