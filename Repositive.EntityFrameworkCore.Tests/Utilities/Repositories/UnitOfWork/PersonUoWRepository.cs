namespace Repositive.EntityFrameworkCore.Tests.Utilities.Repositories
{
    using Repositive.Abstractions;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Context;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Repositories.Contracts;

    /// <summary>
    ///     Provides a repository pattern implementation for querying and saving <see cref="Person"/> instances.
    /// </summary>
    internal class PersonUoWRepository : Repository<Person, RepositiveUoWContext>, IPersonUoWRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PersonUoWRepository"/> class.
        /// </summary>
        /// <param name="context">
        ///     The database context.
        /// </param>
        public PersonUoWRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
