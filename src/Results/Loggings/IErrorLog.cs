using System.Diagnostics.CodeAnalysis;

namespace Results.Loggings
{
    /// <summary>
    /// The error log.
    /// </summary>
    public interface IErrorLog : ILog
    {
        /// <summary>
        /// Gets a value indicating whether or not an exception occurred.
        /// </summary>
        /// <value><c>true</c> if <c>Exception</c> is not <c>null</c>, otherwise, <c>false</c>.</value>
        bool HasException { get; }

        /// <summary>
        /// Gets an exception that occurred.
        /// </summary>
        [MaybeNull]
        Exception Exception { get; }
    }
}
