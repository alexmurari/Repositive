namespace Repositive.EntityFrameworkCore.Tests.Repository
{
    using System.Linq;
    using System.Threading.Tasks;
    using Repositive.Contracts;
    using Repositive.EntityFrameworkCore.Tests.Utilities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Extensions;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;
    using Xunit;

    /// <summary>
    ///     Tests the asynchronous repository methods responsible for adding entities.
    /// </summary>
    public class AddEntityAsyncTests
    {
        /// <summary>
        ///     The person repository.
        /// </summary>
        private readonly IPersonRepository _personRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddEntityAsyncTests"/> class.
        /// </summary>
        /// <param name="personRepository">
        ///     The person repository.
        /// </param>
        public AddEntityAsyncTests(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        ///     Asserts that the <see cref="IRepository{TEntity}.AddAsync"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Add_Entity_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.GeneratePersons(1)[0];

            // Act
            await _personRepository.AddAsync(person).ConfigureAwait(false);
            var affectedRows = await _personRepository.CommitAsync().ConfigureAwait(false);

            // Assert
            Assert.Equal(person.CountRelatedEntities() + 1, affectedRows);
            Assert.False(person.Id == default);
            Assert.DoesNotContain(person.Vehicles, t => t.Id == default);
            Assert.DoesNotContain(person.Vehicles, t => t.ManufacturerId == default);
            Assert.DoesNotContain(person.Vehicles.SelectMany(t => t.Manufacturer.Subsidiaries), t => t.Id == default);
        }

        /// <summary>
        ///     Asserts that the <see cref="IRepository{TEntity}.AddRangeAsync"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Add_Entity_Range_Async_Is_Successful()
        {
            // Arrange
            var personList = DataGenerator.GeneratePersons(50);

            // Act
            await _personRepository.AddRangeAsync(personList).ConfigureAwait(false);
            var affectedRows = await _personRepository.CommitAsync().ConfigureAwait(false);

            // Assert
            Assert.Equal(personList.Sum(t => t.CountRelatedEntities() + 1), affectedRows);
            Assert.DoesNotContain(personList, t => t.Id == default);
            Assert.DoesNotContain(personList.SelectMany(t => t.Vehicles), t => t.Id == default);
            Assert.DoesNotContain(personList.SelectMany(t => t.Vehicles), t => t.ManufacturerId == default);
            Assert.DoesNotContain(personList.SelectMany(t => t.Vehicles).SelectMany(t => t.Manufacturer.Subsidiaries), t => t.Id == default);
        }
    }
}