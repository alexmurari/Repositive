namespace Repositive.EntityFrameworkCore.Tests.UnitOfWork
{
    using System;
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities.Enums;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;
    using Xunit;

    /// <summary>
    ///     Tests the unit of work operation.
    /// </summary>
    public class UnitOfWorkTests
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
        ///     Initializes a new instance of the <see cref="UnitOfWorkTests"/> class.
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
        public UnitOfWorkTests(IUnitOfWork unitOfWork, IPersonUoWRepository personUoWRepository, IVehicleUoWRepository vehicleUoWRepository, IManufacturerUoWRepository manufacturerUoWRepository)
        {
            _unitOfWork = unitOfWork;
            _personUoWRepository = personUoWRepository;
            _vehicleUoWRepository = vehicleUoWRepository;
            _manufacturerUoWRepository = manufacturerUoWRepository;
        }

        /// <summary>
        ///     Asserts that the <see cref="IUnitOfWork.Commit"/> is operating correctly by committing changes made to multiple repositories that use unit of work.
        /// </summary>
        [Fact]
        public void Assert_Commit_Is_Successful()
        {
            // Arrange
            var person = _personUoWRepository.Add(new Person { Name = "Foo" });
            var vehicle = _vehicleUoWRepository.Add(new Vehicle { Type = VehicleType.Car });
            var manufacturer = _manufacturerUoWRepository.Add(new Manufacturer { Name = "Bar" });

            // Act
            var affectedEntries = _unitOfWork.Commit();

            // Assert
            Assert.Equal(3, affectedEntries);
            Assert.NotEqual(default, person.Id);
            Assert.NotEqual(default, vehicle.Id);
            Assert.NotEqual(default, manufacturer.Id);
        }

        /// <summary>
        ///     Asserts that the <see cref="IUnitOfWork.Committing"/> is invoked when committing changes.
        /// </summary>
        [Fact]
        public void Assert_Committing_Event_Is_Invoked_On_Commit()
        {
            // Arrange
            _personUoWRepository.Add(new Person { Name = "Foo" });
            _vehicleUoWRepository.Add(new Vehicle { Type = VehicleType.Car });
            _manufacturerUoWRepository.Add(new Manufacturer { Name = "Bar" });

            // Act
            var commitAction = new Func<int>(_unitOfWork.Commit);

            // Assert
            Assert.Raises<UnitOfWorkCommittingEventArgs>(e => _unitOfWork.Committing += e, e => _unitOfWork.Committing -= e, () => commitAction());
        }
        
        /// <summary>
        ///     Asserts that the <see cref="IUnitOfWork.Committed"/> is invoked when committing changes.
        /// </summary>
        [Fact]
        public void Assert_Committed_Event_Is_Invoked_On_Commit()
        {
            // Arrange
            _personUoWRepository.Add(new Person { Name = "Foo" });
            _vehicleUoWRepository.Add(new Vehicle { Type = VehicleType.Car });
            _manufacturerUoWRepository.Add(new Manufacturer { Name = "Bar" });

            // Act
            var commitAction = new Func<int>(_unitOfWork.Commit);

            // Assert
            Assert.Raises<UnitOfWorkCommittedEventArgs>(e => _unitOfWork.Committed += e, e => _unitOfWork.Committed -= e, () => commitAction());
        }

        /// <summary>
        ///     Asserts that invoking <see cref="ISaveableRepository.Commit"/> in a repository configured to use unit of work throws <see cref="InvalidOperationException"/>.
        /// </summary>
        [Fact]
        public void Assert_Repository_Configured_To_Use_Unit_Of_Work_Throws_On_Direct_Commit()
        {
            // Arrange
            _personUoWRepository.Add(new Person { Name = "Foo" });

            // Act
            var commitAction = new Func<int>(_personUoWRepository.Commit);

            // Assert
            Assert.Throws<InvalidOperationException>(() => commitAction());
        }
    }
}
