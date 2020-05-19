namespace Repositive.EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Exprelsior.Shared.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Repositive.Contracts;

    /// <summary>
    ///     Implements the unit of work pattern and provides commit coordination between repositories.
    /// </summary>
    /// <typeparam name="TContext">
    ///     The type used as the database context.
    ///     Must be of <see cref="DbContext"/> type or derive from it.
    /// </typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        /// <summary>
        ///     The names of the repositories registered in this unit of work instance.
        /// </summary>
        private readonly HashSet<string> _registeredRepositories;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
        /// </summary>
        /// <param name="context">
        ///     The database context.
        /// </param>
        public UnitOfWork(TContext context)
        {
            Context = context.ThrowIfNull(nameof(context));
            _registeredRepositories = new HashSet<string>();
        }

        /// <summary>
        ///     Occurs when the commit operation is started.
        /// </summary>
        public event EventHandler<UnitOfWorkCommittingEventArgs> Committing;

        /// <summary>
        ///     Occurs when the commit operation is finished.
        /// </summary>
        public event EventHandler<UnitOfWorkCommittedEventArgs> Committed;

        /// <summary>
        ///     Gets the database context.
        /// </summary>
        protected TContext Context { get; }

        /// <summary>
        ///     Commits all changes made in this unit of work context to the database.
        /// </summary>
        /// <returns>The number of affected rows in the database.</returns>
        public virtual int Commit()
        {
            Committing?.Invoke(this, new UnitOfWorkCommittingEventArgs(_registeredRepositories));

            var affectedRows = Context.SaveChanges();

            Committed?.Invoke(this, new UnitOfWorkCommittedEventArgs(affectedRows, _registeredRepositories));

            return affectedRows;
        }

        /// <summary>
        ///     Asynchronously commits all changes made in this unit of work context to the database.
        /// </summary>
        /// <param name="cancellationToken">
        ///     The token that propagates a cancellation request to interrupt the operation.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous commit operation.
        ///     The task result contains the number of affected rows in the database.
        /// </returns>
        public virtual async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            Committing?.Invoke(this, new UnitOfWorkCommittingEventArgs(_registeredRepositories));

            var affectedRows = await Context.SaveChangesAsync(cancellationToken);

            Committed?.Invoke(this, new UnitOfWorkCommittedEventArgs(affectedRows, _registeredRepositories));

            return affectedRows;
        }

        /// <summary>
        ///     Gets the database context associated to this unit of work.
        /// </summary>
        /// <returns>
        ///     The database context.
        /// </returns>
        internal TContext GetDbContext()
        {
            return Context;
        }

        /// <summary>
        ///     Registers a repository in this unit of work instance, if not already registered.
        ///     The registration is only used for informational purposes and does not affect the class operation.
        /// </summary>
        /// <param name="repositoryName">
        ///     The name of the repository.
        /// </param>
        internal void AddRepository(string repositoryName)
        {
            _registeredRepositories.Add(repositoryName.ThrowIfNullOrWhitespace(nameof(repositoryName)));
        }

        /// <summary>
        ///     Removes a repository register from this unit of work instance.
        ///     The registration is only used for informational purposes and does not affect the class operation.
        /// </summary>
        /// <param name="repositoryName">
        ///     The name of the repository.
        /// </param>
        internal void RemoveRepository(string repositoryName)
        {
            _registeredRepositories.Remove(repositoryName.ThrowIfNullOrWhitespace(nameof(repositoryName)));
        }
    }
}