namespace Repositive.EntityFrameworkCore.Tests.UnitOfWork
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Repositive.Contracts;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities.Enums;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;
    using Xunit;

    /// <summary>
    ///     Tests the unit of work asynchronous operation.
    /// </summary>
    public class UnitOfWorkAsyncTests
    {
        /// <summary>
        ///     The unit of work.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     The person repository that uses unit of work.
        /// </summary>
        private readonly IPersonUoWRepository _personUoWRepository;

        /// <summary>
        ///     The vehicle repository that uses unit of work.
        /// </summary>
        private readonly IVehicleUoWRepository _vehicleUoWRepository;

        /// <summary>
        ///     The manufacturer repository that uses unit of work.
        /// </summary>
        private readonly IManufacturerUoWRepository _manufacturerUoWRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitOfWorkAsyncTests"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        ///     The unit of work.
        /// </param>
        /// <param name="personUoWRepository">
        ///     The person repository.
        /// </param>
        /// <param name="vehicleUoWRepository">
        ///     The vehicle repository.
        /// </param>
        /// <param name="manufacturerUoWRepository">
        ///     The manufacturer repository.
        /// </param>
        public UnitOfWorkAsyncTests(IUnitOfWork unitOfWork, IPersonUoWRepository personUoWRepository, IVehicleUoWRepository vehicleUoWRepository, IManufacturerUoWRepository manufacturerUoWRepository)
        {
            _unitOfWork = unitOfWork;
            _personUoWRepository = personUoWRepository;
            _vehicleUoWRepository = vehicleUoWRepository;
            _manufacturerUoWRepository = manufacturerUoWRepository;
        }

        /// <summary>
        ///     Asserts that the <see cref="IUnitOfWork.CommitAsync"/> is operating correctly by asynchronously committing changes made to multiple repositories that use unit of work.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Commit_Async_Is_Successful()
        {
            // Arrange
            var person = await _personUoWRepository.AddAsync(new Person { Name = "Foo" }).ConfigureAwait(false);
            var vehicle = await _vehicleUoWRepository.AddAsync(new Vehicle { Type = VehicleType.Car }).ConfigureAwait(false);
            var manufacturer = await _manufacturerUoWRepository.AddAsync(new Manufacturer { Name = "Bar" }).ConfigureAwait(false);

            // Act
            var affectedEntries = await _unitOfWork.CommitAsync().ConfigureAwait(false);

            // Assert
            Assert.Equal(3, affectedEntries);
            Assert.NotEqual(default, person.Id);
            Assert.NotEqual(default, vehicle.Id);
            Assert.NotEqual(default, manufacturer.Id);
        }

        /// <summary>
        ///     Asserts that the <see cref="IUnitOfWork.Committing"/> is invoked when committing changes asynchronously.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Committing_Event_Is_Invoked_On_Commit_Async()
        {
            // Arrange
            await _personUoWRepository.AddAsync(new Person { Name = "Foo" }).ConfigureAwait(false);
            await _vehicleUoWRepository.AddAsync(new Vehicle { Type = VehicleType.Car }).ConfigureAwait(false);
            await _manufacturerUoWRepository.AddAsync(new Manufacturer { Name = "Bar" }).ConfigureAwait(false);

            // Act
            var commitAction = new Func<CancellationToken, Task<int>>(_unitOfWork.CommitAsync);

            // Assert
            await Assert.RaisesAsync<UnitOfWorkCommittingEventArgs>(e => _unitOfWork.Committing += e, e => _unitOfWork.Committing -= e, () => commitAction(default)).ConfigureAwait(false);
        }

        /// <summary>
        ///     Asserts that the <see cref="IUnitOfWork.Committed"/> is invoked when committing changes asynchronously.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Committed_Event_Is_Invoked_On_Commit_Async()
        {
            // Arrange
            await _personUoWRepository.AddAsync(new Person { Name = "Foo" }).ConfigureAwait(false);
            await _vehicleUoWRepository.AddAsync(new Vehicle { Type = VehicleType.Car }).ConfigureAwait(false);
            await _manufacturerUoWRepository.AddAsync(new Manufacturer { Name = "Bar" }).ConfigureAwait(false);

            // Act
            var commitAction = new Func<CancellationToken, Task<int>>(_unitOfWork.CommitAsync);

            // Assert
            await Assert.RaisesAsync<UnitOfWorkCommittedEventArgs>(e => _unitOfWork.Committed += e, e => _unitOfWork.Committed -= e, () => commitAction(default)).ConfigureAwait(false);
        }

        /// <summary>
        ///     Asserts that invoking <see cref="ISaveableRepository.CommitAsync"/> in a repository configured to use unit of work throws <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns>The task representing the asynchronous operation.</returns>
        [Fact]
        public async Task Assert_Repository_Configured_To_Use_Unit_Of_Work_Throws_On_Direct_Commit_Async()
        {
            // Arrange
            await _personUoWRepository.AddAsync(new Person { Name = "Foo" }).ConfigureAwait(false);

            // Act
            var commitAction = new Func<CancellationToken, Task<int>>(_personUoWRepository.CommitAsync);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => commitAction(default)).ConfigureAwait(false);
        }
    }
}
