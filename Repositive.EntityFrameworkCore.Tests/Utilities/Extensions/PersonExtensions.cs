namespace Repositive.EntityFrameworkCore.Tests.Utilities
{
    using System.Linq;

    /// <summary>
    ///     Provides extension methods to the <see cref="Person"/> class.
    /// </summary>
    public static class PersonExtensions
    {
        /// <summary>
        ///     Gets the number of related entities reachable from the provided entity instance, including sub-entities.
        /// </summary>
        /// <param name="person">The person instance.</param>
        /// <returns>The number of related entities.</returns>
        public static int CountRelatedEntities(this Person person)
        {
            return (person.Vehicles?.Count + person.Vehicles?.Sum(t => t.CountRelatedEntities())).GetValueOrDefault();
        }
    }
}
