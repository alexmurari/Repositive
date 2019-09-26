namespace KraftCore.Utils.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using KraftCore.Utils.Extensions;

    /// <summary>
    /// Provides static methods to create <see cref="Expression"/> instances.
    /// </summary>
    public static class ExpressionBuilder
    {
        /// <summary>
        /// Creates a lambda expression that represents an accessor to a property from an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="propertyNameOrPath">
        /// The name or the path to the property to be accessed composed of simple dot-separated property access expressions.
        /// </param>
        /// <typeparam name="T">
        /// The type that contains the property to be accessed.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the accessed property used as the delegate return type.
        /// </typeparam>
        /// <returns>
        /// The built <see cref="Expression{TDelegate}"/> instance representing the property accessor.
        /// </returns>
        public static Expression<Func<T, TResult>> CreateAccessorExpression<T, TResult>(string propertyNameOrPath)
        {
            var (parameter, accessor) = BuildAccessor<T>(propertyNameOrPath);
            var conversion = Expression.Convert(accessor, typeof(TResult));

            return Expression.Lambda<Func<T, TResult>>(conversion, parameter);
        }

        /// <summary>
        /// Creates a binary lambda expression that compares the value of an property from an object of
        /// type <typeparamref name="T"/> with the provided value using the specified comparison operator.
        /// </summary>
        /// <typeparam name="T">
        /// The type that contains the property to be compared.
        /// </typeparam>
        /// <param name="propertyNameOrPath">
        /// The name or the path to access the property to be compared composed of simple dot-separated property access expressions.
        /// </param>
        /// <param name="value">
        /// The value to compare the property.
        /// </param>
        /// <param name="operator">
        /// The comparison operator.
        /// </param>
        /// <returns>The built <see cref="Expression{TDelegate}"/> instance representing the binary operation.</returns>
        public static Expression<Func<T, bool>> CreateBinaryExpression<T>(string propertyNameOrPath, object value, ExpressionOperator @operator)
        {
            return BuildBinaryExpression<T>(propertyNameOrPath, value, @operator);
        }

        /// <summary>
        /// Creates a binary lambda expression that compares the value of an property from an object of
        /// type <typeparamref name="T"/> with the provided value using the specified comparison operator.
        /// </summary>
        /// <typeparam name="T">
        /// The type that contains the property to be compared.
        /// </typeparam>
        /// <param name="propertyInfo">
        /// The metadata of the property to be compared.
        /// </param>
        /// <param name="value">
        /// The value to compare the property.
        /// </param>
        /// <param name="operator">
        /// The comparison operator.
        /// </param>
        /// <returns>The built <see cref="Expression{TDelegate}"/> instance representing the binary operation.</returns>
        public static Expression<Func<T, bool>> CreateBinaryExpression<T>(PropertyInfo propertyInfo, object value, ExpressionOperator @operator)
        {
            return BuildBinaryExpression<T>(propertyInfo.Name, value, @operator);
        }

        /// <summary>
        /// Creates a <see cref="MemberExpression"/> that represents accessing a property from an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="propertyNameOrPath">
        /// The name or the path to the property to be accessed composed of simple dot-separated property access expressions.
        /// </param>
        /// <typeparam name="T">
        /// The type that contains the property to be accessed.
        /// </typeparam>
        /// <returns>
        /// The <see cref="ParameterExpression"/> representing a parameter of the type that contains 
        /// the accessed property and the <see cref="MemberExpression"/> representing the accessor to the property.
        /// </returns>
        private static (ParameterExpression Parameter, MemberExpression Accessor) BuildAccessor<T>(string propertyNameOrPath)
        {
            var param = Expression.Parameter(typeof(T));
            var accessor = propertyNameOrPath.Split('.').Aggregate<string, MemberExpression>(
                null,
                (current, property) => Expression.Property((Expression)current ?? param, property.Trim()));

            return (param, accessor);
        }

        /// <summary>
        /// Creates a binary lambda expression that compares the value of an property from an object of
        /// type <typeparamref name="T"/> with the provided value using the specified comparison operator.
        /// </summary>
        /// <typeparam name="T">
        /// The type with the property to be compared.
        /// </typeparam>
        /// <param name="propertyNameOrPath">
        /// The name or the path to access the property to be compared composed of simple dot-separated property access expressions.
        /// </param>
        /// <param name="value">
        /// The value to compare the property.
        /// </param>
        /// <param name="operator">
        /// The comparison operator.
        /// </param>
        /// <returns>The built <see cref="Expression{TDelegate}"/> instance representing the binary operation.</returns>
        private static Expression<Func<T, bool>> BuildBinaryExpression<T>(string propertyNameOrPath, object value, ExpressionOperator @operator)
        {
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
        /// Creates the <see cref="Expression"/> parameters that can be used to build <see cref="BinaryExpression"/> objects.
        /// </summary>
        /// <param name="property">
        /// The expression representing the property accessor.
        /// </param>
        /// <param name="value">
        /// The value to compare the property.
        /// </param>
        /// <param name="operator">
        /// The comparison operator.
        /// </param>
        /// <returns>
        /// The expression parameters to build <see cref="BinaryExpression"/> objects.
        /// </returns>
        private static (Expression leftExpression, Expression rightExpression) BuildBinaryExpressionParameters(Expression property, object value, ExpressionOperator @operator)
        {
            Expression leftExpression = null;
            Expression rightExpression = null;

            var propertyType = @operator != ExpressionOperator.ContainsOnValue ? property.Type : value.GetType();
            var isCollection = propertyType.IsCollection();

            if (isCollection)
            {
                var isGenericCollection = propertyType.IsGenericCollection();

                switch (@operator)
                {
                    case ExpressionOperator.Contains when isGenericCollection:
                        rightExpression = Expression.Constant(value);
                        break;
                    case ExpressionOperator.Contains:
                        rightExpression = Expression.Constant(value, typeof(object));
                        break;
                    case ExpressionOperator.ContainsOnValue when isGenericCollection:
                        leftExpression = Expression.Constant(value.ToList());
                        rightExpression = property;
                        break;
                    case ExpressionOperator.ContainsOnValue:
                        leftExpression = Expression.Constant(value);
                        rightExpression = property;
                        break;
                    case ExpressionOperator.Equal:
                    case ExpressionOperator.NotEqual:
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
            else
            {
                if (propertyType.IsString())
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
                else if (propertyType.IsNumeric())
                {
                    switch (@operator)
                    {
                        case ExpressionOperator.Equal:
                        case ExpressionOperator.NotEqual:
                        case ExpressionOperator.LessThan:
                        case ExpressionOperator.LessThanOrEqual:
                        case ExpressionOperator.GreaterThan:
                        case ExpressionOperator.GreaterThanOrEqual:
                            break;
                        case ExpressionOperator.Contains:
                        case ExpressionOperator.ContainsOnValue:
                        case ExpressionOperator.StartsWith:
                        case ExpressionOperator.EndsWith:
                            throw new ArgumentException($"Operator {@operator} isn't valid for the type {propertyType.Name}.", nameof(@operator));
                        default:
                            throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
                    }

                    if (!value.GetType().IsNumeric())
                    {
                        var parameters = new[]
                        {
                            value, null
                        };

                        var argumentTypes = new[]
                        {
                            value.GetType(), propertyType.MakeByRefType()
                        };

                        var parseSuccess = (bool?)propertyType.GetMethod(nameof(int.TryParse), argumentTypes)?.Invoke(property, parameters);

                        if (parseSuccess.GetValueOrDefault())
                            rightExpression = Expression.Constant(parameters[1], propertyType);
                        else
                            throw new ArgumentException($"Value '{value}' of type '{value.GetType().Name}' isn't valid for comparing with values of type '{propertyType.Name}'.", nameof(value));
                    }
                }
            }

            return (leftExpression ?? property, rightExpression ?? Expression.Constant(value));
        }

        /// <summary>
        /// Returns an <see cref="List{T}"/> from an generic collection of objects by enumerating it.
        /// </summary>
        /// <param name="collection">
        /// The collection to be enumerated.
        /// </param>
        /// <returns>
        /// The <see cref="object"/> representing the generic list.
        /// </returns>
        private static object ToList(this object collection)
        {
            var colType = collection.GetType();

            if (!colType.IsGenericCollection())
                throw new ArgumentException("Parameter must be a generic collection to be enumerated.", nameof(collection));

            var valueGenericArgs = colType.GetGenericArguments();

            if (colType.IsArray || colType.IsAssignableFrom(typeof(List<>).MakeGenericType(valueGenericArgs[0])))
                return collection;

            var containsMethod = typeof(Enumerable).GetMethods()
                .Where(x => x.Name == nameof(Enumerable.ToList))
                .Single(x => x.GetParameters().Length == 1)
                .MakeGenericMethod(valueGenericArgs[valueGenericArgs.Length - 1]);

            var parameters = new[]
            {
                collection
            };

            return containsMethod.Invoke(null, parameters);
        }
    }
}