namespace Repositive.EntityFrameworkCore.Tests.Utilities.Context.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities;

    /// <summary>
    ///     Provides database configuration for the <see cref="Manufacturer"/> entity.
    /// </summary>
    internal class ManufacturerMap : IEntityTypeConfiguration<Manufacturer>
    {
        /// <summary>
        ///     Configures the entity of type <see cref="Manufacturer"/>.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(t => t.Name).IsRequired();

            // Relationships
            builder.HasMany(t => t.Vehicles).WithOne(t => t.Manufacturer).HasForeignKey(t => t.ManufacturerId);
            builder.HasMany(t => t.Subsidiaries).WithOne().HasForeignKey(t => t.ManufacturerId);
        }
    }
}
