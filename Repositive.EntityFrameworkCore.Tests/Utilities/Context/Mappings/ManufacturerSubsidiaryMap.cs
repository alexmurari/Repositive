namespace Repositive.EntityFrameworkCore.Tests.Utilities.Context.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    ///     Provides database configuration for the <see cref="ManufacturerSubsidiary"/> entity.
    /// </summary>
    internal class ManufacturerSubsidiaryMap : IEntityTypeConfiguration<ManufacturerSubsidiary>
    {
        /// <summary>
        ///     Configures the entity of type <see cref="ManufacturerSubsidiary"/>.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<ManufacturerSubsidiary> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(t => t.City).IsRequired();
        }
    }
}
