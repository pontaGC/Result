using System.Diagnostics.CodeAnalysis;

using Results.Loggings;

namespace Results
{
    /// <summary>
    /// The basic implementation of the <see cref="IResult"/>.
    /// </summary>
    public abstract class ResultBase : IResult
    {
        /// <inheritdoc />
        public bool IsSuccess { get; protected set; }

        /// <inheritdoc />
        public bool IsFailed => !this.IsSuccess;

        /// <inheritdoc />
        [NotNull]
        public IEnumerable<IErrorLog> Errors => this.AllLogs.OfType<IErrorLog>();

        /// <inheritdoc />
        [NotNull]
        public IEnumerable<ILog> Warnings => this.AllLogs.Where(log => log.Severity == Severity.Warning);

        /// <inheritdoc />
        [NotNull]
        public IEnumerable<ILog> Informations => this.AllLogs.Where(log => log.Severity == Severity.Information);

        /// <inheritdoc />
        [NotNull]
        public virtual IEnumerable<ILog> AllLogs => this.allLogs;

        /// <summary>
        /// Gets a substance of the <c>AllLogs</c>.
        /// </summary>
        [NotNull]
        protected List<ILog> allLogs { get; } = new List<ILog>();
    }
}
