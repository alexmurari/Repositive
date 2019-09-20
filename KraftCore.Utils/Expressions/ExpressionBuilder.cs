namespace KraftCore.Utils.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
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
        /// The <see cref="Expression"/>.
        /// The built <see cref="Expression{T}"/> instance.
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
        /// <typeparam name="T">The type that contains the property to be compared.</typeparam>
        /// <param name="propertyName">The name of the property to be compared.</param>
        /// <param name="value">The value to be compared to the provided property.</param>
        /// <param name="operator">The operator to define the comparison type.</param>
        /// <returns>The expression containing the conditional predicate.</returns>
        public static Expression<Func<T, bool>> CreateBinaryExpression<T>(string propertyName, object value, ExpressionOperator @operator)
        {
            return BuildBinaryExpression<T>(propertyName, value, @operator);
        }

        /// <summary>
        /// Creates a binary lambda expression that compares the value of an property from an object of
        /// type <typeparamref name="T"/> with the provided value using the specified comparison operator.
        /// </summary>
        /// <typeparam name="T">The type that contains the property to be compared.</typeparam>
        /// <param name="propertyInfo">The property info that provides access to the property attributes and metadata.</param>
        /// <param name="value">The value to be compared to the provided property.</param>
        /// <param name="operator">The operator to define the comparison type.</param>
        /// <returns>The built <see cref="Expression{T}"/> instance.</returns>
        public static Expression<Func<T, bool>> CreateBinaryExpression<T>(PropertyInfo propertyInfo, object value, ExpressionOperator @operator)
        {
            return BuildBinaryExpression<T>(propertyInfo.Name, value, @operator);
        }

        /// <summary>
        /// Builds a binary lambda expression that compares the value of an property of
        /// type <typeparamref name="T"/> with the provided value using the specified comparison operator.
        /// </summary>
        /// <typeparam name="T">The type that contains the property to be compared.</typeparam>
        /// <param name="propertyName">The property of the type the expression will evaluate to be accessed.</param>
        /// <param name="value">The value of the property.</param>
        /// <param name="operator">The comparison operator.</param>
        /// <returns>The built <see cref="Expression{TDelegate}"/> instance.</returns>
        private static Expression<Func<T, bool>> BuildBinaryExpression<T>(string propertyName, object value, ExpressionOperator @operator)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);

            BuildBinaryExpressionBody(property, value, @operator, out var leftExpression, out var rightExpression);

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
                case ExpressionOperator.Contains when property.Type == typeof(string):
                    body = BuildStringContainsMethodExpression(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.Contains when property.Type.IsGenericCollection():
                    body = BuildGenericCollectionContainsMethodExpression(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.Contains when property.Type.IsNonGenericList():
                    body = BuildNonGenericCollectionContainsMethodExpression(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.StartsWith:
                    body = BuildStringStartsWithMethodExpression(leftExpression, rightExpression);
                    break;
                case ExpressionOperator.EndsWith:
                    body = BuildStringEndsWithMethodExpression(leftExpression, rightExpression);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
            }

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        /// <summary>
        /// Builds the left and right binary <see cref="Expression"/> bodies that compares the value from the
        /// provided property accessor with the provided value using the specified comparison operator.
        /// </summary>
        /// <param name="property">The expression representing the access to the property to be compared.</param>
        /// <param name="value">The value to compare the property.</param>
        /// <param name="operator">The comparison operator.</param>
        /// <param name="leftExpression">The built left <see cref="Expression"/>.</param>
        /// <param name="rightExpression">The built right <see cref="Expression"/>.</param>
        private static void BuildBinaryExpressionBody(Expression property, object value, ExpressionOperator @operator, out Expression leftExpression, out Expression rightExpression)
        {
            leftExpression = null;
            rightExpression = null;

            if (property.Type == typeof(string))
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
                        throw new ArgumentException($"Operator {@operator} isn't valid for the type {nameof(String)}.", nameof(@operator));
                    default:
                        throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
                }

            if (property.Type.IsNumeric())
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
                        throw new ArgumentException($"Operator {@operator} isn't valid for the type {property.Type.Name}.", nameof(@operator));
                    default:
                        throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
                }

                if (!value.GetType().IsNumeric())
                {
                    var parameters = new[]
                    {
                        value
                    };

                    var argumentTypes = new[]
                    {
                        value.GetType()
                    };

                    // ToDo - Use TryParse
                    rightExpression = Expression.Constant(property.Type.GetMethod(nameof(int.Parse), argumentTypes)?.Invoke(property, parameters));
                }
            }
            else
                switch (@operator)
                {
                    case ExpressionOperator.Equal:
                    case ExpressionOperator.NotEqual:
                        break;
                    case ExpressionOperator.Contains when property.Type.IsGenericCollection():
                        rightExpression = Expression.Constant(value, property.Type.GetGenericArguments()[0]);
                        break;
                    case ExpressionOperator.Contains when property.Type.IsNonGenericList():
                        rightExpression = Expression.Constant(value, typeof(object));
                        break;
                    case ExpressionOperator.StartsWith:
                    case ExpressionOperator.EndsWith:
                    case ExpressionOperator.LessThan:
                    case ExpressionOperator.LessThanOrEqual:
                    case ExpressionOperator.GreaterThan:
                    case ExpressionOperator.GreaterThanOrEqual:
                        throw new ArgumentException($"Operator {@operator} isn't valid for the type {property.Type.Name}.", nameof(@operator));
                    default:
                        throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
                }

            leftExpression = leftExpression ?? property;
            rightExpression = rightExpression ?? Expression.Constant(value);
        }

        /// <summary>
        /// Builds an method call expression that represents a call to the <see cref="string.Contains(string)"/> method.
        /// </summary>
        /// <param name="property">The expression representing the instance for the instance method call.</param>
        /// <param name="value">The expression representing the value to be passed as the method argument.</param>
        /// <returns>The call to the method.</returns>
        private static MethodCallExpression BuildStringContainsMethodExpression(Expression property, Expression value)
        {
            var argumentTypes = new[]
            {
                typeof(string)
            };

            return Expression.Call(property, typeof(string).GetMethod(nameof(string.Contains), argumentTypes) ?? throw new InvalidOperationException(), value);
        }

        /// <summary>
        /// Builds an method call expression that represents a call to the <see cref="string.StartsWith(string)"/> method.
        /// </summary>
        /// <param name="property">The expression representing the instance for the instance method call.</param>
        /// <param name="value">The expression representing the value to be passed as the method argument.</param>
        /// <returns>The expression representing the call to the method.</returns>
        private static MethodCallExpression BuildStringStartsWithMethodExpression(Expression property, Expression value)
        {
            var argumentTypes = new[]
            {
                typeof(string)
            };

            return Expression.Call(property, typeof(string).GetMethod(nameof(string.StartsWith), argumentTypes) ?? throw new InvalidOperationException(), value);
        }

        /// <summary>
        /// Builds an method call expression that represents a call to the <see cref="string.EndsWith(string)"/> method.
        /// </summary>
        /// <param name="property">The expression representing the instance for the instance method call.</param>
        /// <param name="value">The expression representing the value to be passed as the method argument.</param>
        /// <returns>The expression representing the call to the method.</returns>
        private static MethodCallExpression BuildStringEndsWithMethodExpression(Expression property, Expression value)
        {
            var argumentTypes = new[]
            {
                typeof(string)
            };

            return Expression.Call(property, typeof(string).GetMethod(nameof(string.EndsWith), argumentTypes) ?? throw new InvalidOperationException(), value);
        }

        /// <summary>
        /// Builds an method call expression that represents a call to the <see cref="ICollection{T}.Contains(T)"/> method.
        /// </summary>
        /// <param name="property">The expression representing the instance for the instance method call.</param>
        /// <param name="value">The expression representing the value to be passed as the method argument.</param>
        /// <returns>The expression representing the call to the method.</returns>
        private static MethodCallExpression BuildGenericCollectionContainsMethodExpression(Expression property, Expression value)
        {
            var argumentTypes = property.Type.GetGenericArguments();

            return Expression.Call(property,
                typeof(ICollection<>).MakeGenericType(argumentTypes).GetMethod(nameof(ICollection<object>.Contains), argumentTypes) ?? throw new InvalidOperationException(),
                value);
        }

        /// <summary>
        /// Builds an method call expression that represents a call to the <see cref="IList.Contains"/> method.
        /// </summary>
        /// <param name="property">The expression representing the instance for the instance method call.</param>
        /// <param name="value">The expression representing the value to be passed as the method argument.</param>
        /// <returns>The expression representing the call to the method.</returns>
        private static MethodCallExpression BuildNonGenericCollectionContainsMethodExpression(Expression property, Expression value)
        {
            var argumentTypes = new[]
            {
                typeof(object)
            };

            return Expression.Call(property, typeof(IList).GetMethod(nameof(IList.Contains), argumentTypes) ?? throw new InvalidOperationException(), value);
        }
    }
}
