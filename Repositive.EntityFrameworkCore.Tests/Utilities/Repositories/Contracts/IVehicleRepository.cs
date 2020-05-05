namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts
{
    using Repositive.Contracts;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;

    /// <summary>
    ///     Provides methods for querying and saving instances of <see cref="Vehicle"/>.
    /// </summary>
    public interface IVehicleRepository : IRepository<Vehicle>, IRelatedLoadableRepository<Vehicle>
    {
    }
}