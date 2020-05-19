namespace Repositive.Contracts
{
    /// <summary>
    ///     Defines a repository contract for creating, reading, updating and deleting instances of <typeparamref name="TEntity" />.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type that the repository creates, reads, updates and deletes.
    /// </typeparam>
    /// <remarks>
    ///     <para>
    ///         This interface provides CRUD (create, read, update, delete) methods only.
    ///         It's a combination of the <see cref="ICreatableRepository{TEntity}"/>, <see cref="IReadableRepository{TEntity}"/>,
    ///         <see cref="IUpdateableRepository{TEntity}"/> and <see cref="IDeletableRepository{TEntity}"/> interfaces.
    ///     </para>
    ///     <para>
    ///         This interface does not provide methods for committing changes.
    ///         For committing changes, use the <see cref="ISaveableRepository"/> interface or use the unit of work
    ///         pattern (<see cref="IUnitOfWork"/>) for synchronizing commit operations between repositories.
    ///     </para>
    /// </remarks>
    public interface IRepository<TEntity> : ICreatableRepository<TEntity>, IReadableRepository<TEntity>, IUpdateableRepository<TEntity>, IDeletableRepository<TEntity> where TEntity : class
    {
    }
}