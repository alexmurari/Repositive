namespace KraftCore.Tests.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;

    /// <summary>
    /// Utilities for unit testing.
    /// </summary>
    internal static class TestUtilities
    {
        /// <summary>
        /// The faker used to create dummy objects with fake data.
        /// </summary>
        private static readonly Faker Faker;

        /// <summary>
        /// The person faker used to generate fake <see cref="Person"/> objects.
        /// </summary>
        private static readonly Faker<Person> PersonFaker;

        /// <summary>
        /// The list of colors.
        /// </summary>
        private static readonly string[] Colors = 
        {
            "Blue", "Red", "Yellow", "Purple", "Cyan", "Black", "White", "Green", "Brown"
        };

        /// <summary>
        /// The list of fruits.
        /// </summary>
        private static readonly string[] Fruits =
        {
            "apple", "banana", "orange", "strawberry", "kiwi", "passion-fruit", "blueberry"
        };

        /// <summary>
        /// Initializes static members of the <see cref="TestUtilities"/> class.
        /// </summary>
        static TestUtilities()
        {
            Faker = new Faker();

            PersonFaker = new Faker<Person>()
                .StrictMode(true)
                .RuleFor(t => t.FirstName, f => f.Name.FirstName())
                .RuleFor(t => t.LastName, f => f.Name.LastName())
                .RuleFor(t => t.FullName, (f, p) => string.Concat(p.FirstName, " ", p.LastName))
                .RuleFor(t => t.DateOfBirth, f => f.Date.Past(100, DateTime.Now.AddYears(-25)))
                .RuleFor(t => t.Age, (f, p) => DateTime.Now.Year - p.DateOfBirth.Year)
                .RuleFor(t => t.FavoriteNumbers, f => f.Random.ListItems(Enumerable.Range(1, 5000).ToList(), 5))
                .RuleFor(t => t.FavoriteWords, f => f.Random.WordsArray(10))
                .RuleFor(t => t.FavoriteColors, f => f.Random.ArrayElements(Colors, 3))
                .RuleFor(t => t.FavoriteFruits, f => new ArrayList(f.Random.ArrayElements(Fruits, 2)));

            PersonFaker.AssertConfigurationIsValid();
        }

        /// <summary>
        /// Gets an collection of <see cref="Person"/> objects filled with fake data.
        /// </summary>
        /// <param name="count">
        /// The number of entries to be generated.
        /// </param>
        /// <returns>
        /// The collection <see cref="Person"/> objects.
        /// </returns>
        internal static List<Person> GetFakePersonCollection(int count = 50) => PersonFaker.Generate(count);

        /// <summary>
        /// Gets an random item from the provided collection of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="collection">
        /// The collection of <typeparamref name="T"/>.
        /// </param>
        /// <typeparam name="T">
        /// The type of the generic collection.
        /// </typeparam>
        /// <returns>
        /// The random <typeparamref name="T"/> object.
        /// </returns>
        internal static T GetRandomItem<T>(IEnumerable<T> collection) => Faker.Random.CollectionItem(collection.ToList());

        /// <summary>
        /// Gets an random collection of items from the provided collection of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="collection">
        /// The collection of <typeparamref name="T"/>.
        /// </param>
        /// <param name="count">
        /// The number of elements to be returned.
        /// </param>
        /// <typeparam name="T">
        /// The type of the generic collection.
        /// </typeparam>
        /// <returns>
        /// The random collection of <typeparamref name="T"/>.
        /// </returns>
        internal static T[] GetRandomItems<T>(IEnumerable<T> collection, int count = 5) => Faker.Random.ArrayElements(collection.ToArray(), count);
    }
}
