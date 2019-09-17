namespace KraftCore.Utils.Extensions
{
    using System;

    /// <summary>
    /// Extensions for the <see cref="Type"/> type.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns whether the provided type is a numeric type.
        /// </summary>
        /// <param name="o">
        /// The type to be checked.
        /// </param>
        /// <returns>
        /// True if the type is a numeric type; otherwise, false.
        /// </returns>
        public static bool IsNumeric(this Type o) => o == typeof(byte) || o == typeof(sbyte) || o == typeof(ushort) || o == typeof(uint) || o == typeof(ulong) || o == typeof(short)
            || o == typeof(int) || o == typeof(long) || o == typeof(float) || o == typeof(double) || o == typeof(decimal);
    }
}