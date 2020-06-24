namespace Repositive.EntityFrameworkCore.Tests.Utilities
{
    using System.Collections.Generic;
    using Bogus;
    using Person = Repositive.EntityFrameworkCore.Tests.Utilities.Person;

    /// <summary>
    ///     Provides static methods for generating fake data for testing purposes.
    /// </summary>
    internal static class DataGenerator
    {
        /// <summary>
        ///     The person faker. Generates fake <see cref="Utilities.Person"/> objects.
        /// </summary>
        private static readonly Faker<Utilities.Person> PersonFaker = new Faker<Utilities.Person>()
            .RuleFor(t => t.Name, t => t.Name.FirstName())
            .RuleFor(t => t.Vehicles, t => VehicleFaker.Generate(t.Random.Int(2, 5)));

        /// <summary>
        ///     The vehicle faker. Generates fake <see cref="Vehicle"/> objects.
        /// </summary>
        private static readonly Faker<Vehicle> VehicleFaker = new Faker<Vehicle>()
            .RuleFor(t => t.Manufacturer, t => VehicleManufacturerFaker.Generate())
            .RuleFor(t => t.Type, t => t.PickRandom<VehicleType>());

        /// <summary>
        ///     The vehicle manufacturer faker. Generates fake <see cref="Manufacturer"/> objects.
        /// </summary>
        private static readonly Faker<Manufacturer> VehicleManufacturerFaker = new Faker<Manufacturer>()
            .RuleFor(t => t.Name, t => t.Company.CompanyName())
            .RuleFor(t => t.Subsidiaries, t => ManufacturerSubsidiaryFaker.Generate(t.Random.Int(1, 3)));

        /// <summary>
        ///     The manufacturer subsidiary faker. Generates fake <see cref="ManufacturerSubsidiary"/> objects.
        /// </summary>
        private static readonly Faker<ManufacturerSubsidiary> ManufacturerSubsidiaryFaker = new Faker<ManufacturerSubsidiary>()
            .RuleFor(t => t.City, t => t.Address.City());

        /// <summary>
        ///     The random generator.
        /// </summary>
        private static readonly Randomizer Random = new Randomizer();

        /// <summary>
        ///     Generates and returns a collection of <see cref="Utilities.Person"/> objects filled with data.
        /// </summary>
        /// <param name="count">
        ///     The number of objects to be generated.
        /// </param>
        /// <returns>
        ///     The collection of objects.
        /// </returns>
        internal static IList<Utilities.Person> GeneratePersons(int count = 500)
        {
            return PersonFaker.Generate(count);
        }

        /// <summary>
        ///     Picks a random item from the provided collection.
        /// </summary>
        /// <param name="collection">
        ///     The collection to select the item from.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the item.
        /// </typeparam>
        /// <returns>
        ///     The random item.
        /// </returns>
        internal static T PickRandomItem<T>(IList<T> collection)
        {
            return Random.CollectionItem(collection);
        }

        /// <summary>
        ///     Picks a random subset of items from the provided collection.
        /// </summary>
        /// <param name="collection">
        ///     The collection to select the items from.
        /// </param>
        /// <param name="count">
        ///     The number of items to be retrieved.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the item.
        /// </typeparam>
        /// <returns>
        ///     The random collection of items.
        /// </returns>
        internal static IList<T> PickRandomItemRange<T>(IList<T> collection, int? count = null)
        {
            return Random.ListItems(collection, count);
        }
    }
}