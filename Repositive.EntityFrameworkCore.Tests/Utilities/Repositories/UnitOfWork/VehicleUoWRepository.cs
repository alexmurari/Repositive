namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.UnitOfWork
{
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Context;

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
