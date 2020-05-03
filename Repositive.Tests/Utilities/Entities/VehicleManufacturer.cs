namespace Repositive.Tests.Utilities.Entities
{
    using System.Collections.Generic;

    /// <summary>
    ///     Represents an vehicle manufacturer.
    /// </summary>
    public class VehicleManufacturer
    {
        /// <summary>
        ///     Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the vehicles.
        /// </summary>
        public ICollection<Vehicle> Vehicles { get; set; }

        /// <summary>
        ///     Gets or sets the subsidiaries.
        /// </summary>
        public ICollection<ManufacturerSubsidiary> Subsidiaries { get; set; }
    }
}
