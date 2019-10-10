namespace KraftCore.Shared.DynamicQuery
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using KraftCore.Shared.Expressions;
    using KraftCore.Shared.Extensions;

    /// <summary>
    ///     Provides methods to parse the string representation of a query to a object representation.
    /// </summary>
    internal static class QueryParser
    {
        /// <summary>
        ///     Regular expression to match the operator that aggregate queries.
        /// </summary>
        private static readonly Regex QueryAggregatorRegex = new Regex("([\\+])(?:(?=(\\\\?))\\2.)[A-Za-z]{1,2}\\1", RegexOptions.Compiled);

        /// <summary>
        ///     Regular expression to match the operator, property and single value of a query.
        /// </summary>
        private static readonly Regex QuerySingleValueRegex =
            new Regex("^([A-Za-z]{2,3})(?=\\()|(?<=\\()([\"'])(?:(?=(\\\\?))\\3.)[a-zA-Z][a-zA-Z0-9_]+\\2|(?<!\\[)(?<=\\s*\\,\\s*)([\"'])(?:(?=(\\\\?))\\5.)+?\\4(?!\\])", RegexOptions.Compiled);

        /// <summary>
        ///     Regular expression to match the operator, property and an array of values of a query.
        /// </summary>
        private static readonly Regex QueryMultipleValueRegex =
            new Regex("^^([A-Za-z]{2,3})(?=\\()|(?<=\\()([\"'])(?:(?=(\\\\?))\\3.)[a-zA-Z][a-zA-Z0-9_]+\\2|(?:\\[|\\G(?!^))('[^']+?')\\s*,?\\s*(?=[^\\]]*?\\])", RegexOptions.Compiled);

        /// <summary>
        ///     The special characters.
        /// </summary>
        private static readonly List<Tuple<string, string, string>> SpecialCharacters = new List<Tuple<string, string, string>>
        {
            new Tuple<string, string, string>("'", @"\'", "<!&singleQuote&!>")
        };

        /// <summary>
        ///     The query keywords.
        /// </summary>
        private static readonly Dictionary<string, object> QueryKeywords = new Dictionary<string, object>
        {
            { "$!NULL!$", null }
        };

        /// <summary>
        ///     Parses the provided <see cref="string"/> representing an query to a collection of <see cref="QueryInfo"/> objects representing the elements of the query.
        /// </summary>
        /// <param name="query">
        ///     The <see cref="string"/> with the query to be parsed.
        /// </param>
        /// <returns>
        ///     The collection of query elements.
        /// </returns>
        internal static IEnumerable<QueryInfo> ParseQuery(string query)
        {
            var operations = QueryAggregatorRegex.Split(query).Select(t => t.Trim('+')).Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
            var aggregates = QueryAggregatorRegex.Matches(query).Cast<Match>().Select(t => GetExpressionAggregate(t.Value.Trim('+')) ?? throw new InvalidOperationException()).ToList();

            if (operations.Count - aggregates.Count != 1)
                throw new InvalidOperationException("Malformed query: invalid number of operations and aggregates.");

            for (var i = 0; i < operations.Count; i++)
            {
                var multipleValueOperation = QueryMultipleValueRegex.Matches(ReplaceEscapedCharacters(operations[i])).Cast<Match>().ToList();

                if (multipleValueOperation.Count >= 3)
                {
                    var operationElements = multipleValueOperation.Select(t => t.Value.Trim('\'')).ToList();
                    var valuesArray = multipleValueOperation.Select(t => t.Groups[4].Value.Trim('\'')).Where(t => !string.IsNullOrWhiteSpace(t)).Select(ReplacePlaceholders).ToArray();

                    yield return new QueryInfo(i > 0 ? (ExpressionAggregate?)aggregates[i - 1] : null,
                                               GetExpressionOperator(operationElements[0]) ?? throw new InvalidOperationException(),
                                               operationElements[1],
                                               ReplaceKeywords(valuesArray));

                    continue;
                }

                var singleValueOperation = QuerySingleValueRegex.Matches(operations[i]).Cast<Match>().ToList();

                if (singleValueOperation.Count == 3)
                {
                    var operationElements = singleValueOperation.Select(t => t.Value.Trim('\'')).ToList();

                    yield return new QueryInfo(i > 0 ? (ExpressionAggregate?)aggregates[i - 1] : null,
                                               GetExpressionOperator(operationElements[0]) ?? throw new InvalidOperationException(),
                                               operationElements[1],
                                               ReplaceKeywords(operationElements[2]));
                }
            }
        }

        /// <summary>
        ///     Replaces the escaped special characters in the provided <see cref="string" /> with placeholders.
        /// </summary>
        /// <param name="query">
        ///     The string containing the characters to be replaced.
        /// </param>
        /// <returns>
        ///     The string with the replaced characters.
        /// </returns>
        private static string ReplaceEscapedCharacters(string query)
        {
            var sb = new StringBuilder(query);

            foreach (var (_, escapedChar, placeholder) in SpecialCharacters)
                sb = sb.Replace(escapedChar, placeholder);

            return sb.ToString();
        }

        /// <summary>
        ///     Replaces the placeholders for special characters in the provided <see cref="string" /> with the actual special characters.
        /// </summary>
        /// <param name="query">
        ///     The string containing the placeholders to be replaced.
        /// </param>
        /// <returns>
        ///     The string with the replaced placeholders.
        /// </returns>
        private static string ReplacePlaceholders(string query)
        {
            var sb = new StringBuilder(query);

            foreach (var (originalChar, _, placeholder) in SpecialCharacters)
                sb = sb.Replace(placeholder, originalChar);

            return sb.ToString();
        }

        /// <summary>
        ///     Replaces the keywords in the query values to their actual values.
        /// </summary>
        /// <param name="values">
        ///     The query values to be replaced.
        /// </param>
        /// <returns>
        ///     The object containing the values with the replaced keywords.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Exception thrown when the provided value object is invalid.
        /// </exception>
        private static object ReplaceKeywords(object values)
        {
            switch (values)
            {
                case string[] stringArray:
                {
                    for (var i = 0; i < stringArray.Length; i++)
                    {
                        foreach (var queryKeyword in QueryKeywords.Where(queryKeyword => stringArray[i].IndexOf(queryKeyword.Key, StringComparison.OrdinalIgnoreCase) != -1))
                        {
                            stringArray.SetValue(queryKeyword.Value, i);
                            break;
                        }
                    }

                    return stringArray;
                }

                case string stringValue:
                {
                    foreach (var queryKeyword in QueryKeywords.Where(queryKeyword => stringValue.IndexOf(queryKeyword.Key, StringComparison.OrdinalIgnoreCase) != -1))
                    {
                        stringValue = (string)queryKeyword.Value;
                        break;
                    }

                    return stringValue;
                }

                default:
                    throw new InvalidOperationException();
            }
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