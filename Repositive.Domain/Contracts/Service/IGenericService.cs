namespace Repositive.Domain.Contracts.Service
{
    using Repositive.Domain.Contracts.Repository;

    // ReSharper disable once UnusedTypeParameter

    /// <summary>
    ///     Provides methods for service operations on instances of <typeparamref name="TEntity" />.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that this service perform operations.
    /// </typeparam>
    /// <typeparam name="TEntityRepository">
    ///     The repository type that queries and saves instances of <typeparamref name="TEntity"/>.
    /// </typeparam>
    public interface IGenericService<TEntity, TEntityRepository> where TEntity : class where TEntityRepository : class, IGenericRepository<TEntity>
    {
    }
}