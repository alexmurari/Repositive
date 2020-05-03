namespace Repositive.Tests.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Repositive.Domain.Contracts.Repository;
    using Repositive.Tests.Utilities;
    using Repositive.Tests.Utilities.Repositories.Contracts;
    using Xunit;

    /// <summary>
    ///     Tests the asynchronous repository methods responsible for deleting entities.
    /// </summary>
    public class DeleteEntityAsyncTests
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
        ///     Initializes a new instance of the <see cref="DeleteEntityAsyncTests"/> class.
        /// </summary>
        /// <param name="personRepository">
        ///     The person repository.
        /// </param>
        /// <param name="databaseHelper">
        ///     The database helper.
        /// </param>
        public DeleteEntityAsyncTests(IPersonRepository personRepository, DatabaseHelper databaseHelper)
        {
            _personRepository = personRepository;
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.DeleteAsync(TEntity, bool)"/> is operating correctly when ignoring related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Delete_Entity_Without_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersons());

            // Act
            await _personRepository.DeleteAsync(person);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.Equal(1, affectedRows);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.DeleteAsync(IEnumerable{TEntity}, bool)"/> is operating correctly when ignoring related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Delete_Entity_Range_Without_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(_databaseHelper.GetPersons(), 10);

            // Act
            await _personRepository.DeleteAsync(persons);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.Equal(persons.Count, affectedRows);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.DeleteAsync(TEntity, bool)"/> is operating correctly when considering related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Delete_Entity_With_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersons());

            // Act
            await _personRepository.DeleteAsync(person, true);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.Equal(person.Vehicles.Count + 1, affectedRows);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.DeleteAsync(IEnumerable{TEntity}, bool)"/> is operating correctly when considering related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Delete_Entity_Range_With_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(_databaseHelper.GetPersons(), 10);

            // Act
            await _personRepository.DeleteAsync(persons, true);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.Equal(persons.Sum(t => t.Vehicles.Count + 1), affectedRows);
        }
    }
}
