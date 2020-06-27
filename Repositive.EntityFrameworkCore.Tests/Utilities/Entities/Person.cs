namespace Repositive.EntityFrameworkCore.Tests.Utilities
{
    using System.Collections.Generic;

    /// <summary>
    ///     Represents a person.
    /// </summary>
    public class Person
    {
        /// <summary>
        ///     Gets or sets the id.
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
    }
}
