namespace Repositive.Tests.Utilities.Repositories
{
    using Repositive.Repository;
    using Repositive.Tests.Utilities.Context;
    using Repositive.Tests.Utilities.Entities;
    using Repositive.Tests.Utilities.Repositories.Contracts;

    /// <summary>
    ///     Provides an <see cref="IVehicleRepository"/> implementation for querying and saving instances of <see cref="Vehicle"/>.
    /// </summary>
    internal class VehicleRepository : GenericEfRepository<Vehicle>, IVehicleRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VehicleRepository"/> class.
        /// </summary>
        /// <param name="context">
        ///     The database context.
        /// </param>
        public VehicleRepository(RepositiveContext context) : base(context)
        {
        }
    }
}
