namespace Repositive.Tests.Utilities.Context.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Repositive.Tests.Utilities.Entities;

    /// <summary>
    ///     Provides database configuration for the <see cref="VehicleManufacturer"/> entity.
    /// </summary>
    internal class VehicleManufacturerMap : IEntityTypeConfiguration<VehicleManufacturer>
    {
        /// <summary>
        ///     Configures the entity of type <see cref="VehicleManufacturer"/>.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<VehicleManufacturer> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(t => t.Name).IsRequired();

            // Relationships
            builder.HasMany(t => t.Vehicles).WithOne(t => t.Manufacturer).HasForeignKey(t => t.ManufacturerId);
        }
    }
}
