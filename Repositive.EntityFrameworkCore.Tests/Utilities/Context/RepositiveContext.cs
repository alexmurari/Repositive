namespace Repositive.EntityFrameworkCore.Tests.Utilities.Context
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Represents a database context.
    /// </summary>
    public class RepositiveContext : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RepositiveContext"/> class.
        /// </summary>
        /// <param name="options">
        ///     The options to configure the database context instance.
        /// </param>
        public RepositiveContext(DbContextOptions options) : base(options)
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
