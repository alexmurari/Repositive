namespace Repositive.Tests.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Repositive.Domain.Contracts.Repository;
    using Repositive.Tests.Utilities;
    using Repositive.Tests.Utilities.Repositories.Contracts;
    using Xunit;

    /// <summary>
    ///     Tests the repository methods responsible for deleting entities.
    /// </summary>
    public class DeleteEntityTests
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
        ///     Initializes a new instance of the <see cref="DeleteEntityTests"/> class.
        /// </summary>
        /// <param name="personRepository">
        ///     The person repository.
        /// </param>
        /// <param name="databaseHelper">
        ///     The database helper.
        /// </param>
        public DeleteEntityTests(IPersonRepository personRepository, DatabaseHelper databaseHelper)
        {
            _personRepository = personRepository;
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.Delete(TEntity, bool)"/> is operating correctly when ignoring related entities.
        /// </summary>
        [Fact]
        public void Assert_Delete_Entity_Without_Related_Entities_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersons());

            // Act
            _personRepository.Delete(person, false);
            var affectedRows = _personRepository.SaveChanges();

            // Assert
            Assert.Equal(1, affectedRows);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.Delete(IEnumerable{TEntity}, bool)"/> is operating correctly when ignoring related entities.
        /// </summary>
        [Fact]
        public void Assert_Delete_Entity_Range_Without_Related_Entities_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(_databaseHelper.GetPersons(), 10);

            // Act
            _personRepository.Delete(persons, false);
            var affectedRows = _personRepository.SaveChanges();

            // Assert
            Assert.Equal(persons.Count, affectedRows);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.Delete(TEntity, bool)"/> is operating correctly when considering related entities.
        /// </summary>
        [Fact]
        public void Assert_Delete_Entity_With_Related_Entities_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersons());

            // Act
            _personRepository.Delete(person);
            var affectedRows = _personRepository.SaveChanges();

            // Assert
            Assert.True(affectedRows > 1);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.Delete(IEnumerable{TEntity}, bool)"/> is operating correctly when considering related entities.
        /// </summary>
        [Fact]
        public void Assert_Delete_Entity_Range_With_Related_Entities_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(_databaseHelper.GetPersons(), 10);

            // Act
            _personRepository.Delete(persons);
            var affectedRows = _personRepository.SaveChanges();

            // Assert
            Assert.True(affectedRows > persons.Count);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.Delete(Expression{Func{TEntity, bool}})"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Delete_Entity_By_Predicate_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersons());

            // Act
            _personRepository.Delete(t => t.Name == person.Name);
            var affectedRows = _personRepository.SaveChanges();

            // Assert
            Assert.True(affectedRows >= 1);
        }
    }
}
