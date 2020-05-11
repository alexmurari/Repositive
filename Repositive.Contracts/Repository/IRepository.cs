namespace Repositive.Contracts
{
    /// <summary>
    ///     Provides repository methods for creating, reading, updating and deleting instances of <typeparamref name="TEntity" />.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that the repository creates, reads, updates and deletes.
    /// </typeparam>
    /// <remarks>
    ///     This interface is a unification of the <see cref="ICreatableRepository{TEntity}"/>, <see cref="IReadableRepository{TEntity}"/>,
    ///     <see cref="IUpdateableRepository{TEntity}"/> and <see cref="IDeletableRepository{TEntity}"/> interfaces.
    /// </remarks>
    public interface IRepository<TEntity> : ICreatableRepository<TEntity>, IReadableRepository<TEntity>, IUpdateableRepository<TEntity>, IDeletableRepository<TEntity> where TEntity : class
    {
    }
}