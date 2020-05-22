namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts
{
    using Repositive.Contracts;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;

    /// <summary>
    ///     Provides repository methods for instances of <see cref="Manufacturer"/>.
    /// </summary>
    public interface IManufacturerUoWRepository : IRepository<Manufacturer>
    {
    }
}
