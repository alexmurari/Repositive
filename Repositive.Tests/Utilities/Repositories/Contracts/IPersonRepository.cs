namespace Repositive.Tests.Utilities.Repositories.Contracts
{
    using Repositive.Domain.Contracts.Repository;
    using Repositive.Tests.Utilities.Entities;

    /// <summary>
    ///     Provides methods for querying and saving instances of <see cref="Person"/>.
    /// </summary>
    public interface IPersonRepository : IGenericRepository<Person>
    {
    }
}