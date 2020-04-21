namespace Repositive.Tests.Repository
{
    using System.Linq;
    using Domain.Contracts.Repository;
    using Repositive.Tests.Utilities;
    using Repositive.Tests.Utilities.Repositories.Contracts;
    using Xunit;

    /// <summary>
    ///     Tests the repository methods responsible for adding entities.
    /// </summary>
    public class AddEntityTests
    {
        /// <summary>
        ///     The person repository.
        /// </summary>
        private readonly IPersonRepository _personRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddEntityTests"/> class.
        /// </summary>
        /// <param name="personRepository">
        ///     The person repository.
        /// </param>
        public AddEntityTests(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.Add"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Add_Entity_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.GeneratePersons(1)[0];

            // Act
            person = _personRepository.Add(person);
            var affectedRows = _personRepository.SaveChanges();

            // Assert
            Assert.Equal(1 + person.Vehicles.Count + person.Vehicles.Sum(t => t.Manufacturer != null ? 1 : 0), affectedRows);
            Assert.False(person.Id == default || person.Vehicles.Any(t => t.Id == default || t.Manufacturer.Id == default));
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.AddRange"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Add_Range_Of_Entities_Is_Successful()
        {
            // Arrange
            var personList = DataGenerator.GeneratePersons(50);

            // Act
            _personRepository.AddRange(personList);
            var affectedRows = _personRepository.SaveChanges();

            // Assert
            Assert.True(affectedRows == personList.Count + personList.Sum(t => t.Vehicles.Count + t.Vehicles.Sum(x => x.Manufacturer != null ? 1 : 0)));
            Assert.DoesNotContain(personList, t => t.Id == default || t.Vehicles.Any(x => x.Id == default || x.Manufacturer.Id == default));
        }
    }
}