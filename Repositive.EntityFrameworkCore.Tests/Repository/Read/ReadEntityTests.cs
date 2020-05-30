namespace Repositive.EntityFrameworkCore.Tests.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities.Enums;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;
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
            var any = _databaseHelper.GetPersonsWithoutRelated().Any();

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
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersonsWithoutRelated());

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
            var count = _databaseHelper.GetPersonsWithoutRelated().Count;

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
            var count = _databaseHelper.GetPersonsWithoutRelated(predicate).Count;

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
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersonsWithoutRelated());

            // Act
            var result = _personRepository.Find(person.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
            Assert.Equal(person.Name, result.Name);
        }
    }
}
