using System.Diagnostics.CodeAnalysis;

using Results.Loggings;

namespace Results
{
    /// <summary>
    /// The basic implementation of the <see cref="IResult{T}"/> class.
    /// </summary>
    public abstract class ResultBase<T> : IResult<T>
    {
        /// <inheritdoc />
        public bool IsSuccess { get; protected set; }

        /// <inheritdoc />
        public bool IsFailed => !IsSuccess;

        /// <inheritdoc />
        [NotNull]
        public IEnumerable<IErrorLog> Errors => AllLogs.OfType<IErrorLog>();

        /// <inheritdoc />
        [NotNull]
        public IEnumerable<ILog> Warnings => AllLogs.Where(log => log.Severity == Severity.Warning);

        /// <inheritdoc />
        [NotNull]
        public IEnumerable<ILog> Informations => AllLogs.Where(log => log.Severity == Severity.Information);

        /// <inheritdoc />
        [NotNull]
        public abstract IEnumerable<ILog> AllLogs { get; }

        /// <inheritdoc />
        public abstract T Value { get; }
    }
}
