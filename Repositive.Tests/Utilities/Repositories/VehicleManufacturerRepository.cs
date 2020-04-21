namespace Repositive.Tests.Utilities.Repositories
{
    using Repositive.Repository;
    using Repositive.Tests.Utilities.Context;
    using Repositive.Tests.Utilities.Entities;
    using Repositive.Tests.Utilities.Repositories.Contracts;

    /// <summary>
    ///     Provides an <see cref="IVehicleManufacturerRepository"/> implementation for querying and saving instances of <see cref="VehicleManufacturer"/>.
    /// </summary>
    internal class VehicleManufacturerRepository : GenericEfRepository<VehicleManufacturer>, IVehicleManufacturerRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VehicleManufacturerRepository"/> class.
        /// </summary>
        /// <param name="context">
        ///     The database context.
        /// </param>
        public VehicleManufacturerRepository(RepositiveContext context) : base(context)
        {
        }
    }
}
