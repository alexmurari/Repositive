namespace Repositive.Abstractions
{
    using System.Collections.Generic;

    /// <summary>
    ///     Arguments for the <see cref="IUnitOfWork.Committing"/> event.
    /// </summary>
    public class UnitOfWorkCommittingEventArgs : UnitOfWorkCommitEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitOfWorkCommittingEventArgs"/> class.
        /// </summary>
        /// <param name="registeredRepositories">
        ///     The collection containing the names of the repositories registered in the unit of work.
        /// </param>
        public UnitOfWorkCommittingEventArgs(IEnumerable<string> registeredRepositories) : base(registeredRepositories)
        {
        }
    }
}