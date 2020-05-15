namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories
{
    using Repositive.EntityFrameworkCore.Tests.Utilities.Context;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;

    /// <summary>
    ///     Provides a repository pattern implementation for querying and saving <see cref="Manufacturer"/> instances.
    /// </summary>
    internal class ManufacturerRepository : Repository<Manufacturer, RepositiveContext>, IManufacturerRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ManufacturerRepository"/> class.
        /// </summary>
        /// <param name="context">
        ///     The database context.
        /// </param>
        public ManufacturerRepository(RepositiveContext context) : base(context)
        {
        }
    }
}
