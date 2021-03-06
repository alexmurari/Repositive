﻿namespace Repositive.EntityFrameworkCore.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Extensions for generic types.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class GenericExtensions
    {
        /// <summary>
        ///     Checks if the provided value is the default value for the type.
        /// </summary>
        /// <param name="value">
        ///     The value to be checked.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the value being checked.
        /// </typeparam>
        /// <returns>
        ///     <see langword="true"/> if default; otherwise, <see langword="false"/>.
        /// </returns>
        internal static bool CheckIfDefault<T>(this T value) where T : struct
        {
            return EqualityComparer<T>.Default.Equals(value, default);
        }

        /// <summary>
        ///     Checks if the provided value is null.
        /// </summary>
        /// <param name="value">
        ///     The value to be checked.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the value being checked.
        /// </typeparam>
        /// <returns>
        ///     <see langword="true"/> if null; otherwise, <see langword="false"/>.
        /// </returns>
        internal static bool CheckIfNull<T>(this T value) where T : class
        {
            return value == null;
        }

        /// <summary>
        ///     Throws an exception if the provided value is the default value for the type, otherwise returns the value.
        /// </summary>
        /// <param name="value">
        ///     The value to be checked.
        /// </param>
        /// <param name="paramName">
        ///     The name of the parameter being checked.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the value being checked.
        /// </typeparam>
        /// <returns>
        ///     The value being checked.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Thrown when the value is the default value for the type.
        /// </exception>
        internal static T ThrowIfDefault<T>(this T value, string paramName) where T : struct
        {
            if (value.CheckIfDefault())
                throw new ArgumentException("The argument value must not be the default value of the type.", paramName);

            return value;
        }

        /// <summary>
        ///     Throws an exception if the provided value is null, otherwise returns the value.
        /// </summary>
        /// <param name="value">
        ///     The value to be checked.
        /// </param>
        /// <param name="paramName">
        ///     The name of the parameter being checked.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the value being checked.
        /// </typeparam>
        /// <returns>
        ///     The value being checked.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when the value is null.
        /// </exception>
        internal static T ThrowIfNull<T>(this T value, string paramName) where T : class
        {
            if (value.CheckIfNull())
                throw new ArgumentNullException(paramName, "The argument value must not be null.");

            return value;
        }
    }
}