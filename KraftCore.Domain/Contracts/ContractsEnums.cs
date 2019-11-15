namespace KraftCore.Domain.Contracts
{
    /// <summary>
    ///     Directions which elements of a sequence can be sorted.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        ///     Sorts the elements in ascending order.
        /// </summary>
        Ascending,

        /// <summary>
        ///     Sorts the elements in descending order.
        /// </summary>
        Descending
    }

    /// <summary>
    ///     Behaviors for the query change tracker.
    /// </summary>
    public enum QueryTracking
    {
        /// <summary>
        ///     Utilizes the default query tracking behavior defined in the repository.
        /// </summary>
        Default,

        /// <summary>
        ///     Entities returned from the query are not tracked by the database context.
        /// </summary>
        NoTracking,

        /// <summary>
        ///     Entities returned from the query are tracked by the database context.
        /// </summary>
        TrackAll
    }
}