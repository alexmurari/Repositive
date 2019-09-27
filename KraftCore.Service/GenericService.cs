namespace KraftCore.Service
{
    using KraftCore.Domain.Contracts.Repository;
    using KraftCore.Domain.Contracts.Service;

    /// <summary>
    /// Base class for generic service implementations that operates on values of type <typeparamref name="TEntity"/>.
    /// Querying and saving is done through <see cref="IGenericRepository{TEntity}"/> implementations.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The underlying type that this service operates on.
    /// </typeparam>
    public abstract class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericService{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository for this service.
        /// </param>
        protected GenericService(IGenericRepository<TEntity> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        protected IGenericRepository<TEntity> Repository { get; }
    }
}