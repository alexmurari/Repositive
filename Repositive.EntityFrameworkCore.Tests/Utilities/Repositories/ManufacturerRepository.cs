namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories
{
    using Repositive.EntityFrameworkCore.Tests.Utilities.Context;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;

    /// <summary>
    ///     Provides an <see cref="IManufacturerRepository"/> implementation for querying and saving instances of <see cref="Manufacturer"/>.
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
