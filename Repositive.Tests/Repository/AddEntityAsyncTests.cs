namespace Repositive.Tests.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Contracts.Repository;
    using Repositive.Tests.Utilities.Entities;
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
            var person = new Person { Name = "Alex" };

            // Act
            await _personRepository.AddAsync(person);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.Equal(1, affectedRows);
            Assert.False(person.Id == default);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.AddRangeAsync"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Add_Range_Of_Entities_Async_Is_Successful()
        {
            // Arrange
            var personList = new List<Person> { new Person { Name = "Alex" }, new Person { Name = "Gabriela" }, new Person { Name = "Snow" } };

            // Act
            await _personRepository.AddRangeAsync(personList);
            var affectedRows = await _personRepository.SaveChangesAsync();

            // Assert
            Assert.Equal(personList.Count, affectedRows);
            Assert.DoesNotContain(personList, t => t.Id == default);
        }
    }
}