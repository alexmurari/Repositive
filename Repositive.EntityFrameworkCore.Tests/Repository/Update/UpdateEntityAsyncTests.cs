namespace Repositive.EntityFrameworkCore.Tests.Repository
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Standard;
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
        ///     Asserts that the <see cref="IRepository{TEntity}.UpdateAsync"/> is operating correctly when ignoring related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Update_Entity_Without_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(await _databaseHelper.Query<Person>().ToListAsync());

            // Act
            await _personRepository.UpdateAsync(person);
            var affectedEntries = await _personRepository.CommitAsync();

            // Assert
            Assert.Equal(1, affectedEntries);
        }

        /// <summary>
        ///     Asserts that the <see cref="IRepository{TEntity}.UpdateRangeAsync"/> is operating correctly when ignoring related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Update_Entity_Range_Without_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(await _databaseHelper.Query<Person>().ToListAsync(), 50);

            // Act
            await _personRepository.UpdateRangeAsync(persons);
            var affectedEntries = await _personRepository.CommitAsync();

            // Assert
            Assert.Equal(persons.Count, affectedEntries);
        }

        /// <summary>
        ///     Asserts that the <see cref="IRepository{TEntity}.UpdateAsync"/> is operating correctly when considering related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Update_Entity_With_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(await _databaseHelper.Query<Person>().ToListAsync());

            // Act
            await _personRepository.UpdateAsync(person, true);
            var affectedEntries = await _personRepository.CommitAsync();

            // Assert
            Assert.Equal(person.CountRelatedEntities() + 1, affectedEntries);
        }

        /// <summary>
        ///     Asserts that the <see cref="IRepository{TEntity}.UpdateRangeAsync"/> is operating correctly when considering related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Update_Entity_Range_With_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(await _databaseHelper.Query<Person>().ToListAsync(), 50);

            // Act
            await _personRepository.UpdateRangeAsync(persons, true);
            var affectedEntries = await _personRepository.CommitAsync();

            // Assert
            Assert.Equal(persons.Sum(t => t.CountRelatedEntities() + 1), affectedEntries);
        }
    }
}
