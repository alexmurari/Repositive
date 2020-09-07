namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Standard
{
    using Repositive.Abstractions;

    /// <summary>
    ///     Provides repository methods for instances of <see cref="Vehicle"/>.
    /// </summary>
    public interface IVehicleRepository : IRepository<Vehicle>, IRelatedLoadableRepository<Vehicle>, IQueryableRepository<Vehicle>
    {
    }
}