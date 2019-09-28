namespace KraftCore.Domain.Contracts.Service
{
    // ReSharper disable once UnusedTypeParameter

    /// <summary>
    ///     Provides methods for service operations on instances of <typeparamref name="TEntity" />.
    /// </summary>
    /// <typeparam name="TEntity">The entity type that this service perform operations.</typeparam>
    public interface IGenericService<TEntity> where TEntity : class
    {
    }
}