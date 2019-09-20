namespace KraftCore.Utils.Expressions
{
    /// <summary>
    /// Expression comparison operators.
    /// </summary>
    public enum ExpressionOperator
    {
        /// <summary>
        /// The equality comparison.
        /// </summary>
        Equal,

        /// <summary>
        /// The inequality comparison.
        /// </summary>
        NotEqual,

        /// <summary>
        /// The "less than" numeric comparison.
        /// </summary>
        LessThan,

        /// <summary>
        /// The "less than or equal" numeric comparison.
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// The "greater than" numeric comparison.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// The "greater than or equal" numeric comparison.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// The "contains" comparison.
        /// </summary>
        Contains,

        /// <summary>
        /// The "starts with" comparison.
        /// </summary>
        StartsWith,

        /// <summary>
        /// The "ends with" comparison.
        /// </summary>
        EndsWith
    }
}