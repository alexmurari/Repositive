namespace Repositive.EntityFrameworkCore.Tests.Utilities.Context
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Represents a database context.
    /// </summary>
    public class RepositiveUoWContext : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RepositiveUoWContext"/> class.
        /// </summary>
        /// <param name="options">
        ///     The options to configure the database context instance.
        /// </param>
        public RepositiveUoWContext(DbContextOptions<RepositiveUoWContext> options) : base(options)
        {
        }

        /// <summary>
        ///     Configures the creation of the database model.
        /// </summary>
        /// <param name="modelBuilder">The model builder instance for configuring the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}