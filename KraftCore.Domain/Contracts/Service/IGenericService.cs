namespace KraftCore.Domain.Contracts.Service
{
    // ReSharper disable UnusedTypeParameter

    /// <summary>
    /// Provides methods for service operations on instances of <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity for which the service will operate.</typeparam>
    public interface IGenericService<TEntity> where TEntity : class
    {
    }
}