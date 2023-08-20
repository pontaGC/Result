using Results.Loggings;

namespace Results
{
    /// <summary>
    /// The result.
    /// </summary>
    /// <typeparam name="T">The type of the <c>Value</c> obtained as an action result.</typeparam>
    public class Result<T> : Result, IResult<T>
    {   
        // Inherits Result to be able to cast Result<T> to Result.

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="isSuccess">The value indicating the result is successful.</param>
        protected Result(bool isSuccess, T value)
            : base (isSuccess)
        {
            this.Value = value;
        }

        #region Properties

        /// <inheritdoc />
        public T Value { get; }

        #endregion

        #region Create instance

        /// <summary>
        /// Creates an instance as a successful result.
        /// </summary>
        /// <param name="value">The value obtained as result.</param>
        /// <returns>The new instance.</returns>
        public static Result<T> Succeeded(T value)
        {
            return new Result<T>(true, value);
        }

        /// <summary>
        /// Creates an instance as a failure result.
        /// </summary>
        /// <param name="value">The value. It is optinal.</param>
        /// <returns>The new instance.</returns>
        public static Result<T> Failed(T value = default)
        {
            return new Result<T>(false, value);
        }

        #endregion

        #region Add logs

        /// <summary>
        /// Adds the error logs.
        /// </summary>
        /// <param name="errors">The error logs to add.</param>
        /// <returns>The self.</returns>
        public new Result<T> AddErrors(IEnumerable<IErrorLog> errors)
        {
            var _ = base.AddErrors(errors);
            return this;
        }

        /// <summary>
        /// Adds the error log if both of the log message and exception are <c>null</c>.
        /// </summary>
        /// <param name="error">The error log to add.</param>
        /// <returns>The self.</returns>
        public new Result<T> AddErrors(IErrorLog error)
        {
            var _ = base.AddErrors(error);
            return this;
        }

        /// <summary>
        /// Adds the error log.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="severity">The log severity.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>The self.</returns>
        public new Result<T> AddErrors(
            string message,
            Severity severity = Severity.Error,
            Exception exception = null)
        {
            var _ = base.AddErrors(message, severity, exception);
            return this;
        }

        /// <summary>
        /// Adds the warning logs if the log message is not <c>null</c>.
        /// </summary>
        /// <param name="messages">The log messages to add.</param>
        /// <returns>The self.</returns>
        public new Result<T> AddWarnings(IEnumerable<string> messages)
        {
            var _ = base.AddWarnings(messages);
            return this;
        }

        /// <summary>
        /// Adds the warning log.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <returns>The self.</returns>
        public new Result<T> AddWarnings(string message)
        {
            var _ = base.AddWarnings(message);
            return this;
        }

        /// <summary>
        /// Adds the information log if the log message is not <c>null</c>.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <returns>The self.</returns>
        public new Result<T> AddInformations(string message)
        {
            var _ = base.AddInformations(message);
            return this;
        }

        /// <summary>
        /// Adds the information logs.
        /// </summary>
        /// <param name="messages">The log messages to add.</param>
        /// <returns>The self.</returns>
        public new Result<T> AddInformations(IEnumerable<string> messages)
        {
            var _ = base.AddInformations(messages);
            return this;
        }

        /// <summary>
        /// Adds the logs.
        /// </summary>
        /// <param name="logs">The logs to add.</param>
        /// <returns>The self.</returns>
        public new Result<T> AddLogs(IEnumerable<ILog> logs)
        {
            var _ = base.AddLogs(logs);
            return this;
        }

        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="log">The log to add.</param>
        /// <returns>The self.</returns>
        public new Result<T> AddLogs(ILog log)
        {
            var _ = base.AddLogs(log);
            return this;
        }

        #endregion
    }
}
