namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories
{
    using Repositive.EntityFrameworkCore.Tests.Utilities.Context;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;

    /// <summary>
    ///     Provides a repository pattern implementation for querying and saving <see cref="Vehicle"/> instances.
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
