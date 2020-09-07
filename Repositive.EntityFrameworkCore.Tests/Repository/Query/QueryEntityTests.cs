namespace Repositive.EntityFrameworkCore.Tests.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Standard;
    using Xunit;

    /// <summary>
    ///     Tests the repository methods responsible for querying entities.
    /// </summary>
    public class QueryEntityTests
    {
        /// <summary>
        ///     The vehicle repository.
        /// </summary>
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        ///     The database helper.
        /// </summary>
        private readonly DatabaseHelper _databaseHelper;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QueryEntityTests"/> class.
        /// </summary>
        /// <param name="vehicleRepository">
        ///     The vehicle repository.
        /// </param>
        /// <param name="databaseHelper">
        ///     The database helper.
        /// </param>
        public QueryEntityTests(IVehicleRepository vehicleRepository, DatabaseHelper databaseHelper)
        {
            _vehicleRepository = vehicleRepository;
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        ///     Asserts that the <see cref="IQueryableRepository{TEntity}.Query{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, QueryTracking, Expression{Func{TEntity, object}}[])"/>
        /// </summary>
        [Fact]
        public void Assert_Query_Entity_Is_Successful()
        {
            // Arrange
            var vehicle = DataGenerator.PickRandomItem(_databaseHelper.Query<Vehicle>().ToList());

            // Act
            var result = _vehicleRepository.Query(t => t.Where(x => x.Type == vehicle.Type).GroupBy(x => x.Type, (type, vehicles) => new { VehicleType = type, Count = vehicles.Count() }), QueryTracking.NoTracking).ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.DoesNotContain(result, t => t.VehicleType != vehicle.Type);
        }

        /// <summary>
        ///     Asserts that the <see cref="IQueryableRepository{TEntity}.Query{TResult}(int, int, Func{IQueryable{TEntity}, IQueryable{TResult}}, QueryTracking, Expression{Func{TEntity, object}}[])"/>
        /// </summary>
        [Fact]
        public void Assert_Query_Entity_With_Pagination_Is_Successful()
        {
            // Arrange
            var vehicle = DataGenerator.PickRandomItem(_databaseHelper.Query<Vehicle>().ToList());

            // Act
            var (entities, count) = _vehicleRepository.Query(10, 10, vehicles => vehicles.Where(x => x.Type == vehicle.Type).Select(x => new { x.Manufacturer }), QueryTracking.NoTracking, t => t.Manufacturer);
            entities = entities.ToList();

            // Assert
            Assert.NotEmpty(entities);
            Assert.Equal(10, entities.Count());
            Assert.NotEqual(default, count);
        }

        /// <summary>
        ///     Asserts that the <see cref="IQueryableRepository{TEntity}.QuerySingle{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, QueryTracking, Expression{Func{TEntity, object}}[])"/>
        /// </summary>
        [Fact]
        public void Assert_Query_Single_Entity_Is_Successful()
        {
            // Arrange
            var vehicle = DataGenerator.PickRandomItem(_databaseHelper.Query<Vehicle>().ToList());

            // Act
            var result = _vehicleRepository.QuerySingle(vehicles => vehicles.Where(x => x.Type == vehicle.Type).Select(x => new { x.Manufacturer }), QueryTracking.NoTracking, t => t.Manufacturer);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Manufacturer);
        }
    }
}
