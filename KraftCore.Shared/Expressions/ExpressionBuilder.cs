namespace KraftCore.Shared.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using KraftCore.Shared.Extensions;

    /// <summary>
    ///     Provides static methods to create <see cref="Expression" /> instances.
    /// </summary>
    public static class ExpressionBuilder
    {
        /// <summary>
        ///     Creates a lambda expression that represents an accessor to a property from an object of type <typeparamref name="T" />.
        /// </summary>
        /// <param name="propertyNameOrPath">
        ///     The name or the path to the property to be accessed composed of simple dot-separated property access expressions.
        /// </param>
        /// <typeparam name="T">
        ///     The type that contains the property to be accessed.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The type of the accessed property used as the delegate return type.
        /// </typeparam>
        /// <returns>
        ///     The built <see cref="Expression{TDelegate}" /> instance representing the property accessor.
        /// </returns>
        public static Expression<Func<T, TResult>> CreateAccessorExpression<T, TResult>(string propertyNameOrPath)
        {
            propertyNameOrPath.ThrowIfNullOrWhitespace(nameof(propertyNameOrPath));

            var (parameter, accessor) = BuildAccessor<T>(propertyNameOrPath);
            var conversion = Expression.Convert(accessor, typeof(TResult));

            return Expression.Lambda<Func<T, TResult>>(conversion, parameter);
        }

        /// <summary>
        ///     Creates a binary lambda expression that compares the value of an property from an object of
        ///     type <typeparamref name="T" /> with the provided value using the specified comparison operator.
        /// </summary>
        /// <typeparam name="T">
        ///     The type that contains the property to be compared.
        /// </typeparam>
        /// <param name="propertyNameOrPath">
        ///     The name or the path to access the property to be compared composed of simple dot-separated property access
        ///     expressions.
        /// </param>
        /// <param name="value">
        ///     The value to compare the property.
        /// </param>
        /// <param name="operator">
        ///     The comparison operator.
        /// </param>
        /// <returns>The built <see cref="Expression{TDelegate}" /> instance representing the binary operation.</returns>
        public static Expression<Func<T, bool>> CreateBinaryExpression<T>(string propertyNameOrPath, object value, ExpressionOperator @operator)
        {
            return BuildBinaryExpression<T>(propertyNameOrPath, value, @operator);
        }

        /// <summary>
        ///     Creates a binary lambda expression that compares the value of an property from an object of
        ///     type <typeparamref name="T" /> with the provided value using the specified comparison operator.
        /// </summary>
        /// <typeparam name="T">
        ///     The type that contains the property to be compared.
        /// </typeparam>
        /// <param name="propertyInfo">
        ///     The metadata of the property to be compared.
        /// </param>
        /// <param name="value">
        ///     The value to compare the property.
        /// </param>
        /// <param name="operator">
        ///     The comparison operator.
        /// </param>
        /// <returns>The built <see cref="Expression{TDelegate}" /> instance representing the binary operation.</returns>
        public static Expression<Func<T, bool>> CreateBinaryExpression<T>(PropertyInfo propertyInfo, object value, ExpressionOperator @operator)
        {
            return BuildBinaryExpression<T>(propertyInfo.Name, value, @operator);
        }

        /// <summary>
        ///     Creates a <see cref="MemberExpression" /> that represents accessing a property from an object of type <typeparamref name="T" />.
        /// </summary>
        /// <param name="propertyNameOrPath">
        ///     The name or the path to the property to be accessed composed of simple dot-separated property access expressions.
        /// </param>
        /// <typeparam name="T">
        ///     The type that contains the property to be accessed.
        /// </typeparam>
        /// <returns>
        ///     The <see cref="ParameterExpression" /> representing a parameter of the type that contains
        ///     the accessed property and the <see cref="MemberExpression" /> representing the accessor to the property.
        /// </returns>
        private static (ParameterExpression Parameter, MemberExpression Accessor) BuildAccessor<T>(string propertyNameOrPath)
        {
            propertyNameOrPath.ThrowIfNullOrWhitespace(nameof(propertyNameOrPath));

            var param = Expression.Parameter(typeof(T));
            var accessor = propertyNameOrPath.Split('.').Aggregate<string, MemberExpression>(
                null,
                (current, property) => Expression.Property((Expression)current ?? param, property.Trim()));

            return (param, accessor);
        }

        /// <summary>
        ///     Creates a binary lambda expression that compares the value of an property from an object of
        ///     type <typeparamref name="T" /> with the provided value using the specified comparison operator.
        /// </summary>
        /// <typeparam name="T">
        ///     The type with the property to be compared.
        /// </typeparam>
        /// <param name="propertyNameOrPath">
        ///     The name or the path to access the property to be compared composed of simple dot-separated property access expressions.
        /// </param>
        /// <param name="value">
        ///     The value to compare the property.
        /// </param>
        /// <param name="operator">
        ///     The comparison operator.
        /// </param>
        /// <returns>The built <see cref="Expression{TDelegate}" /> instance representing the binary operation.</returns>
        private static Expression<Func<T, bool>> BuildBinaryExpression<T>(string propertyNameOrPath, object value, ExpressionOperator @operator)
        {
            propertyNameOrPath.ThrowIfNullOrWhitespace(nameof(propertyNameOrPath));
            value.ThrowIfNull(nameof(value));

            var (parameter, property) = BuildAccessor<T>(propertyNameOrPath);
            var (leftExpression, rightExpression) = BuildBinaryExpressionParameters(property, value, @operator);

            Expression body;

            switch (@operator)
            {
                case ExpressionOperator.LessThan:
                    body = Expression.LessThan(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.GreaterThan:
                    body = Expression.GreaterThan(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.LessThanOrEqual:
                    body = Expression.LessThanOrEqual(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.GreaterThanOrEqual:
                    body = Expression.GreaterThanOrEqual(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.Equal:
                    body = Expression.Equal(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.NotEqual:
                    body = Expression.NotEqual(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.Contains when property.Type.IsString():
                    body = ExpressionMethodCallBuilder.BuildStringContainsMethodCall(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.Contains when property.Type.IsGenericCollection(typeof(string)):
                case ExpressionOperator.ContainsOnValue when leftExpression.Type.IsGenericCollection(typeof(string)):
                    body = ExpressionMethodCallBuilder.BuildGenericStringCollectionContainsMethodCall(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.Contains when property.Type.IsGenericCollection():
                case ExpressionOperator.ContainsOnValue when leftExpression.Type.IsGenericCollection():
                    body = ExpressionMethodCallBuilder.BuildGenericCollectionContainsMethodCall(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.Contains when property.Type.IsNonGenericIList():
                case ExpressionOperator.ContainsOnValue when leftExpression.Type.IsNonGenericIList():
                    body = ExpressionMethodCallBuilder.BuildIListContainsMethodCall(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.StartsWith:
                    body = ExpressionMethodCallBuilder.BuildStringStartsWithMethodCall(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.EndsWith:
                    body = ExpressionMethodCallBuilder.BuildStringEndsWithMethodCall(leftExpression, rightExpression);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
            }

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        /// <summary>
        ///     Creates the <see cref="Expression" /> parameters that can be used to build <see cref="BinaryExpression" /> objects.
        /// </summary>
        /// <param name="property">
        ///     The expression representing the property accessor.
        /// </param>
        /// <param name="value">
        ///     The value to compare the property.
        /// </param>
        /// <param name="operator">
        ///     The comparison operator.
        /// </param>
        /// <returns>
        ///     The expression parameters to build <see cref="BinaryExpression" /> objects.
        /// </returns>
        private static (Expression leftExpression, Expression rightExpression) BuildBinaryExpressionParameters(Expression property, object value, ExpressionOperator @operator)
        {
            Expression leftExpression = null;
            Expression rightExpression = null;

            var propertyType = @operator != ExpressionOperator.ContainsOnValue ? property.Type : value.GetType();
            var isCollection = propertyType.IsCollection();

            if (isCollection)
                BuildBinaryExpressionParametersForCollection(ref leftExpression, ref rightExpression, property, propertyType, value, @operator);
            else if (propertyType.IsString())
                BuildBinaryExpressionParametersForString(ref leftExpression, ref rightExpression, property, propertyType, value, @operator);
            else if (propertyType.IsNumeric())
                BuildBinaryExpressionParametersForNumeric(ref leftExpression, ref rightExpression, property, propertyType, value, @operator);
            else if (propertyType.IsDateTime())
                BuildBinaryExpressionParametersForDateTime(ref leftExpression, ref rightExpression, property, propertyType, value, @operator);
            else
                BuildBinaryExpressionParametersForObject(ref leftExpression, ref rightExpression, property, propertyType, value, @operator);

            return (leftExpression ?? property, rightExpression ?? Expression.Constant(value));
        }

        /// <summary>
        ///     Creates the <see cref="Expression" /> parameters that can be used to build
        ///     <see cref="BinaryExpression" /> objects when the property to be compared is a collection type.
        /// </summary>
        /// <param name="leftExpression">
        ///     The left expression reference.
        /// </param>
        /// <param name="rightExpression">
        ///     The right expression reference.
        /// </param>
        /// <param name="property">
        ///     The expression representing the property accessor.
        /// </param>
        /// <param name="propertyType">
        ///     The property type.
        /// </param>
        /// <param name="value">
        ///     The value to be compared.
        /// </param>
        /// <param name="operator">
        ///     The comparison operator.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Exception thrown when the comparison operator value is not supported.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Exception thrown when the comparison operator value is out of range.
        /// </exception>
        private static void BuildBinaryExpressionParametersForCollection(
            ref Expression leftExpression,
            ref Expression rightExpression,
            Expression property,
            Type propertyType,
            object value,
            ExpressionOperator @operator)
        {
            var isGenericCollection = propertyType.IsGenericCollection();

            switch (@operator)
            {
                case ExpressionOperator.Contains:
                    if (isGenericCollection && !propertyType.IsArray)
                    {
                        var genericType = propertyType.GetGenericArguments()[0];

                        if (genericType.IsNumeric() && !value.GetType().IsNumeric())
                            value = ParseStringToNumber(value, genericType);
                        else if (genericType.IsDateTime() && !value.GetType().IsDateTime())
                            value = ParseStringToDateTime(value);
                    }

                    leftExpression = property;
                    rightExpression = Expression.Constant(value);
                    break;
                case ExpressionOperator.ContainsOnValue when isGenericCollection:
                    propertyType = property.Type;
                    value = value.ToList();

                    if (propertyType.IsNumeric() && value.GetType().IsGenericCollection(typeof(string)))
                        value = ParseStringCollectionToNumber(value, propertyType);
                    else if (propertyType.IsDateTime() && value.GetType().IsGenericCollection(typeof(string)))
                        value = ParseStringCollectionToDateTime(value);

                    leftExpression = Expression.Constant(value);
                    rightExpression = property;
                    break;
                case ExpressionOperator.ContainsOnValue:
                    leftExpression = Expression.Constant(value);
                    rightExpression = property;
                    break;
                case ExpressionOperator.Equal:
                case ExpressionOperator.NotEqual:
                    leftExpression = property;
                    rightExpression = Expression.Constant(value);
                    break;
                case ExpressionOperator.LessThan:
                case ExpressionOperator.LessThanOrEqual:
                case ExpressionOperator.GreaterThan:
                case ExpressionOperator.GreaterThanOrEqual:
                case ExpressionOperator.StartsWith:
                case ExpressionOperator.EndsWith:
                    throw new ArgumentException($"Operator {@operator} isn't valid for the type {propertyType.Name}.", nameof(@operator));
                default:
                    throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
            }
        }

        /// <summary>
        ///     Creates the <see cref="Expression" /> parameters that can be used to build
        ///     <see cref="BinaryExpression" /> objects when the property to be compared is a <see cref="DateTime" /> type.
        /// </summary>
        /// <param name="leftExpression">
        ///     The left expression reference.
        /// </param>
        /// <param name="rightExpression">
        ///     The right expression reference.
        /// </param>
        /// <param name="property">
        ///     The expression representing the property accessor.
        /// </param>
        /// <param name="propertyType">
        ///     The property type.
        /// </param>
        /// <param name="value">
        ///     The value to be compared.
        /// </param>
        /// <param name="operator">
        ///     The comparison operator.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Exception thrown when the comparison operator value is not supported.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Exception thrown when the comparison operator value is out of range.
        /// </exception>
        private static void BuildBinaryExpressionParametersForDateTime(
            ref Expression leftExpression,
            ref Expression rightExpression,
            Expression property,
            MemberInfo propertyType,
            object value,
            ExpressionOperator @operator)
        {
            switch (@operator)
            {
                case ExpressionOperator.Equal:
                case ExpressionOperator.NotEqual:
                case ExpressionOperator.LessThan:
                case ExpressionOperator.LessThanOrEqual:
                case ExpressionOperator.GreaterThan:
                case ExpressionOperator.GreaterThanOrEqual:
                    leftExpression = property;
                    break;
                case ExpressionOperator.Contains:
                case ExpressionOperator.ContainsOnValue:
                case ExpressionOperator.StartsWith:
                case ExpressionOperator.EndsWith:
                    throw new ArgumentException($"Operator {@operator} isn't valid for the type {propertyType.Name}.", nameof(@operator));
                default:
                    throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
            }

            rightExpression = value.GetType().IsDateTime() ? Expression.Constant(value) : Expression.Constant(ParseStringToDateTime(value));
        }

        /// <summary>
        ///     Creates the <see cref="Expression" /> parameters that can be used to build
        ///     <see cref="BinaryExpression" /> objects when the property to be compared is a numeric type.
        /// </summary>
        /// <param name="leftExpression">
        ///     The left expression reference.
        /// </param>
        /// <param name="rightExpression">
        ///     The right expression reference.
        /// </param>
        /// <param name="property">
        ///     The expression representing the property accessor.
        /// </param>
        /// <param name="propertyType">
        ///     The property type.
        /// </param>
        /// <param name="value">
        ///     The value to be compared.
        /// </param>
        /// <param name="operator">
        ///     The comparison operator.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Exception thrown when the comparison operator value is not supported.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Exception thrown when the comparison operator value is out of range.
        /// </exception>
        private static void BuildBinaryExpressionParametersForNumeric(
            ref Expression leftExpression,
            ref Expression rightExpression,
            Expression property,
            MemberInfo propertyType,
            object value,
            ExpressionOperator @operator)
        {
            switch (@operator)
            {
                case ExpressionOperator.Equal:
                case ExpressionOperator.NotEqual:
                case ExpressionOperator.LessThan:
                case ExpressionOperator.LessThanOrEqual:
                case ExpressionOperator.GreaterThan:
                case ExpressionOperator.GreaterThanOrEqual:
                    leftExpression = property;
                    break;
                case ExpressionOperator.Contains:
                case ExpressionOperator.ContainsOnValue:
                case ExpressionOperator.StartsWith:
                case ExpressionOperator.EndsWith:
                    throw new ArgumentException($"Operator {@operator} isn't valid for the type {propertyType.Name}.", nameof(@operator));
                default:
                    throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
            }

            rightExpression = value.GetType().IsNumeric() ? Expression.Constant(value) : Expression.Constant(ParseStringToNumber(value, property.Type));
        }

        /// <summary>
        ///     Creates the <see cref="Expression" /> parameters that can be used to build <see cref="BinaryExpression" />
        ///     objects when the property to be compared is a non-specific/complex <see cref="object" /> type.
        /// </summary>
        /// <param name="leftExpression">
        ///     The left expression reference.
        /// </param>
        /// <param name="rightExpression">
        ///     The right expression reference.
        /// </param>
        /// <param name="property">
        ///     The expression representing the property accessor.
        /// </param>
        /// <param name="propertyType">
        ///     The property type.
        /// </param>
        /// <param name="value">
        ///     The value to be compared.
        /// </param>
        /// <param name="operator">
        ///     The comparison operator.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Exception thrown when the comparison operator value is not supported.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Exception thrown when the comparison operator value is out of range.
        /// </exception>
        private static void BuildBinaryExpressionParametersForObject(
            ref Expression leftExpression,
            ref Expression rightExpression,
            Expression property,
            MemberInfo propertyType,
            object value,
            ExpressionOperator @operator)
        {
            switch (@operator)
            {
                case ExpressionOperator.Equal:
                case ExpressionOperator.NotEqual:
                    leftExpression = property;
                    rightExpression = Expression.Constant(value);
                    break;
                case ExpressionOperator.LessThan:
                case ExpressionOperator.LessThanOrEqual:
                case ExpressionOperator.GreaterThan:
                case ExpressionOperator.GreaterThanOrEqual:
                case ExpressionOperator.Contains:
                case ExpressionOperator.ContainsOnValue:
                case ExpressionOperator.StartsWith:
                case ExpressionOperator.EndsWith:
                    throw new ArgumentException($"Operator {@operator} isn't valid for the type {propertyType.Name}.", nameof(@operator));
                default:
                    throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
            }
        }

        /// <summary>
        ///     Creates the <see cref="Expression" /> parameters that can be used to build <see cref="BinaryExpression" />
        ///     objects when the property to be compared is a <see cref="string" /> type.
        /// </summary>
        /// <param name="leftExpression">
        ///     The left expression reference.
        /// </param>
        /// <param name="rightExpression">
        ///     The right expression reference.
        /// </param>
        /// <param name="property">
        ///     The expression representing the property accessor.
        /// </param>
        /// <param name="propertyType">
        ///     The property type.
        /// </param>
        /// <param name="value">
        ///     The value to be compared.
        /// </param>
        /// <param name="operator">
        ///     The comparison operator.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Exception thrown when the comparison operator value is not supported.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Exception thrown when the comparison operator value is out of range.
        /// </exception>
        private static void BuildBinaryExpressionParametersForString(
            ref Expression leftExpression,
            ref Expression rightExpression,
            Expression property,
            MemberInfo propertyType,
            object value,
            ExpressionOperator @operator)
        {
            switch (@operator)
            {
                case ExpressionOperator.Equal:
                case ExpressionOperator.NotEqual:
                case ExpressionOperator.Contains:
                case ExpressionOperator.StartsWith:
                case ExpressionOperator.EndsWith:
                    leftExpression = Expression.Call(property, typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes) ?? throw new InvalidOperationException());
                    rightExpression = Expression.Constant(value?.ToString().ToLower());
                    break;
                case ExpressionOperator.LessThan:
                case ExpressionOperator.LessThanOrEqual:
                case ExpressionOperator.GreaterThan:
                case ExpressionOperator.GreaterThanOrEqual:
                case ExpressionOperator.ContainsOnValue:
                    throw new ArgumentException($"Operator {@operator} isn't valid for the type {propertyType.Name}.", nameof(@operator));
                default:
                    throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
            }
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
        ///     The <see cref="ConstantExpression"/> representing the converted value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static object ParseStringToNumber(object value, Type propertyType)
        {
            var parameters = new[]
            {
                value, null
            };

            var argumentTypes = new[]
            {
                value.GetType(), propertyType.MakeByRefType()
            };

            var parseSuccess = (bool?)propertyType.GetMethod(nameof(int.TryParse), argumentTypes)?.Invoke(null, parameters);

            if (parseSuccess.GetValueOrDefault())
                return parameters[1];

            throw new ArgumentException($"Value '{value}' of type '{value.GetType().Name}' isn't valid for comparing with values of type '{propertyType.Name}'.", nameof(value));
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
        ///     The <see cref="ConstantExpression"/> representing the converted value.
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
            {
                result.Add(ParseStringToNumber(str, propertyType));
            }

            return result;
        }

        /// <summary>
        ///     Converts the string representation of a date and time to it's <see cref="DateTime"/> equivalent.
        /// </summary>
        /// <param name="value">
        ///     The value to be converted.
        /// </param>
        /// <returns>
        ///     The <see cref="ConstantExpression"/> representing the converted value.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static object ParseStringToDateTime(object value)
        {
            var propertyType = typeof(DateTime);

            var parameters = new[]
            {
                value, null
            };

            var argumentTypes = new[]
            {
                value.GetType(), propertyType.MakeByRefType()
            };

            var parseSuccess = (bool?)propertyType.GetMethod(nameof(DateTime.TryParse), argumentTypes)?.Invoke(null, parameters);

            if (parseSuccess.GetValueOrDefault())
                return parameters[1];

            throw new ArgumentException($"Value '{value}' of type '{value.GetType().Name}' isn't valid for comparing with values of type '{propertyType.Name}'.", nameof(value));
        }

        /// <summary>
        ///     Converts an collection of strings representing a date and time to it's <see cref="DateTime"/> equivalents.
        /// </summary>
        /// <param name="value">
        ///     The collection to be converted.
        /// </param>
        /// <returns>
        ///     The object representing the converted collection.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     The exception thrown when the value cannot be converted.
        /// </exception>
        private static object ParseStringCollectionToDateTime(object value)
        {
            var propertyType = typeof(DateTime);

            if (!(value is IEnumerable<string> collection))
                return null;

            var result = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(propertyType));

            foreach (var str in collection)
            {
                result.Add(ParseStringToDateTime(str));
            }

            return result;
        }

        /// <summary>
        ///     Returns an <see cref="List{T}" /> from an generic collection of objects by enumerating it.
        /// </summary>
        /// <param name="collection">
        ///     The collection to be enumerated.
        /// </param>
        /// <returns>
        ///     The <see cref="object" /> representing the generic list.
        /// </returns>
        private static object ToList(this object collection)
        {
            var colType = collection.GetType();

            if (colType.IsArray)
                return collection;

            if (!colType.IsGenericCollection())
                throw new ArgumentException("Parameter must be a generic collection to be enumerated.", nameof(collection));

            var valueGenericArgs = colType.GetGenericArguments();

            if (valueGenericArgs.Length == 1 && colType.IsAssignableFrom(typeof(List<>).MakeGenericType(valueGenericArgs[0])))
                return collection;

            var containsMethod = typeof(Enumerable).GetMethods()
                .Single(x => x.Name == nameof(Enumerable.ToList) && x.GetParameters().Length == 1)
                .MakeGenericMethod(valueGenericArgs[valueGenericArgs.Length - 1]);

            var parameters = new[]
            {
                collection
            };

            return containsMethod.Invoke(null, parameters);
        }
    }
}