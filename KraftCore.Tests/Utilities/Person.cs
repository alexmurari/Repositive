namespace KraftCore.Tests.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    ///     Represents an person.
    /// </summary>
    internal class Person
    {
        /// <summary>
        ///     Gets or sets the age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        ///     Gets or sets the best friend.
        /// </summary>
        public Person BestFriend { get; set; }

        /// <summary>
        ///     Gets or sets the date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        ///     Gets or sets the favorite colors.
        /// </summary>
        public string[] FavoriteColors { get; set; }

        /// <summary>
        ///     Gets or sets the favorite fruits.
        /// </summary>
        public ArrayList FavoriteFruits { get; set; }

        /// <summary>
        ///     Gets or sets the favorite numbers.
        /// </summary>
        public IEnumerable<int> FavoriteNumbers { get; set; }

        /// <summary>
        ///     Gets or sets the favorite words.
        /// </summary>
        public ICollection<string> FavoriteWords { get; set; }

        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }
    }
}