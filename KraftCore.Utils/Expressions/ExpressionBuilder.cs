namespace KraftCore.Utils.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using KraftCore.Utils.Extensions;

    /// <summary>
    /// Provides static methods to create <see cref="Expression"/> instances.
    /// </summary>
    public static class ExpressionBuilder
    {
        /// <summary>
        /// Creates a lambda expression that represents an accessor to a property
        /// or field from an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property or field to be accessed.
        /// </param>
        /// <typeparam name="T">
        /// The type that contains the property or field to be accessed.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the accessed property or field used as the delegate return type.
        /// </typeparam>
        /// <returns>
        /// The built <see cref="Expression{TDelegate}"/> instance representing the property accessor.
        /// </returns>
        public static Expression<Func<T, TResult>> CreateAccessorExpression<T, TResult>(string propertyName)
        {
            var param = Expression.Parameter(typeof(T));
            var accessor = Expression.PropertyOrField(param, propertyName);
            var conversion = Expression.Convert(accessor, typeof(TResult));

            return Expression.Lambda<Func<T, TResult>>(conversion, param);
        }

        /// <summary>
        /// Creates a binary lambda expression that compares the value of an property from an object of
        /// type <typeparamref name="T"/> with the provided value using the specified comparison operator.
        /// </summary>
        /// <typeparam name="T">
        /// The type that contains the property to be compared.
        /// </typeparam>
        /// <param name="propertyName">
        /// The name of the property to be compared.
        /// </param>
        /// <param name="value">
        /// The value to compare the property.
        /// </param>
        /// <param name="operator">
        /// The comparison operator.
        /// </param>
        /// <returns>The built <see cref="Expression{TDelegate}"/> instance representing the binary operation.</returns>
        public static Expression<Func<T, bool>> CreateBinaryExpression<T>(string propertyName, object value, ExpressionOperator @operator)
        {
            return BuildBinaryExpression<T>(propertyName, value, @operator);
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
        /// Builds a binary lambda expression that compares the value of an property of
        /// type <typeparamref name="T"/> with the provided value using the specified comparison operator.
        /// </summary>
        /// <typeparam name="T">
        /// The type with the property to be compared.
        /// </typeparam>
        /// <param name="propertyName">
        /// The name of the property to be compared.
        /// </param>
        /// <param name="value">
        /// The value to compare the property.
        /// </param>
        /// <param name="operator">
        /// The comparison operator.
        /// </param>
        /// <returns>The built <see cref="Expression{TDelegate}"/> instance representing the binary operation.</returns>
        private static Expression<Func<T, bool>> BuildBinaryExpression<T>(string propertyName, object value, ExpressionOperator @operator)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);

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
                    body = ExpressionMethodCallBuilder.BuildGenericStringCollectionContainsMethodCall(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.Contains when property.Type.IsGenericCollection():
                    body = ExpressionMethodCallBuilder.BuildGenericCollectionContainsMethodCall(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.Contains when property.Type.IsNonGenericIList():
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
        /// The expression parameters to be used to build <see cref="BinaryExpression"/> instances.
        /// </returns>
        private static (Expression leftExpression, Expression rightExpression) BuildBinaryExpressionParameters(Expression property, object value, ExpressionOperator @operator)
        {
            Expression leftExpression = null;
            Expression rightExpression = null;

            var propertyType = property.Type;
            var isCollection = property.Type.IsCollection();

            if (isCollection)
            {
                var isGenericCollection = property.Type.IsGenericCollection();

                switch (@operator)
                {
                    case ExpressionOperator.Contains when isGenericCollection:
                        rightExpression = Expression.Constant(value, propertyType.GetGenericArguments()[0]);
                        break;
                    case ExpressionOperator.Contains:
                        rightExpression = Expression.Constant(value, typeof(object));
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
                            rightExpression = Expression.Constant(value.ToString().ToLower());
                            break;
                        case ExpressionOperator.LessThan:
                        case ExpressionOperator.LessThanOrEqual:
                        case ExpressionOperator.GreaterThan:
                        case ExpressionOperator.GreaterThanOrEqual:
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
                            throw new ArgumentException($"Value '{value}' of type '{value.GetType().Name}' isn't valid for comparing with values of type '{propertyType.Name}'.",
                                nameof(value));
                    }
                }
            }

            return (leftExpression ?? property, rightExpression ?? Expression.Constant(value));
        }
    }
}