namespace Repositive.EntityFrameworkCore.Tests.Utilities
{
    /// <summary>
    ///     Provides extension methods to the <see cref="Vehicle"/> class.
    /// </summary>
    public static class VehicleExtensions
    {
        /// <summary>
        ///     Gets the number of related entities reachable from the provided entity instance, including sub-entities.
        /// </summary>
        /// <param name="vehicle">The vehicle instance.</param>
        /// <returns>The number of related entities.</returns>
        public static int CountRelatedEntities(this Vehicle vehicle)
        {
            return (vehicle?.Manufacturer == null ? default : 1) + (vehicle?.Manufacturer?.Subsidiaries?.Count).GetValueOrDefault();
        }
    }
}
