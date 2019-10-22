namespace KraftCore.Tests.Projects.Shared.DynamicQuery
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using KraftCore.Shared.Expressions;
    using KraftCore.Tests.Utilities;

    /// <summary>
    ///     Base class for expression builder unit tests.
    /// </summary>
    public abstract class DynamicQueryTestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicQueryTestBase"/> class.
        /// </summary>
        protected DynamicQueryTestBase()
        {
            HydraArmy = Utilities.GetFakeHydraCollection();
        }

        /// <summary>
        ///     Gets the hydra army.
        /// </summary>
        protected List<Hydra> HydraArmy { get; }

        /// <summary>
        ///     Builds the <see cref="string" /> representing the query from the provided parameters.
        /// </summary>
        /// <param name="propertyName">
        ///     The name property of the property to be compared by the query.
        /// </param>
        /// <param name="value">
        ///     The value to compare.
        /// </param>
        /// <param name="operator">
        ///     The operator of query.
        /// </param>
        /// <returns>
        ///     The <see cref="string" /> representing the query.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Exception thrown when the operator value is out of the valid range of values.
        /// </exception>
        protected static string BuildQueryText(string propertyName, object value, ExpressionOperator @operator)
        {
            if (!(value is string) && value is IEnumerable valueCollection)
            {
                var valueList = valueCollection.Cast<object>().ToList();

                IEnumerable<string> strCollection;

                if (valueList.OfType<System.DateTime?>().Any())
                {
                    strCollection = valueList.Cast<System.DateTime?>().Select(t => string.Concat("'", t?.ToString("O") ?? "$!NULL!$", "'"));
                }
                else if (valueList.OfType<float?>().Any())
                {
                    strCollection = valueList.Cast<float?>().Select(t => string.Concat("'", t?.ToString("R") ?? "$!NULL!$", "'"));
                }
                else
                {
                    strCollection = valueList.Select(t => string.Concat("'", t?.ToString() ?? "$!NULL!$", "'"));
                }

                value = string.Concat("[", string.Join(',', strCollection), "]");
            }
            else
            {
                if (value is System.DateTime dtValue)
                {
                    value = string.Concat("'", dtValue.ToString("O"), "'");
                }
                else if (value is float floatValue)
                {
                    value = string.Concat("'", floatValue.ToString("R"), "'");
                }
                else
                    value = string.Concat("'", value?.ToString() ?? "$!NULL!$", "'");
            }

            switch (@operator)
            {
                case ExpressionOperator.Equal:
                    return $"eq('{propertyName}', {value})";
                case ExpressionOperator.NotEqual:
                    return $"ne('{propertyName}', {value})";
                case ExpressionOperator.LessThan:
                    return $"lt('{propertyName}', {value})";
                case ExpressionOperator.LessThanOrEqual:
                    return $"lte('{propertyName}', {value})";
                case ExpressionOperator.GreaterThan:
                    return $"gt('{propertyName}', {value})";
                case ExpressionOperator.GreaterThanOrEqual:
                    return $"gte('{propertyName}', {value})";
                case ExpressionOperator.Contains:
                    return $"ct('{propertyName}', {value})";
                case ExpressionOperator.ContainsOnValue:
                    return $"cov('{propertyName}', {value})";
                case ExpressionOperator.StartsWith:
                    return $"sw('{propertyName}', {value})";
                case ExpressionOperator.EndsWith:
                    return $"ew('{propertyName}', {value})";
                default:
                    throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null);
            }
        }
    }
}
