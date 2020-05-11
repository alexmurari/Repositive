namespace Repositive.Contracts
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Provides methods for saving changes made in a repository to the underlying database.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Only use this interface when not using unit of work pattern (<see cref="IUnitOfWork"/> interface) as it synchronizes
    ///         the commit operation between different repositories and, therefore, no commit should be made directly from a repository.
    ///     </para>
    ///     <para>
    ///         Caution is advised when using this interface, as the database context may be shared between different repositories, changes
    ///         from other repositories may be committed when saving changes from a specific repository, leading to unexpected and/or unintended behavior.
    ///         For these scenarios, the unit of work pattern (<see cref="IUnitOfWork"/> interface) is the recommended approach.
    ///     </para>
    /// </remarks>
    public interface ISaveableRepository
    {
        /// <summary>
        ///     Saves all changes made in this repository to the database.
        /// </summary>
        /// <returns>The number of affected rows in the database.</returns>
        int SaveChanges();

        /// <summary>
        ///     Asynchronously saves all changes made in this repository to the database.
        /// </summary>
        /// <param name="cancellationToken">
        ///     The token that propagates a cancellation request to interrupt the operation.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous save operation.
        ///     The task result contains the number of affected rows in the database.
        /// </returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}