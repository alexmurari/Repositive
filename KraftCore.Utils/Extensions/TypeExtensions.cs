namespace KraftCore.Utils.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extensions for the <see cref="Type"/> type.
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
        /// Returns whether the provided type is a generic <see cref="ICollection{T}"/> implementation.
        /// </summary>
        /// <param name="type">
        /// The type to be checked.
        /// </param>
        /// <returns>
        /// True if the type is a generic collection; otherwise, false.
        /// </returns>
        public static bool IsGenericCollection(this Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICollection<>))
                || type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>));
        }

        /// <summary>
        /// Returns whether the provided type is a non-generic <see cref="IList"/> implementation.
        /// </summary>
        /// <param name="type">
        /// The type to be checked.
        /// </param>
        /// <returns>
        /// True if the type is a generic collection; otherwise, false.
        /// </returns>
        public static bool IsNonGenericList(this Type type)
        {
            return type == typeof(IList) || type.GetInterfaces().Any(t => t == typeof(IList));
        }
    }
}