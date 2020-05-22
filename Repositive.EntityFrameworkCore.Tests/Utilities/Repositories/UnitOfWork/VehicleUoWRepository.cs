namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories
{
    using Repositive.Contracts;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Context;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;

    /// <summary>
    ///     Provides a repository pattern implementation for querying and saving <see cref="Vehicle"/> instances.
    /// </summary>
    internal class VehicleUoWRepository : Repository<Vehicle, RepositiveUoWContext>, IVehicleUoWRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VehicleUoWRepository"/> class.
        /// </summary>
        /// <param name="context">
        ///     The database context.
        /// </param>
        public VehicleUoWRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
