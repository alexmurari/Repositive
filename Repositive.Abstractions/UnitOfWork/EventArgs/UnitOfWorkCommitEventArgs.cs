namespace Repositive.Abstractions
{
    using System;
    using System.Collections.Generic;

    // ReSharper disable StyleCop.SA1126

    /// <summary>
    ///     Base class for arguments of unit of work commit events.
    /// </summary>
    public abstract class UnitOfWorkCommitEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitOfWorkCommitEventArgs"/> class.
        /// </summary>
        /// <param name="registeredRepositories">
        ///     The collection containing the names of the repositories registered in the unit of work.
        /// </param>
        protected UnitOfWorkCommitEventArgs(IEnumerable<string> registeredRepositories)
        {
            RegisteredRepositories = new List<string>(registeredRepositories);
        }

        /// <summary>
        ///     Gets the names of the repositories registered in the unit of work.
        /// </summary>
        public IReadOnlyCollection<string> RegisteredRepositories { get; }
    }
}