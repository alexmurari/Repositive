namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.UnitOfWork
{
    using Repositive.Abstractions;

    /// <summary>
    ///     Provides repository methods for instances of <see cref="Person"/>.
    /// </summary>
    public interface IPersonUoWRepository : IRepository<Person>, ISaveableRepository
    {
    }
}