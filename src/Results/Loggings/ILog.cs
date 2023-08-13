namespace Results.Loggings
{
    /// <summary>
    /// The result log.
    /// </summary>
    public interface ILog : ILogBase<Severity>
    {
        /// <summary>
        /// Gets a value indicating whether or not a message is recorded.
        /// </summary>
        /// <value><c>true</c> if <c>Message</c> is not <c>null</c> or an empty string, otherwise, <c>false</c>.</value>
        bool HasMessage { get; }

        /// <summary>
        /// Gets a log message.
        /// </summary>
        string Message { get; }
    }
}
