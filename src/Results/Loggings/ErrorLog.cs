using System.Diagnostics.CodeAnalysis;

namespace Results.Loggings
{
    /// <summary>
    /// The error log.
    /// </summary>
    public class ErrorLog : Log, IErrorLog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLog"/> class.
        /// </summary>
        /// <param name="severity">The severity of a log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="exception">The exception that occurred.</param>
        public ErrorLog(Severity severity, string message, Exception exception = null) 
            : this (severity, message, null, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLog"/> class.
        /// </summary>
        /// <param name="severity">The severity of a log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="additionalData">The additional data.</param>
        /// <param name="exception">The exception that occurred.</param>
        public ErrorLog(Severity severity, string message, object additionalData, Exception exception = null)
            : base(severity, message, additionalData)
        {
            this.Exception = exception;
        }

        /// <inheritdoc />
        public bool HasException => this.Exception != null;

        /// <inheritdoc />
        [MaybeNull]
        public Exception Exception { get; }
    }
}
