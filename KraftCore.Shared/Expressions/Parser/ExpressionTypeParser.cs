namespace KraftCore.Shared.Expressions.Parser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using KraftCore.Shared.Extensions;

    /// <summary>
    ///     Provides static methods for generating expression accessors and constants with parsed/converted object values to match the corresponding accessed property type.
    /// </summary>
    internal static class ExpressionTypeParser
    {
        /// <summary>
        ///     Builds the property accessor and converted value constant according to the specified comparison operator.
        /// </summary>
        /// <param name="property">
        ///     The expression representing the property accessor.
        /// </param>
        /// <param name="value">
        ///     The value to be compared.
        /// </param>
        /// <param name="operator">
        ///     The comparison operator.
        /// </param>
        /// <returns>
        ///     The property accessor expression and parsed value constant expression.
        /// </returns>
        internal static (Expression property, Expression value) BuildAccessorAndValue(Expression property, object value, ExpressionOperator @operator)
        {
            var resultProperty = property;
            Expression resultValue;

            var propertyType = property.Type;
            var valueType = value?.GetType();

            if (value == null || (value is string[] strCollection && strCollection.Contains(null)))
            {
                var type = propertyType.IsGenericCollection() ? propertyType.GetGenericArguments()[0] : propertyType;

                if (type.IsValueType)
                    throw new InvalidOperationException($"Invalid comparison: provided a null value for comparing with a non-nullable type. Type: {type.Name}.");

                if (value == null)
                    return (resultProperty, Expression.Convert(Expression.Constant(null), type));
            }

            if (@operator == ExpressionOperator.ContainsOnValue)
            {
                if (propertyType.IsChar() && valueType.IsGenericCollection(typeof(string)))
                    value = ParseStringCollectionToChar(value, propertyType.IsNullableType());
                else if (propertyType.IsNumeric() && valueType.IsGenericCollection(typeof(string)))
                    value = ParseStringCollectionToNumber(value, propertyType);
                else if (propertyType.IsDateTime() && valueType.IsGenericCollection(typeof(string)))
                    value = ParseStringCollectionToDateTime(value, propertyType.IsNullableType());
                else if (propertyType.IsBoolean() && valueType.IsGenericCollection(typeof(string)))
                    value = ConvertCollectionToBoolean(value, propertyType.IsNullableType());
                else if (propertyType.IsGuid() && valueType.IsGenericCollection(typeof(string)))
                    value = ParseStringCollectionToGuid(value, propertyType.IsNullableType());

                resultValue = Expression.Constant(value);
            }
            else
            {
                if (propertyType.IsCollection() && !propertyType.IsArray)
                {
                    if (propertyType.IsGenericCollection())
                    {
                        var genericType = propertyType.GetGenericArguments()[0];

                        if (genericType.IsChar() && !valueType.IsChar())
                            value = ParseStringToChar(value);
                        else if (genericType.IsNumeric() && !valueType.IsNumeric())
                            value = ParseStringToNumber(value, genericType);
                        else if (genericType.IsDateTime() && !valueType.IsDateTime())
                            value = ParseStringToDateTime(value);
                        else if (genericType.IsBoolean() && !valueType.IsBoolean())
                            value = ConvertToBoolean(value);
                        else if (genericType.IsGuid() && !valueType.IsGuid())
                            value = ParseStringToGuid(value);

                        if (genericType.IsNullableType())
                            resultValue = Expression.Convert(Expression.Constant(value), genericType);
                        else
                            resultValue = Expression.Constant(value);
                    }
                    else
                        resultValue = Expression.Constant(value);
                }
                else if (propertyType.IsArray)
                {
                    var elementType = propertyType.GetElementType();

                    if (elementType.IsChar() && !valueType.IsChar())
                        value = ParseStringToChar(value);
                    else if (elementType.IsNumeric() && !valueType.IsNumeric())
                        value = ParseStringToNumber(value, elementType);
                    else if (elementType.IsDateTime() && !valueType.IsDateTime())
                        value = ParseStringToDateTime(value);
                    else if (elementType.IsBoolean() && !valueType.IsBoolean())
                        value = ConvertToBoolean(value);
                    else if (elementType.IsGuid() && !valueType.IsGuid())
                        value = ParseStringToGuid(value);

                    if (elementType.IsNullableType())
                        resultValue = Expression.Convert(Expression.Constant(value), elementType ?? throw new InvalidOperationException());
                    else
                        resultValue = Expression.Constant(value);
                }
                else if (propertyType.IsString())
                {
                    resultProperty = Expression.Call(property, typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes) ?? throw new InvalidOperationException());
                    resultValue = Expression.Constant(value.ToString().ToLower());
                }
                else if (propertyType.IsChar())
                {
                    resultValue = valueType.IsChar() ? Expression.Constant(value) : Expression.Constant(ParseStringToChar(value));

                    if (propertyType.IsNullableType())
                        resultValue = Expression.Convert(resultValue, propertyType);
                }
                else if (propertyType.IsNumeric())
                {
                    resultValue = valueType.IsNumeric() ? Expression.Constant(value) : Expression.Constant(ParseStringToNumber(value, propertyType));

                    if (propertyType.IsNullableType())
                        resultValue = Expression.Convert(resultValue, propertyType);
                }
                else if (propertyType.IsDateTime())
                {
                    resultValue = valueType.IsDateTime() ? Expression.Constant(value) : Expression.Constant(ParseStringToDateTime(value));

                    if (propertyType.IsNullableType())
                        resultValue = Expression.Convert(resultValue, propertyType);
                }
                else if (propertyType.IsBoolean())
                {
                    resultValue = valueType.IsBoolean() ? Expression.Constant(value) : Expression.Constant(ConvertToBoolean(value));

                    if (propertyType.IsNullableType())
                        resultValue = Expression.Convert(resultValue, propertyType);
                }
                else if (propertyType.IsGuid())
                {
                    resultValue = valueType.IsGuid() ? Expression.Constant(value) : Expression.Constant(ParseStringToGuid(value));

                    if (propertyType.IsNullableType())
                        resultValue = Expression.Convert(resultValue, propertyType);
                }
                else
                    resultValue = Expression.Constant(value);
            }

            return (resultProperty, resultValue);
        }

        /// <summary>
        ///     Converts the string representation of a true or false value to it's <see cref="bool" /> equivalent.
        /// </summary>
        /// <param name="value">
        ///     The value to be converted.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" /> representing the converted value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static bool? ConvertToBoolean(object value)
        {
            if (value == null)
                return null;

            return Convert.ToBoolean(value);
        }

        /// <summary>
        ///     Converts an collection of objects representing a true or false value to it's <see cref="bool" /> equivalents.
        /// </summary>
        /// <param name="value">
        ///     The collection to be converted.
        /// </param>
        /// <param name="isNullable">
        ///     Indicates whether the <see cref="bool" /> type can be null.
        /// </param>
        /// <returns>
        ///     The object representing the converted collection.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static object ConvertCollectionToBoolean(object value, bool isNullable = false)
        {
            if (value is IEnumerable<object> collection)
            {
                if (isNullable)
                    return collection.Select(ConvertToBoolean).ToList();

                return collection.Select(ConvertToBoolean).Cast<bool>().ToList();
            }

            return value;
        }

        /// <summary>
        ///     Converts the string representation of a number to it's numeric equivalent.
        /// </summary>
        /// <param name="value">
        ///     The value to be converted.
        /// </param>
        /// <param name="propertyType">
        ///     The numeric type that the value must be parsed to.
        /// </param>
        /// <returns>
        ///     The <see cref="object" /> representing the converted value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static object ParseStringToNumber(object value, Type propertyType)
        {
            if (value == null)
                return null;

            propertyType = propertyType.IsNullableType() ? Nullable.GetUnderlyingType(propertyType) : propertyType;

            var parameters = new[]
            {
                value, null
            };

            var argumentTypes = new[]
            {
                value.GetType(), propertyType?.MakeByRefType()
            };

            var parseSuccess = (bool?)propertyType?.GetMethod(nameof(int.TryParse), argumentTypes)?.Invoke(null, parameters);

            if (parseSuccess.GetValueOrDefault())
                return parameters[1];

            throw new ArgumentException($"Value '{value}' of type '{value.GetType().Name}' isn't valid for comparing with values of type '{propertyType?.Name}'.", nameof(value));
        }

        /// <summary>
        ///     Converts an collection of strings representing a number to it's numeric equivalents.
        /// </summary>
        /// <param name="value">
        ///     The collection to be converted.
        /// </param>
        /// <param name="propertyType">
        ///     The numeric type that the collection elements must be parsed to.
        /// </param>
        /// <returns>
        ///     The <see cref="object" /> representing the converted value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static object ParseStringCollectionToNumber(object value, Type propertyType)
        {
            if (!(value is IEnumerable<string> collection))
                return null;

            var result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(propertyType));

            foreach (var str in collection)
                result.Add(ParseStringToNumber(str, propertyType));

            return result;
        }

        /// <summary>
        ///     Converts the string representation of a date and time to it's <see cref="DateTime" /> equivalent.
        /// </summary>
        /// <param name="value">
        ///     The value to be converted.
        /// </param>
        /// <returns>
        ///     The <see cref="DateTime" /> representing the converted value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static DateTime? ParseStringToDateTime(object value)
        {
            if (value == null)
                return null;

            if (DateTime.TryParse(value.ToString(), out var result))
                return result;

            throw new ArgumentException($"Value '{value}' of type '{value.GetType().Name}' isn't valid for comparing with values of type '{nameof(DateTime)}'.", nameof(value));
        }

        /// <summary>
        ///     Converts an collection of strings representing a date and time to it's <see cref="DateTime" /> equivalents.
        /// </summary>
        /// <param name="value">
        ///     The collection to be converted.
        /// </param>
        /// <param name="isNullable">
        ///     Indicates whether the <see cref="DateTime" /> type can be null.
        /// </param>
        /// <returns>
        ///     The object representing the converted collection.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static object ParseStringCollectionToDateTime(object value, bool isNullable = false)
        {
            if (value is IEnumerable<string> collection)
            {
                if (isNullable)
                    return collection.Select(ParseStringToDateTime).ToList();

                return collection.Select(ParseStringToDateTime).Cast<DateTime>().ToList();
            }

            return null;
        }

        /// <summary>
        ///     Converts the string representation of a globally unique identifier to it's <see cref="Guid" /> equivalent.
        /// </summary>
        /// <param name="value">
        ///     The value to be converted.
        /// </param>
        /// <returns>
        ///     The <see cref="object" /> representing the converted value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static Guid? ParseStringToGuid(object value)
        {
            if (value == null)
                return null;

            if (Guid.TryParse(value.ToString(), out var result))
                return result;

            throw new ArgumentException($"Value '{value}' of type '{value.GetType().Name}' isn't valid for comparing with values of type '{nameof(Guid)}'.", nameof(value));
        }

        /// <summary>
        ///     Converts an collection of strings representing globally unique identifiers to it's <see cref="Guid" /> equivalents.
        /// </summary>
        /// <param name="value">
        ///     The collection to be converted.
        /// </param>
        /// <param name="isNullable">
        ///     Indicates whether the <see cref="DateTime" /> type can be null.
        /// </param>
        /// <returns>
        ///     The object representing the converted collection.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static object ParseStringCollectionToGuid(object value, bool isNullable = false)
        {
            if (value is IEnumerable<string> collection)
            {
                if (isNullable)
                    return collection.Select(ParseStringToGuid).ToList();

                return collection.Select(ParseStringToGuid).Cast<Guid>().ToList();
            }

            return null;
        }

        /// <summary>
        ///     Converts the string representation of a globally unique identifier to it's <see cref="char" /> equivalent.
        /// </summary>
        /// <param name="value">
        ///     The value to be converted.
        /// </param>
        /// <returns>
        ///     The <see cref="object" /> representing the converted value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static char? ParseStringToChar(object value)
        {
            if (value == null)
                return null;

            if (char.TryParse(value.ToString(), out var result))
                return result;

            throw new ArgumentException($"Value '{value}' of type '{value.GetType().Name}' isn't valid for comparing with values of type '{nameof(Char)}'.", nameof(value));
        }

        /// <summary>
        ///     Converts an collection of strings representing globally unique identifiers to it's <see cref="Guid" /> equivalents.
        /// </summary>
        /// <param name="value">
        ///     The collection to be converted.
        /// </param>
        /// <param name="isNullable">
        ///     Indicates whether the <see cref="DateTime" /> type can be null.
        /// </param>
        /// <returns>
        ///     The object representing the converted collection.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static object ParseStringCollectionToChar(object value, bool isNullable = false)
        {
            if (value is IEnumerable<string> collection)
            {
                if (isNullable)
                    return collection.Select(ParseStringToChar).ToList();

                return collection.Select(ParseStringToChar).Cast<char>().ToList();
            }

            return null;
        }
    }
}
