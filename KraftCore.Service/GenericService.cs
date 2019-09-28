namespace KraftCore.Service
{
    using KraftCore.Domain.Contracts.Repository;
    using KraftCore.Domain.Contracts.Service;

    /// <summary>
    ///     Base class for generic services that operates on instances of <typeparamref name="TEntity" />.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that this service perform operations.
    /// </typeparam>
    public abstract class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericService{TEntity}" /> class.
        /// </summary>
        /// <param name="repository">
        ///     The repository for this service.
        /// </param>
        protected GenericService(IGenericRepository<TEntity> repository)
        {
            Repository = repository;
        }

        /// <summary>
        ///     Gets the repository.
        /// </summary>
        protected IGenericRepository<TEntity> Repository { get; }
    }
}