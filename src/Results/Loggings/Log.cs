using System.Diagnostics.CodeAnalysis;

namespace Results.Loggings
{
    /// <summary>
    /// The result log.
    /// </summary>
    public class Log : ILog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="severity">The severity of a log.</param>
        /// <param name="message">The log message.</param>
        public Log(Severity severity, string message)
            : this (severity, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="severity">The severity of a log.</param>
        /// <param name="message">The log message.</param>
        /// <param name="addtionalData">The additional data.</param>
        public Log(Severity severity, string message, [AllowNull]object addtionalData)
        {
            this.Severity = severity;
            this.Message = message;
            this.AddtinalData = addtionalData;
        }

        /// <inheritdoc />
        public bool HasMessage => !string.IsNullOrEmpty(this.Message);

        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public Severity Severity { get; }

        /// <inheritdoc />
        [MaybeNull]
        public object AddtinalData { get; }
    }
}
