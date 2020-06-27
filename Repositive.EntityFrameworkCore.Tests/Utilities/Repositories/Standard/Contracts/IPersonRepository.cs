namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Standard
{
    using Repositive.Abstractions;

    /// <summary>
    ///     Provides repository methods for instances of <see cref="Person"/>.
    /// </summary>
    public interface IPersonRepository : IRepository<Person>, IRelatedLoadableRepository<Person>, ISaveableRepository
    {
    }
}