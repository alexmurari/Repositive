namespace KraftCore.Tests.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    ///     Represents an person.
    /// </summary>
    public class Person
    {
        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets the full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///     Gets or sets the date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        ///     Gets or sets the age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        ///     Gets or sets the best friend.
        /// </summary>
        public Person BestFriend { get; set; }

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
        ///     Gets or sets the driver's license date.
        /// </summary>
        public DateTime? DateOfDriversLicense { get; set; }

        /// <summary>
        ///     Gets or sets the account balance.
        /// </summary>
        public decimal? AccountBalance { get; set; }

        /// <summary>
        ///     Gets or sets the favorite numbers.
        /// </summary>
        public IEnumerable<int?> LeastFavoriteNumbers { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the person has a pet.
        /// </summary>
        public bool HasPet { get; set; }

        /// <summary>
        ///     Gets or sets the person's unique identifier.
        /// </summary>
        public Guid PersonGuid { get; set; }

        /// <summary>
        ///     Gets or sets the person's optional unique identifier.
        /// </summary>
        public Guid? OptionalPersonGuid { get; set; }
    }
}