namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories
{
    using Repositive.EntityFrameworkCore;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Context;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;

    /// <summary>
    ///     Provides an <see cref="IVehicleRepository"/> implementation for querying and saving instances of <see cref="Vehicle"/>.
    /// </summary>
    internal class VehicleRepository : Repository<Vehicle, RepositiveContext>, IVehicleRepository
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
