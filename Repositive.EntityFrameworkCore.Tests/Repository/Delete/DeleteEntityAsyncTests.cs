namespace Repositive.EntityFrameworkCore.Tests.Repository
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;
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
        ///     Asserts that the <see cref="IRepository{TEntity}.DeleteAsync(TEntity, bool)"/> is operating correctly when ignoring related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Delete_Entity_Without_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(await _databaseHelper.Query<Person>().ToListAsync());

            // Act
            await _personRepository.DeleteAsync(person).ConfigureAwait(false);
            var affectedEntries = await _personRepository.CommitAsync().ConfigureAwait(false);

            // Assert
            Assert.Equal(1, affectedEntries);
        }

        /// <summary>
        ///     Asserts that the <see cref="IDeletableRepository{TEntity}.DeleteRangeAsync"/> is operating correctly when ignoring related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Delete_Entity_Range_Without_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(await _databaseHelper.Query<Person>().ToListAsync(), 10);

            // Act
            await _personRepository.DeleteRangeAsync(persons).ConfigureAwait(false);
            var affectedEntries = await _personRepository.CommitAsync().ConfigureAwait(false);

            // Assert
            Assert.Equal(persons.Count, affectedEntries);
        }

        /// <summary>
        ///     Asserts that the <see cref="IRepository{TEntity}.DeleteAsync(TEntity, bool)"/> is operating correctly when considering related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Delete_Entity_With_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.PickRandomItem(await _databaseHelper.Query<Person>().Include(t => t.Vehicles).ToListAsync());

            // Act
            await _personRepository.DeleteAsync(person, true).ConfigureAwait(false);
            var affectedEntries = await _personRepository.CommitAsync().ConfigureAwait(false);

            // Assert
            Assert.Equal(person.Vehicles.Count + 1, affectedEntries);
        }

        /// <summary>
        ///     Asserts that the <see cref="IDeletableRepository{TEntity}.DeleteRangeAsync"/> is operating correctly when considering related entities.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Delete_Entity_Range_With_Related_Entities_Async_Is_Successful()
        {
            // Arrange
            var persons = DataGenerator.PickRandomItemRange(await _databaseHelper.Query<Person>().Include(t => t.Vehicles).ToListAsync(), 10);

            // Act
            await _personRepository.DeleteRangeAsync(persons, true).ConfigureAwait(false);
            var affectedEntries = await _personRepository.CommitAsync().ConfigureAwait(false);

            // Assert
            Assert.Equal(persons.Sum(t => t.Vehicles.Count + 1), affectedEntries);
        }
    }
}
