namespace KraftCore.Tests.Projects.Shared.DynamicQueryBuilder.Base
{
    using System;
    using System.Collections.Generic;
    using KraftCore.Shared.Expressions;
    using KraftCore.Tests.Utilities;

    /// <summary>
    /// Base class for the dynamic query builder tests.
    /// </summary>
    public abstract class DynamicQueryBuilderTestsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicQueryBuilderTestsBase"/> class.
        /// </summary>
        protected DynamicQueryBuilderTestsBase()
        {
            Persons = Utilities.GetFakePersonCollection();
        }

        /// <summary>
        /// Gets the collection of fake persons to be used by the tests.
        /// </summary>
        protected List<Person> Persons { get; }

        /// <summary>
        /// Builds the <see cref="string"/> representing the query from the provided parameters.
        /// </summary>
        /// <param name="operator">
        /// The operator of query.
        /// </param>
        /// <param name="propertyName">
        /// The name property of the property to be compared by the query.
        /// </param>
        /// <param name="value">
        /// The value to compare.
        /// </param>
        /// <returns>
        /// The <see cref="string"/> representing the query.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Exception thrown when the operator value is out of the valid range of values.
        /// </exception>
        protected static string BuildQueryText(ExpressionOperator @operator, string propertyName, object value)
        {
            switch (@operator)
            {
                case ExpressionOperator.Equal:
                    return $"eq('{propertyName}', '{value}')";
                case ExpressionOperator.NotEqual:
                    return $"ne('{propertyName}', '{value}')";
                case ExpressionOperator.LessThan:
                    return $"lt('{propertyName}', '{value}')";
                case ExpressionOperator.LessThanOrEqual:
                    return $"lte('{propertyName}', '{value}')";
                case ExpressionOperator.GreaterThan:
                    return $"gt('{propertyName}', '{value}')";
                case ExpressionOperator.GreaterThanOrEqual:
                    return $"gte('{propertyName}', '{value}')";
                case ExpressionOperator.Contains:
                    return $"ct('{propertyName}', '{value}')";
                case ExpressionOperator.ContainsOnValue:
                    return $"cov('{propertyName}', [{value}])";
                case ExpressionOperator.StartsWith:
                    return $"sw('{propertyName}', '{value}')";
                case ExpressionOperator.EndsWith:
                    return $"ew('{propertyName}', '{value}')";
                default:
                    throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
            }
        }
    }
}
