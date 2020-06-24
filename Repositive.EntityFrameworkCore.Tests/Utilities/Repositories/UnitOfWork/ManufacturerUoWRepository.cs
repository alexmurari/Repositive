namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.UnitOfWork
{
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Context;

    /// <summary>
    ///     Provides a repository pattern implementation for querying and saving <see cref="Manufacturer"/> instances.
    /// </summary>
    internal class ManufacturerUoWRepository : Repository<Manufacturer, RepositiveUoWContext>, IManufacturerUoWRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ManufacturerUoWRepository"/> class.
        /// </summary>
        /// <param name="context">
        ///     The database context.
        /// </param>
        public ManufacturerUoWRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
