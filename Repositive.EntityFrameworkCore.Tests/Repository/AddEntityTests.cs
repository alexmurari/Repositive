namespace Repositive.EntityFrameworkCore.Tests.Repository
{
    using System.Linq;
    using Repositive.Contracts;
    using Repositive.EntityFrameworkCore.Tests.Utilities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Extensions;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;
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
        ///     Asserts that the <see cref="IRepository{TEntity}.Add"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Add_Entity_Is_Successful()
        {
            // Arrange
            var person = DataGenerator.GeneratePersons(1)[0];

            // Act
            person = _personRepository.Add(person);
            var affectedRows = _personRepository.Commit();

            // Assert
            Assert.Equal(person.CountRelatedEntities() + 1, affectedRows);
            Assert.False(person.Id == default);
            Assert.DoesNotContain(person.Vehicles, t => t.Id == default);
            Assert.DoesNotContain(person.Vehicles, t => t.ManufacturerId == default);
            Assert.DoesNotContain(person.Vehicles.SelectMany(t => t.Manufacturer.Subsidiaries), t => t.Id == default);
        }

        /// <summary>
        ///     Asserts that the <see cref="IRepository{TEntity}.AddRange"/> is operating correctly.
        /// </summary>
        [Fact]
        public void Assert_Add_Entity_Range_Is_Successful()
        {
            // Arrange
            var personList = DataGenerator.GeneratePersons(10);

            // Act
            _personRepository.AddRange(personList);
            var affectedRows = _personRepository.Commit();

            // Assert
            Assert.Equal(personList.Sum(t => t.CountRelatedEntities() + 1), affectedRows);
            Assert.DoesNotContain(personList, t => t.Id == default);
            Assert.DoesNotContain(personList.SelectMany(t => t.Vehicles), t => t.Id == default);
            Assert.DoesNotContain(personList.SelectMany(t => t.Vehicles), t => t.ManufacturerId == default);
            Assert.DoesNotContain(personList.SelectMany(t => t.Vehicles).SelectMany(t => t.Manufacturer.Subsidiaries), t => t.Id == default);
        }
    }
}