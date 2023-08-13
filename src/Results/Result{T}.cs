using System.Diagnostics.CodeAnalysis;

using Results.Loggings;

namespace Results
{
    /// <summary>
    /// The result.
    /// </summary>
    /// <typeparam name="T">The type of the <c>Value</c> obtained as an action result.</typeparam>
    public class Result<T> : ResultBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="isSuccess">The value indicating the result is successful.</param>
        protected Result(bool isSuccess, T value)
        {
            IsSuccess = isSuccess;
            Value = value;
        }

        #region Properties

        /// <inheritdoc />
        public override T Value { get; }

        /// <inheritdoc />
        [NotNull]
        public override IEnumerable<ILog> AllLogs => allLogs;

        /// <summary>
        /// Gets a substance of the <c>AllLogs</c>.
        /// </summary>
        [NotNull]
        protected List<ILog> allLogs { get; } = new List<ILog>();

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
        /// <param name="value">The value. This is optinal.</param>
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
        public Result<T> AddErrors(IEnumerable<IErrorLog> errors)
        {
            return AddLogs(errors);
        }

        /// <summary>
        /// Adds the error log.
        /// </summary>
        /// <param name="error">The error log to add.</param>
        /// <returns>The self.</returns>
        public Result<T> AddErrors(IErrorLog error)
        {
            return AddLogs(error);
        }

        /// <summary>
        /// Adds the error log.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="severity">The log severity.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="additionalData">The additonal data.</param>
        /// <returns>The self.</returns>
        public Result<T> AddErrors(
            string message,
            Severity severity = Severity.Error,
            Exception exception = null,
            object additionalData = null)
        {
            return AddErrors(new ErrorLog(severity, message, additionalData, exception));
        }

        /// <summary>
        /// Adds the logs.
        /// </summary>
        /// <param name="logs">The logs to add.</param>
        /// <returns>The self.</returns>
        public Result<T> AddLogs(IEnumerable<ILog> logs)
        {
            if (logs is null)
            {
                return this;
            }

            allLogs.AddRange(logs);
            return this;
        }

        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="log">The log to add.</param>
        /// <returns>The self.</returns>
        public Result<T> AddLogs(ILog log)
        {
            if (log is null)
            {
                return this;
            }

            allLogs.Add(log);
            return this;
        }

        #endregion
    }
}
