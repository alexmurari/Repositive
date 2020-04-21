namespace Repositive.Tests.Utilities.Context.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Repositive.Tests.Utilities.Entities;

    /// <summary>
    ///     Provides database configuration for the <see cref="Vehicle"/> entity.
    /// </summary>
    internal class VehicleMap : IEntityTypeConfiguration<Vehicle>
    {
        /// <summary>
        ///     Configures the entity of type <see cref="Vehicle"/>.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(t => t.Type).IsRequired();
            builder.Property(t => t.OwnerId).IsRequired(false);
            builder.Property(t => t.ManufacturerId).IsRequired();

            // Relationships
            builder.HasOne(t => t.Manufacturer).WithMany(t => t.Vehicles).HasForeignKey(t => t.ManufacturerId);
            builder.HasOne(t => t.Owner).WithMany(t => t.Vehicles).HasForeignKey(t => t.OwnerId);
        }
    }
}
