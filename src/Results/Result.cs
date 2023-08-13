using System.Diagnostics.CodeAnalysis;

using Results.Loggings;
namespace Results
{
    /// <summary>
    /// The result.
    /// </summary>
    public class Result : ResultBase<ResultVoidType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="isSuccess">The value indicating the result is successful.</param>
        protected Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        #region Properties

        /// <inheritdoc />
        [NotNull]
        public override IEnumerable<ILog> AllLogs => allLogs;

        /// <summary>
        /// Gets a void value.
        /// </summary>
        public override ResultVoidType Value => ResultVoidType.Instance;

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
        /// <returns>The new instance.</returns>
        public static Result Succeeded()
        {
            return new Result(true);
        }

        /// <summary>
        /// Creates an instance as a failure result.
        /// </summary>
        /// <returns>The new instance.</returns>
        public static Result Failed()
        {
            return new Result(false);
        }

        #endregion

        #region Add logs

        /// <summary>
        /// Adds the error logs.
        /// </summary>
        /// <param name="errors">The error logs to add.</param>
        /// <returns>The self.</returns>
        public Result AddErrors(IEnumerable<IErrorLog> errors)
        {
            return AddLogs(errors);
        }

        /// <summary>
        /// Adds the error log.
        /// </summary>
        /// <param name="error">The error log to add.</param>
        /// <returns>The self.</returns>
        public Result AddErrors(IErrorLog error)
        {
            return AddLogs(error);
        }

        /// <summary>
        /// Adds the error log.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="severity">The log severity.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>The self.</returns>
        public Result AddErrors(
            string message,
            Severity severity = Severity.Error,
            Exception exception = null)
        {
            return AddErrors(new ErrorLog(severity, message, exception));
        }

        /// <summary>
        /// Adds the warning logs.
        /// </summary>
        /// <param name="messages">The log messages to add.</param>
        /// <returns>The self.</returns>
        public Result AddWarnings(IEnumerable<string> messages)
        {
            if (messages is null)
            {
                return this;
            }

            foreach(var message in messages)
            {
                this.allLogs.Add(new Log(Severity.Warning, message));
            }

            return this;
        }

        /// <summary>
        /// Adds the warning log.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <returns>The self.</returns>
        public Result AddWarnings(string message)
        {
            return AddLogs(new Log(Severity.Warning, message));
        }

        /// <summary>
        /// Adds the information log.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <returns>The self.</returns>
        public Result AddInformations(string message)
        {
            return AddLogs(new Log(Severity.Information, message));
        }

        /// <summary>
        /// Adds the information logs.
        /// </summary>
        /// <param name="messages">The log messages to add.</param>
        /// <returns>The self.</returns>
        public Result AddInformations(IEnumerable<string> messages)
        {
            if (messages is null)
            {
                return this;
            }

            foreach (var message in messages)
            {
                this.allLogs.Add(new Log(Severity.Information, message));
            }

            return this;
        }

        /// <summary>
        /// Adds the logs.
        /// </summary>
        /// <param name="logs">The logs to add.</param>
        /// <returns>The self.</returns>
        public Result AddLogs(IEnumerable<ILog> logs)
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
        public Result AddLogs(ILog log)
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
