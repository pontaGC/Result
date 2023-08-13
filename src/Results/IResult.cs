using System.Diagnostics.CodeAnalysis;

using Results.Loggings;

namespace Results
{
    /// <summary>
    /// The result.
    /// </summary>
    public interface IResult : IResult<ILog, IErrorLog>
    {
    }

    /// <summary>
    /// The result.
    /// </summary>
    /// <typeparam name="TLog">The type of log.</typeparam>
    /// <typeparam name="TErrorLog">The type of error log.</typeparam>
    public interface IResult<TLog, TErrorLog>
    {
        /// <summary>
        /// Gets a value indicating whether or not the result is success.
        /// </summary>
        /// <value>The opposite value of <c>IsFailed</c>.</value>
        bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether or not the result is not success.
        /// </summary>
        /// <value>The opposite value of <c>IsSuccess</c>.</value>
        bool IsFailed { get; }

        /// <summary>
        /// Gets a collection of the error log.
        /// </summary>
        [NotNull]
        IEnumerable<TErrorLog> Errors { get; }

        /// <summary>
        /// Gets a collection of the warning log.
        /// </summary>
        [NotNull]
        IEnumerable<TLog> Warnings { get; }

        /// <summary>
        /// Gets a collection of the information log.
        /// </summary>
        [NotNull]
        IEnumerable<TLog> Informations { get; }

        /// <summary>
        /// Gets all collection of the log.
        /// </summary>
        [NotNull]
        IEnumerable<TLog> AllLogs { get; }
    }
}
