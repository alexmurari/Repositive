namespace KraftCore.Utils.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides extensions for the <see cref="Type"/> type.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns whether the provided type is a numeric type.
        /// </summary>
        /// <param name="type">
        /// The type to be checked.
        /// </param>
        /// <returns>
        /// True if the type is a numeric type; otherwise, false.
        /// </returns>
        public static bool IsNumeric(this Type type) => type == typeof(byte) || type == typeof(sbyte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong)
            || type == typeof(short) || type == typeof(int) || type == typeof(long) || type == typeof(float) || type == typeof(double) || type == typeof(decimal);

        /// <summary>
        /// Returns whether the provided type is a <see cref="string"/> type.
        /// </summary>
        /// <param name="type">
        /// The type to be checked.
        /// </param>
        /// <returns>
        /// True if the type is a string type; otherwise, false.
        /// </returns>
        public static bool IsString(this Type type) => type == typeof(string);

        /// <summary>
        /// Returns whether the provided type is a collection type.
        /// </summary>
        /// <param name="type">The type to be checked.</param>
        /// <returns>True if it is a collection type; otherwise, false.</returns>
        /// <remarks>
        /// Although <see cref="string"/> implements <see cref="IEnumerable{T}"/>, it is not considered a collection type by this method.
        /// </remarks>
        public static bool IsCollection(this Type type)
        {
            if (type == typeof(string))
                return false;

            return type == typeof(IEnumerable) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)) || type.GetInterfaces()
                .Any(t => t == typeof(IEnumerable) || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>)));
        }

        /// <summary>
        /// Returns whether the provided type is a generic collection type.
        /// </summary>
        /// <param name="type">
        /// The type to be checked.
        /// </param>
        /// <returns>
        /// True if the type is a generic collection; otherwise, false.
        /// </returns>
        /// <remarks>
        /// Although <see cref="string"/> implements <see cref="IEnumerable{T}"/>, it is not considered a collection type by this method.
        /// </remarks>
        public static bool IsGenericCollection(this Type type)
        {
            if (type == typeof(string))
                return false;

            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                || type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        /// <summary>
        /// Returns whether the provided type is a generic collection and the generic type parameter is the same as the provided type.
        /// </summary>
        /// <param name="type">
        /// The type to be checked.
        /// </param>
        /// <param name="genericTypeArgument">
        /// The generic type argument of the collection.
        /// </param>
        /// <returns>
        /// True if the type is a generic collection of the provided generic type argument; otherwise, false.
        /// </returns>
        /// <remarks>
        /// Although <see cref="string"/> implements <see cref="IEnumerable{T}"/>, it is not considered a collection type by this method.
        /// </remarks>
        public static bool IsGenericCollection(this Type type, Type genericTypeArgument)
        {
            if (type == typeof(string))
                return false;

            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>) && type.GetGenericArguments()[0] == genericTypeArgument)
                || type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>) && t.GetGenericArguments()[0] == genericTypeArgument);
        }

        /// <summary>
        /// Returns whether the provided type is a non-generic <see cref="IList"/> implementation.
        /// </summary>
        /// <param name="type">
        /// The type to be checked.
        /// </param>
        /// <returns>
        /// True if the type is a non-generic <see cref="IList"/>; otherwise, false.
        /// </returns>
        public static bool IsNonGenericIList(this Type type)
        {
            return type == typeof(IList) || type.GetInterfaces().Any(t => t == typeof(IList));
        }
    }
}