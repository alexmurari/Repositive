namespace Repositive.EntityFrameworkCore.Tests.Utilities.Context.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    ///     Provides database configuration for the <see cref="Person"/> entity.
    /// </summary>
    internal class PersonMap : IEntityTypeConfiguration<Person>
    {
        /// <summary>
        ///     Configures the entity of type <see cref="Person"/>.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(t => t.Name).IsRequired();

            // Relationships
            builder.HasMany(t => t.Vehicles).WithOne(t => t.Owner).HasForeignKey(t => t.OwnerId);
        }
    }
}
