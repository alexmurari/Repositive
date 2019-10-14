namespace KraftCore.Tests.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;

    /// <summary>
    ///     Utilities for unit testing.
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        ///     The list of colors.
        /// </summary>
        private static readonly string[] Colors =
        {
            "Blue", "Red", "Yellow", "Purple", "Cyan", "Black", "White", "Green", "Brown"
        };

        /// <summary>
        ///     The faker used to create dummy objects with fake data.
        /// </summary>
        private static readonly Faker Faker;

        /// <summary>
        ///     The list of fruits.
        /// </summary>
        private static readonly string[] Fruits =
        {
            "apple", "banana", "orange", "strawberry", "kiwi", "passion-fruit", "blueberry"
        };

        /// <summary>
        /// The person list.
        /// </summary>
        private static readonly List<Person> PersonList = new List<Person>();

        /// <summary>
        ///     Initializes static members of the <see cref="Utilities" /> class.
        /// </summary>
        static Utilities()
        {
            Faker = new Faker();

            var personFaker = new Faker<Person>()
                .StrictMode(true)
                .RuleFor(t => t.FirstName, f => f.Name.FirstName())
                .RuleFor(t => t.LastName, f => f.Name.LastName())
                .RuleFor(t => t.FullName, (f, p) => string.Concat(p.FirstName, " ", p.LastName))
                .RuleFor(t => t.DateOfBirth, f => f.Date.Past(100, DateTime.Now.AddYears(-25)))
                .RuleFor(t => t.Age, (f, p) => DateTime.Now.Year - p.DateOfBirth.Year)
                .RuleFor(t => t.BestFriend,
                         (f, p) => new Person
                         {
                             FirstName = f.Name.FirstName(),
                             LastName = f.Name.LastName(),
                             DateOfBirth = f.Date.Past(100, DateTime.Now.AddYears(-25)),
                             BestFriend = p
                         })
                .RuleFor(t => t.FavoriteNumbers, f => f.Random.ListItems(Enumerable.Range(1, 5000).ToList(), 5))
                .RuleFor(t => t.FavoriteWords, f => f.Random.WordsArray(10))
                .RuleFor(t => t.FavoriteColors, f => f.Random.ArrayElements(Colors, 3))
                .RuleFor(t => t.FavoriteFruits, f => new ArrayList(f.Random.ArrayElements(Fruits, 2)))
                .RuleFor(t => t.DateOfDriversLicense, f => f.Date.Past(100, DateTime.Now.AddYears(-25)))
                .RuleFor(t => t.AccountBalance, f => f.Finance.Amount(0, 1000000, 3))
                .RuleFor(t => t.LeastFavoriteNumbers, f => f.Random.ListItems(Enumerable.Range(5001, 10000).Select(t => (int?)t).ToList(), 5))
                .RuleFor(t => t.HasPet, f => f.Random.Bool())
                .RuleFor(t => t.PersonGuid, f => f.Random.Guid())
                .RuleFor(t => t.OptionalPersonGuid, f => f.Random.Guid())
                .RuleFor(t => t.PersonChar, f => f.Random.Char())
                .RuleFor(t => t.OptionalPersonChar, f => f.Random.Char());

            PersonList.AddRange(personFaker.Generate(1000));
        }

        /// <summary>
        ///     Gets an collection of <see cref="Person" /> objects filled with fake data.
        /// </summary>
        /// <returns>
        ///     The collection <see cref="Person" /> objects.
        /// </returns>
        internal static List<Person> GetFakePersonCollection()
        {
            return PersonList;
        }

        /// <summary>
        ///     Gets an random item from the provided collection of type <typeparamref name="T" />.
        /// </summary>
        /// <param name="collection">
        ///     The collection of <typeparamref name="T" />.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the generic collection.
        /// </typeparam>
        /// <returns>
        ///     The random <typeparamref name="T" /> object.
        /// </returns>
        internal static T GetRandomItem<T>(IEnumerable<T> collection)
        {
            return Faker.Random.CollectionItem(collection.ToList());
        }

        /// <summary>
        ///     Gets an random collection of items from the provided collection of type <typeparamref name="T" />.
        /// </summary>
        /// <param name="collection">
        ///     The collection of <typeparamref name="T" />.
        /// </param>
        /// <param name="count">
        ///     The number of elements to be returned.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the generic collection.
        /// </typeparam>
        /// <returns>
        ///     The random collection of <typeparamref name="T" />.
        /// </returns>
        internal static T[] GetRandomItems<T>(IEnumerable<T> collection, int count = 5)
        {
            return Faker.Random.ArrayElements(collection.ToArray(), count);
        }
    }
}