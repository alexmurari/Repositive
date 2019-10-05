namespace KraftCore.Shared.DynamicQuery
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using KraftCore.Shared.Expressions;
    using KraftCore.Shared.Extensions;

    /// <summary>
    ///     Provides static methods to dynamically build <see cref="Expression{TDelegate}" /> objects for data querying.
    /// </summary>
    public static class DynamicQueryBuilder
    {
        /// <summary>
        ///     The <see cref="Regex" /> that matches the operator that aggregate queries;
        /// </summary>
        private static readonly Regex QueryAggregatorRegex = new Regex("([\"+])(?:(?=(\\\\?))\\2.)*?\\1", RegexOptions.Compiled);

        /// <summary>
        ///     The <see cref="Regex" /> that matches the query operator;
        /// </summary>
        private static readonly Regex QueryOperatorRegex = new Regex("^[A-Za-z]{2,3}(?=\\()", RegexOptions.Compiled);

        /// <summary>
        ///     The <see cref="Regex" /> that matches the elements (property and values) of an query;
        /// </summary>
        private static readonly Regex QueryElementRegex = new Regex("([\"'])(?:(?=(\\\\?))\\2.)*?\\1", RegexOptions.Compiled);

        /// <summary>
        ///     The <see cref="Regex" /> that matches the property part from the query;
        /// </summary>
        private static readonly Regex QueryElementPropertyRegex = new Regex("(?<=[A-Za-z][\\(])([\"'])(?:(?=(\\\\?))\\2.)*?\\1", RegexOptions.Compiled);

        /// <summary>
        ///     The <see cref="Regex" /> that matches the value part from the query when it's an array;
        /// </summary>
        private static readonly Regex QueryElementValueArrayRegex = new Regex(@"(?:\[|\G(?!^))('[^']+')\s*,?\s*(?=[^\]]*\])", RegexOptions.Compiled);

        /// <summary>
        ///     Builds an <see cref="Expression{T}" /> from the provided <see cref="string" /> object representing an query.
        /// </summary>
        /// <typeparam name="T">
        ///     The type being queried.
        /// </typeparam>
        /// <param name="query">
        ///     The string representing the query.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression{T}" /> object representing the query.
        /// </returns>
        public static Expression<Func<T, bool>> Build<T>(string query)
        {
            return ParseQuery<T>(query.ThrowIfNullOrWhitespace(nameof(query)));
        }

        /// <summary>
        ///     Parses the provided query as <see cref="string" /> to it's <see cref="Expression{TDelegate}" /> equivalent.
        /// </summary>
        /// <param name="query">
        ///     The query.
        /// </param>
        /// <typeparam name="T">
        ///     The type being queried.
        /// </typeparam>
        /// <returns>
        ///     The <see cref="Expression{T}" /> object representing the query.
        /// </returns>
        private static Expression<Func<T, bool>> ParseQuery<T>(string query)
        {
            var operations = QueryAggregatorRegex.Split(query).Select(t => t.Trim('+')).Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
            var aggregates = QueryAggregatorRegex.Matches(query).Cast<Match>().Select(t => t.Value.Trim('+')).ToList();

            if (operations.Count - aggregates.Count != 1)
                throw new InvalidOperationException("Malformed query: the number of operations and aggregates isn't valid.");

            Expression<Func<T, bool>> expression = null;

            for (var i = 0; i < operations.Count; i++)
            {
                var operation = operations[i];

                var operationElements = QueryElementRegex.Matches(operation).Cast<Match>().Select(t => t.Value.Trim('\'')).ToList();
                var isArray = QueryElementValueArrayRegex.IsMatch(operation);

                if (!isArray && operationElements.Count > 2)
                    throw new InvalidOperationException("Malformed query: multiple elements are only allowed inside array declaration.");

                var @operator = GetExpressionOperator(QueryOperatorRegex.Match(operation).Value);
                var aggregate = i > 0 ? GetExpressionAggregate(aggregates[i - 1]) : null;

                var propertyName = QueryElementPropertyRegex.Match(operation).Value.Trim('\'');
                var values = isArray ? operationElements.Skip(1).ToArray() : (object)operationElements.Skip(1).FirstOrDefault();

                BuildExpression(propertyName, values, @operator.GetValueOrDefault(), aggregate.GetValueOrDefault());
            }

            void BuildExpression(string propertyName, object value, ExpressionOperator @operator, ExpressionAggregate aggregate)
            {
                if (expression == null)
                    expression = ExpressionBuilder.CreateBinaryExpression<T>(propertyName, value, @operator);
                else
                    switch (aggregate)
                    {
                        case ExpressionAggregate.And:
                            expression = expression.And(ExpressionBuilder.CreateBinaryExpression<T>(propertyName, value, @operator));
                            break;
                        case ExpressionAggregate.Or:
                            expression = expression.Or(ExpressionBuilder.CreateBinaryExpression<T>(propertyName, value, @operator));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(aggregate), aggregate, "Invalid aggregate operator.");
                    }
            }

            return expression;
        }

        /// <summary>
        ///     Gets the <see cref="ExpressionOperator" /> value identified by the provided <see cref="string" />.
        /// </summary>
        /// <remarks>
        ///     The <see cref="ExpressionOperator" /> is identified by the description set in the
        ///     <see cref="DescriptionAttribute" />.
        /// </remarks>
        /// <param name="operator">
        ///     The <see cref="string" /> representing the operator value.
        /// </param>
        /// <returns>
        ///     The <see cref="ExpressionOperator" /> identified by the provided <see cref="string" />.
        /// </returns>
        private static ExpressionOperator? GetExpressionOperator(string @operator)
        {
            return Enum.GetValues(typeof(ExpressionOperator))
                .Cast<ExpressionOperator?>()
                .FirstOrDefault(v => string.Equals(v.GetDescription(), @operator, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        ///     Gets the <see cref="ExpressionAggregate" /> value identified by the provided <see cref="string" />.
        /// </summary>
        /// <remarks>
        ///     The <see cref="ExpressionAggregate" /> is identified by the description set in the
        ///     <see cref="DescriptionAttribute" />.
        /// </remarks>
        /// <param name="operator">
        ///     The <see cref="string" /> representing the operator value.
        /// </param>
        /// <returns>
        ///     The <see cref="ExpressionAggregate" /> identified by the provided <see cref="string" />.
        /// </returns>
        private static ExpressionAggregate? GetExpressionAggregate(string @operator)
        {
            return Enum.GetValues(typeof(ExpressionAggregate))
                .Cast<ExpressionAggregate?>()
                .FirstOrDefault(v => string.Equals(v.GetDescription(), @operator, StringComparison.OrdinalIgnoreCase));
        }
    }
}