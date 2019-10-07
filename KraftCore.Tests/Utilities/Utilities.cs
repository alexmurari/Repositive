namespace KraftCore.Tests.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using Bogus.Extensions;

    /// <summary>
    ///     Utilities for unit testing.
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        ///     The faker used to create dummy objects with fake data.
        /// </summary>
        private static readonly Faker Faker;

        /// <summary>
        ///     The faker used to generate fake <see cref="Hydra" /> objects.
        /// </summary>
        private static readonly Faker<Hydra> HydraFaker;

        /// <summary>
        ///     Initializes static members of the <see cref="Utilities" /> class.
        /// </summary>
        static Utilities()
        {
            Faker = new Faker();

            HydraFaker = new Faker<Hydra>()
                    .StrictMode(true)
                    //// Strings
                    .RuleFor(t => t.FirstName, f => f.Name.FirstName())
                    .RuleFor(t => t.LastName, f => f.Name.LastName())
                    .RuleFor(t => t.FullName, f => f.Name.FullName())
                    .RuleFor(t => t.StringArray, f => f.Random.WordsArray(10))
                    .RuleFor(t => t.StringCollection, f => f.Random.WordsArray(10))
                    //// Integers
                    .RuleFor(t => t.Integer, f => f.Random.Int())
                    .RuleFor(t => t.NullableInteger, f => f.Random.Int().OrNull(f))
                    .RuleFor(t => t.IntegerArray, f => GetRandomItems(Enumerable.Range(1, 5000)))
                    .RuleFor(t => t.NullableIntegerArray, f => GetRandomItems(Enumerable.Range(5001, 10000)).Select(t => t.OrNull(f)))
                    .RuleFor(t => t.IntegerCollection, f => GetRandomItems(Enumerable.Range(10001, 15000)))
                    .RuleFor(t => t.NullableIntegerCollection, f => GetRandomItems(Enumerable.Range(15001, 20000)).Select(t => t.OrNull(f)))
                    //// Doubles
                    .RuleFor(t => t.Double, f => (double)f.Random.Int() / 3)
                    .RuleFor(t => t.NullableDouble, f => ((double)f.Random.Int() / 3).OrNull(f))
                    .RuleFor(t => t.DoubleArray, f => GetRandomItems(Enumerable.Range(1, 5000)).Select(t => t / 3d))
                    .RuleFor(t => t.NullableDoubleArray, f => GetRandomItems(Enumerable.Range(5001, 10000)).Select(t => (t / 3d).OrNull(f)))
                    .RuleFor(t => t.DoubleCollection, f => GetRandomItems(Enumerable.Range(10001, 15000)).Select(t => t / 3d))
                    .RuleFor(t => t.NullableDoubleCollection, f => GetRandomItems(Enumerable.Range(15001, 20000)).Select(t => (t / 3d).OrNull(f)))
                    //// Decimals
                    .RuleFor(t => t.Decimal, f => (decimal)f.Random.Int() / 3)
                    .RuleFor(t => t.NullableDecimal, f => ((decimal)f.Random.Int() / 3).OrNull(f))
                    .RuleFor(t => t.DecimalArray, f => GetRandomItems(Enumerable.Range(1, 5000)).Select(t => t / 3m))
                    .RuleFor(t => t.NullableDecimalArray, f => GetRandomItems(Enumerable.Range(5001, 10000)).Select(t => (t / 3m).OrNull(f)))
                    .RuleFor(t => t.DecimalCollection, f => GetRandomItems(Enumerable.Range(10001, 15000)).Select(t => t / 3m))
                    .RuleFor(t => t.NullableDecimalCollection, f => GetRandomItems(Enumerable.Range(15001, 20000)).Select(t => (t / 3m).OrNull(f)))
                    //// DateTimes
                    .RuleFor(t => t.DateTime, f => f.Date.Past())
                    .RuleFor(t => t.NullableDateTime, f => f.Date.Future())
                    .RuleFor(t => t.DateTimeArray, f => GetRandomItems(f.Date.Soon().Range(f.Date.Future())))
                    .RuleFor(t => t.NullableDateTimeArray, f => GetRandomItems(f.Date.Soon().Range(f.Date.Future())).Select(t => t.OrNull(f)))
                    .RuleFor(t => t.DateTimeCollection, f => GetRandomItems(f.Date.Soon().Range(f.Date.Future())))
                    .RuleFor(t => t.NullableDateTimeCollection, f => GetRandomItems(f.Date.Soon().Range(f.Date.Future())).Select(t => t.OrNull(f)))
                    //// GUIDs
                    .RuleFor(t => t.Guid, f => f.Random.Guid())
                    .RuleFor(t => t.NullableGuid, f => f.Random.Guid().OrNull(f))
                    .RuleFor(t => t.GuidArray, f => GetRandomItems(Enumerable.Range(1, 10).Select(t => Guid.NewGuid())))
                    .RuleFor(t => t.NullableGuidArray, f => GetRandomItems(Enumerable.Range(1, 10).Select(t => Guid.NewGuid().OrNull(f))))
                    .RuleFor(t => t.GuidCollection, f => GetRandomItems(Enumerable.Range(1, 10).Select(t => Guid.NewGuid())))
                    .RuleFor(t => t.NullableGuidCollection, f => GetRandomItems(Enumerable.Range(1, 10).Select(t => Guid.NewGuid().OrNull(f))))
                    //// Booleans
                    .RuleFor(t => t.Boolean, f => f.Random.Bool())
                    .RuleFor(t => t.NullableBoolean, f => f.Random.Bool().OrNull(f))
                    .RuleFor(t => t.BooleanArray, f => GetRandomItems(Enumerable.Range(1, 5000)).Select(t => t % 2 == 0))
                    .RuleFor(t => t.NullableBooleanArray, f => GetRandomItems(Enumerable.Range(1, 5000)).Select(t => (t % 2 != 0).OrNull(f)))
                    .RuleFor(t => t.BooleanCollection, f => GetRandomItems(Enumerable.Range(1, 5000)).Select(t => t % 2 == 0))
                    .RuleFor(t => t.NullableBooleanCollection, f => GetRandomItems(Enumerable.Range(1, 5000)).Select(t => (t % 2 != 0).OrNull(f)))
                    //// Objects
                    .RuleFor(t => t.Object, f => f.PickRandom<Person>());
        }

        /// <summary>
        ///     Gets an collection of <see cref="Hydra" /> objects filled with fake data.
        /// </summary>
        /// <param name="count">
        ///     The number of entries to be generated.
        /// </param>
        /// <returns>
        ///     The collection <see cref="Hydra" /> objects.
        /// </returns>
        internal static List<Hydra> GetFakeHydraCollection(int count = 150)
        {
            return HydraFaker.Generate(count);
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

        /// <summary>
        ///     Generates a sequence of <see cref="DateTime"/> objects within a specified range.
        /// </summary>
        /// <param name="startDate">
        ///     The value of the first <see cref="DateTime"/> in the sequence.
        /// </param>
        /// <param name="endDate">
        ///     The value of the last <see cref="DateTime"/> in the sequence.
        /// </param>
        /// <returns>
        ///     The collection containing the range of <see cref="DateTime"/> objects.
        /// </returns>
        internal static IEnumerable<DateTime> Range(this DateTime startDate, DateTime endDate)
        {
            return Enumerable.Range(0, (endDate - startDate).Days + 1).Select(d => startDate.AddDays(d));
        }
    }
}