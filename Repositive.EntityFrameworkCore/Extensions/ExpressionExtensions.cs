﻿namespace Repositive.EntityFrameworkCore.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    ///     Provides extension methods to the <see cref="Expression" /> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class ExpressionExtensions
    {
        /// <summary>
        ///     Converts the property accessor lambda expression to a textual representation of it's path. <br />
        ///     The textual representation consists of the properties that the expression access flattened and separated by a dot character (".").
        /// </summary>
        /// <param name="expression">The property selector expression.</param>
        /// <returns>The extracted textual representation of the expression's path.</returns>
        internal static string AsPath(this LambdaExpression expression)
        {
            if (expression == null)
                return null;

            TryParsePath(expression.Body, out var path);

            return path;
        }

        /// <summary>
        ///     Recursively parses an expression tree representing a property accessor to extract a textual representation of it's path. <br />
        ///     The textual representation consists of the properties accessed by the expression tree flattened and separated by a dot character (".").
        /// </summary>
        /// <param name="expression">The expression tree to parse.</param>
        /// <param name="path">The extracted textual representation of the expression's path.</param>
        /// <returns><see langword="true"/> if the parse operation succeeds; otherwise, <see langword="false"/>.</returns>
        private static bool TryParsePath(Expression expression, out string path)
        {
            var cleanExpression = RemoveConvertOperations(expression);
            path = null;

            switch (cleanExpression)
            {
                case MemberExpression memberExpression:
                {
                    var currentPart = memberExpression.Member.Name;

                    if (!TryParsePath(memberExpression.Expression, out var parentPart))
                        return false;

                    path = string.IsNullOrEmpty(parentPart) ? currentPart : ConcatPropertyToParent(parentPart, currentPart);

                    break;
                }

                case MethodCallExpression callExpression:
                    switch (callExpression.Method.Name)
                    {
                        case nameof(Queryable.Select) when callExpression.Arguments.Count == 2:
                        {
                            if (!TryParsePath(callExpression.Arguments[0], out var parentPart))
                                return false;

                            if (string.IsNullOrEmpty(parentPart))
                                return false;

                            if (!(callExpression.Arguments[1] is LambdaExpression subExpression))
                                return false;

                            if (!TryParsePath(subExpression.Body, out var currentPart))
                                return false;

                            if (string.IsNullOrEmpty(parentPart))
                                return false;

                            path = ConcatPropertyToParent(parentPart, currentPart);

                            return true;
                        }

                        case nameof(Queryable.Where):
                            throw new NotSupportedException("Filtering an Include expression is not supported");
                        case nameof(Queryable.OrderBy):
                        case nameof(Queryable.OrderByDescending):
                            throw new NotSupportedException("Ordering an Include expression is not supported");
                        default:
                            return false;
                    }
            }

            return true;

            string ConcatPropertyToParent(string parentPart, string currentPart)
            {
                return string.Concat(parentPart, ".", currentPart);
            }
        }

        /// <summary>
        ///     Removes all casts or conversion operations from the nodes of the provided <see cref="Expression" />.
        ///     Used to prevent type boxing when manipulating expression trees.
        /// </summary>
        /// <param name="expression">The expression to remove the conversion operations.</param>
        /// <returns>The expression without conversion or cast operations.</returns>
        private static Expression RemoveConvertOperations(Expression expression)
        {
            while (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked)
                expression = ((UnaryExpression)expression).Operand;

            return expression;
        }
    }
}