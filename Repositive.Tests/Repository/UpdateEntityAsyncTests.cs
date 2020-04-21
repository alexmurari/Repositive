namespace Repositive.Tests.Repository
{
    using System.Threading.Tasks;
    using Repositive.Domain.Contracts.Repository;
    using Repositive.Tests.Utilities;
    using Repositive.Tests.Utilities.Repositories.Contracts;
    using Xunit;

    /// <summary>
    ///     Tests the asynchronous repository methods responsible for updating entities.
    /// </summary>
    public class UpdateEntityAsyncTests
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
        ///     Initializes a new instance of the <see cref="UpdateEntityAsyncTests"/> class.
        /// </summary>
        /// <param name="personRepository">
        ///     The person repository.
        /// </param>
        /// <param name="databaseHelper">
        ///     The database helper.
        /// </param>
        public UpdateEntityAsyncTests(IPersonRepository personRepository, DatabaseHelper databaseHelper)
        {
            _personRepository = personRepository;
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.UpdateAsync"/> is operating correctly when ignoring related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Update_Entity_Without_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersons());

            // Act
            await _personRepository.UpdateAsync(person, false);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.Equal(1, affectedRows);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.UpdateRangeAsync"/> is operating correctly when ignoring related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Update_Range_Of_Entities_Without_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(_databaseHelper.GetPersons(), 50);

            // Act
            await _personRepository.UpdateRangeAsync(persons, false);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.Equal(persons.Count, affectedRows);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.UpdateAsync"/> is operating correctly when considering related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Update_Entity_With_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersons());

            // Act
            await _personRepository.UpdateAsync(person);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.True(affectedRows > 1);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.UpdateRangeAsync"/> is operating correctly when considering related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Update_Range_Of_Entities_With_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(_databaseHelper.GetPersons(), 50);

            // Act
            await _personRepository.UpdateRangeAsync(persons);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.True(affectedRows > persons.Count);
        }
    }
}
