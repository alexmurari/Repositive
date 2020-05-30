namespace Repositive.EntityFrameworkCore.Tests.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
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
            var any = _databaseHelper.GetPersonsWithoutRelated().Any();

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
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersonsWithoutRelated());

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
            var count = _databaseHelper.GetPersonsWithoutRelated().Count;

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
            var count = _databaseHelper.GetPersonsWithoutRelated(predicate).Count;

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
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersonsWithoutRelated());

            // Act
            var result = await _personRepository.FindAsync(person.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal(person.Name, result.Name);
        }
    }
}
