namespace Repositive.Tests.Repository
{
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Contracts.Repository;
    using Repositive.Tests.Utilities;
    using Repositive.Tests.Utilities.Repositories.Contracts;
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
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.AddAsync"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Add_Entity_Async_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.GeneratePersons(1)[0];

            // Act
            await _personRepository.AddAsync(person);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.Equal(1 + person.Vehicles.Count + person.Vehicles.Sum(t => t.Manufacturer != null ? 1 : 0) + person.Vehicles.Sum(t => t.Manufacturer.Subsidiaries.Count), affectedRows);
            Assert.False(person.Id == default || person.Vehicles.Any(t => t.Id == default || t.Manufacturer.Id == default));
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.AddRangeAsync"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Add_Entity_Range_Async_Is_Successful()
        {
            // Arrange
            var personList = DataGenerator.GeneratePersons(50);

            // Act
            await _personRepository.AddRangeAsync(personList);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.Equal(personList.Count + personList.Sum(t => t.Vehicles.Count + t.Vehicles.Sum(x => x.Manufacturer != null ? 1 : 0) + t.Vehicles.Sum(x => x.Manufacturer.Subsidiaries.Count)), affectedRows);
            Assert.DoesNotContain(personList, t => t.Id == default || t.Vehicles.Any(x => x.Id == default || x.Manufacturer.Id == default));
        }
    }
}