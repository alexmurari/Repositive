namespace Repositive.Tests.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Repositive.Domain.Contracts.Repository;
    using Repositive.Tests.Utilities;
    using Repositive.Tests.Utilities.Entities.Enums;
    using Repositive.Tests.Utilities.Repositories.Contracts;
    using Xunit;

    /// <summary>
    ///     Tests the repository methods responsible for explicitly loading related entities.
    /// </summary>
    public class LoadRelatedEntityAsyncTests
    {
        /// <summary>
        ///     The person repository.
        /// </summary>
        private readonly IPersonRepository _personRepository;

        /// <summary>
        ///     The vehicle repository.
        /// </summary>
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        ///     The database helper.
        /// </summary>
        private readonly DatabaseHelper _databaseHelper;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoadRelatedEntityAsyncTests"/> class.
        /// </summary>
        /// <param name="personRepository">
        ///     The person repository.
        /// </param>
        /// <param name="databaseHelper">
        ///     The database helper.
        /// </param>
        /// <param name="vehicleRepository">
        ///     The vehicle repository.
        /// </param>
        public LoadRelatedEntityAsyncTests(IPersonRepository personRepository, DatabaseHelper databaseHelper, IVehicleRepository vehicleRepository)
        {
            _personRepository = personRepository;
            _databaseHelper = databaseHelper;
            _vehicleRepository = vehicleRepository;
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.LoadRelatedAsync{TProperty}(TEntity, Expression{Func{TEntity, TProperty}}, Expression{Func{TProperty, object}}[])"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Load_Related_Entity_Is_Successful()
        {
            // Arrange
            var vehicle = DataGenerator.PickRandomItem(_databaseHelper.GetVehiclesWithoutRelated());

            // Act
            vehicle = await _vehicleRepository.LoadRelatedAsync(vehicle, t => t.Manufacturer);

            // Assert
            Assert.NotNull(vehicle);
            Assert.NotNull(vehicle.Manufacturer);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.LoadRelatedAsync{TProperty}(TEntity, Expression{Func{TEntity, TProperty}}, Expression{Func{TProperty, object}}[])"/>
        ///     is operating correctly when the include parameter is defined.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Load_Related_Entity_With_Include_Is_Successful()
        {
            // Arrange
            var vehicle = DataGenerator.PickRandomItem(_databaseHelper.GetVehiclesWithoutRelated());

            // Act
            vehicle = await _vehicleRepository.LoadRelatedAsync(vehicle, t => t.Manufacturer, t => t.Subsidiaries);

            // Assert
            Assert.NotNull(vehicle);
            Assert.NotNull(vehicle.Manufacturer);
            Assert.NotEmpty(vehicle.Manufacturer.Subsidiaries);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.LoadRelatedAsync{TProperty}(TEntity, Expression{Func{TEntity, TProperty}}, Expression{Func{TProperty, bool}}, Expression{Func{TProperty, object}}[])"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Load_Related_Entity_With_Predicate_Is_Successful()
        {
            // Arrange
            var vehicle = DataGenerator.PickRandomItem(_databaseHelper.GetVehiclesWithoutRelated(t => t.Manufacturer != null));
            var manufacturerId = vehicle.ManufacturerId;

            // Act
            vehicle = await _vehicleRepository.LoadRelatedAsync(vehicle, t => t.Manufacturer, t => t.Id != manufacturerId);

            // Assert
            Assert.NotNull(vehicle);
            Assert.Null(vehicle.Manufacturer);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.LoadRelatedCollectionAsync{TProperty}(TEntity, Expression{Func{TEntity, IEnumerable{TProperty}}}, Expression{Func{TProperty, object}}[])"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Load_Collection_Of_Related_Entities_Is_Successful()
        {
            // arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersonsWithoutRelated());

            // Act
            person = await _personRepository.LoadRelatedCollectionAsync(person, t => t.Vehicles);

            // Assert
            Assert.NotNull(person);
            Assert.NotEmpty(person.Vehicles);
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.LoadRelatedCollectionAsync{TProperty}(TEntity, Expression{Func{TEntity, IEnumerable{TProperty}}}, Expression{Func{TProperty, object}}[])"/>
        ///     is operating correctly when the include parameter is defined.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Load_Collection_Of_Related_Entities_With_Include_Is_Successful()
        {
            // arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersonsWithoutRelated());

            // Act
            person = await _personRepository.LoadRelatedCollectionAsync(person, t => t.Vehicles, t => t.Manufacturer.Subsidiaries);

            // Assert
            Assert.NotNull(person);
            Assert.NotEmpty(person.Vehicles);
            Assert.DoesNotContain(person.Vehicles, t => t.Manufacturer == null);
            Assert.DoesNotContain(person.Vehicles, t => !t.Manufacturer.Subsidiaries.Any());
        }

        /// <summary>
        ///     Asserts that the <see cref="IGenericRepository{TEntity}.LoadRelatedCollectionAsync{TProperty}(TEntity, Expression{Func{TEntity, IEnumerable{TProperty}}}, Expression{Func{TProperty, bool}}, Expression{Func{TProperty, object}}[])"/> is operating correctly.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Load_Collection_Of_Related_Entities_With_Predicate_Is_Successful()
        {
            // arrange
            var person = DataGenerator.PickRandomItem(_databaseHelper.GetPersonsWithoutRelated(t => t.Vehicles.Any(x => x.Type == VehicleType.Motorcycle)));

            // Act
            person = await _personRepository.LoadRelatedCollectionAsync(person, t => t.Vehicles, t => t.Type == VehicleType.Car);

            // Assert
            Assert.NotNull(person);
            Assert.DoesNotContain(person.Vehicles, t => t.Type == VehicleType.Motorcycle);
        }
    }
}
