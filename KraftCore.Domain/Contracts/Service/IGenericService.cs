namespace KraftCore.Domain.Contracts.Service
{
    using KraftCore.Domain.Contracts.Repository;

    /// <summary>
    /// Provides methods for service operations on instances of <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity for which the service will operate.</typeparam>
    public interface IGenericService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets the repository.
        /// </summary>
        IGenericRepository<TEntity> Repository { get; }
    }
}