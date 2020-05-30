namespace Repositive.EntityFrameworkCore.Tests.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;
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
        ///     Asserts that the <see cref="IRepository{TEntity}.Delete(TEntity, bool)"/> is operating correctly when ignoring related entities.
        /// </summary>
        [Fact]
        public void Assert_Delete_Entity_Without_Related_Entities_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersons());

            // Act
            _personRepository.Delete(person);
            var affectedEntries = _personRepository.Commit();

            // Assert
            Assert.Equal(1, affectedEntries);
        }

        /// <summary>
        ///     Asserts that the <see cref="IRepository{TEntity}.DeleteRange(IEnumerable{TEntity}, bool)"/> is operating correctly when ignoring related entities.
        /// </summary>
        [Fact]
        public void Assert_Delete_Entity_Range_Without_Related_Entities_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(_databaseHelper.GetPersons(), 10);

            // Act
            _personRepository.DeleteRange(persons);
            var affectedEntries = _personRepository.Commit();

            // Assert
            Assert.Equal(persons.Count, affectedEntries);
        }

        /// <summary>
        ///     Asserts that the <see cref="IRepository{TEntity}.Delete(TEntity, bool)"/> is operating correctly when considering related entities.
        /// </summary>
        [Fact]
        public void Assert_Delete_Entity_With_Related_Entities_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersons());

            // Act
            _personRepository.Delete(person, true);
            var affectedEntries = _personRepository.Commit();

            // Assert
            Assert.Equal(person.Vehicles.Count + 1, affectedEntries);
        }

        /// <summary>
        ///     Asserts that the <see cref="IRepository{TEntity}.DeleteRange(IEnumerable{TEntity}, bool)"/> is operating correctly when considering related entities.
        /// </summary>
        [Fact]
        public void Assert_Delete_Entity_Range_With_Related_Entities_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(_databaseHelper.GetPersons(), 10);

            // Act
            _personRepository.DeleteRange(persons, true);
            var affectedEntries = _personRepository.Commit();

            // Assert
            Assert.Equal(persons.Sum(t => t.Vehicles.Count + 1), affectedEntries);
        }
    }
}
