namespace Repositive.Contracts
{
    using System.Collections.Generic;

    // ReSharper disable StyleCop.SA1126

    /// <summary>
    ///     Arguments for the <see cref="IUnitOfWork.Committed"/> event.
    /// </summary>
    public class UnitOfWorkCommittedEventArgs : UnitOfWorkCommitEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitOfWorkCommittedEventArgs"/> class.
        /// </summary>
        /// <param name="affectedEntries">
        ///     The number of entries affected by the commit operation.
        /// </param>
        /// <param name="registeredRepositories">
        ///     The collection containing the names of the repositories registered in the unit of work.
        /// </param>
        public UnitOfWorkCommittedEventArgs(int affectedEntries, IEnumerable<string> registeredRepositories) : base(registeredRepositories)
        {
            AffectedEntries = affectedEntries;
        }

        /// <summary>
        ///     Gets the number of entries affected by the commit operation.
        /// </summary>
        public int AffectedEntries { get; }
    }
}