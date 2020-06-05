namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts
{
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;

    /// <summary>
    ///     Provides repository methods for instances of <see cref="Person"/>.
    /// </summary>
    public interface IPersonRepository : IRepository<Person>, IRelatedLoadableRepository<Person>, ISaveableRepository
    {
    }
}