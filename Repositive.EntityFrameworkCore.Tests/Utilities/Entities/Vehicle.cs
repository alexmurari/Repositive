namespace Repositive.EntityFrameworkCore.Tests.Utilities.Entities
{
    using Repositive.EntityFrameworkCore.Tests.Utilities.Entities.Enums;

    /// <summary>
    ///     Represents an vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        public VehicleType Type { get; set; }

        /// <summary>
        ///     Gets or sets the manufacturer id.
        /// </summary>
        public int ManufacturerId { get; set; }

        /// <summary>
        ///     Gets or sets the owner id.
        /// </summary>
        public int? OwnerId { get; set; }

        /// <summary>
        ///     Gets or sets the manufacturer.
        /// </summary>
        public Manufacturer Manufacturer { get; set; }

        /// <summary>
        ///     Gets or sets the owner.
        /// </summary>
        public Person Owner { get; set; }
    }
}
