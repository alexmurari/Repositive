namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts
{
    using Repositive.Contracts;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;

    /// <summary>
    ///     Provides methods for querying and saving instances of <see cref="Manufacturer"/>.
    /// </summary>
    public interface IManufacturerRepository : IRepository<Manufacturer>, IRelatedLoadableRepository<Manufacturer>
    {
    }
}
