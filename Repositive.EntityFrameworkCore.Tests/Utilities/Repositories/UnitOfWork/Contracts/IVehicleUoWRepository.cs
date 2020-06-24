namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.UnitOfWork
{
    using Repositive.Abstractions;

    /// <summary>
    ///     Provides repository methods for instances of <see cref="Vehicle"/>.
    /// </summary>
    public interface IVehicleUoWRepository : IRepository<Vehicle>
    {
    }
}