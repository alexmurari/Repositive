namespace Repositive.EntityFrameworkCore.Tests.Utilities
{
    /// <summary>
    ///     Represents an subsidiary of a vehicle manufacturer.
    /// </summary>
    public class ManufacturerSubsidiary
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the manufacturer id.
        /// </summary>
        public int ManufacturerId { get; set; }

        /// <summary>
        ///     Gets or sets the city.
        /// </summary>
        public string City { get; set; }
    }
}
