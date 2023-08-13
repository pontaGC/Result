namespace Results.Loggings
{
    /// <summary>
    /// The severity.
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// The fatal error for the application.
        /// </summary>
        Fatal,

        /// <summary>
        /// The error that cannot continue processing.
        /// </summary>
        Alert,

        /// <summary>
        /// The error that can continue processing.
        /// </summary>
        Error,

        /// <summary>
        /// The warning.
        /// </summary>
        Warning,

        /// <summary>
        /// The information.
        /// </summary>
        Information,
    }
}
