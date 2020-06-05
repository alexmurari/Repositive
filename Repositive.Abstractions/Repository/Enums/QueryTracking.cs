namespace Repositive.Abstractions
{
    /// <summary>
    ///     Behaviors for the query change tracker.
    /// </summary>
    public enum QueryTracking
    {
        /// <summary>
        ///     The default query tracking behavior defined in the database context.
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