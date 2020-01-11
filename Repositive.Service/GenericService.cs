namespace Repositive.Service
{
    using Repositive.Domain.Contracts.Repository;
    using Repositive.Domain.Contracts.Service;
    using Repositive.Shared.Extensions;

    /// <summary>
    ///     Base class for generic services that operates on instances of <typeparamref name="TEntity" />.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that this service perform operations.
    /// </typeparam>
    /// <typeparam name="TEntityRepository">
    ///     The repository type that queries and saves instances of <typeparamref name="TEntity"/>.
    /// </typeparam>
    public abstract class GenericService<TEntity, TEntityRepository> : IGenericService<TEntity, TEntityRepository> where TEntity : class where TEntityRepository : class, IGenericRepository<TEntity>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericService{TEntity,TEntityRepository}"/> class. 
        /// </summary>
        /// <param name="repository">
        ///     The repository for this service to query and save instances of <typeparamref name="TEntity"/>.
        /// </param>
        protected GenericService(TEntityRepository repository)
        {
            Repository = repository.ThrowIfNull(nameof(repository));
        }

        /// <summary>
        ///     Gets the repository that queries and saves instances of <typeparamref name="TEntity" />.
        /// </summary>
        protected TEntityRepository Repository { get; }
    }
}