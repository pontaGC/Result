using System.Diagnostics.CodeAnalysis;

namespace Results.Loggings
{
    /// <summary>
    /// The base of result log.
    /// </summary>
    /// <typeparam name="T">The type of the severity.</typeparam>
    public interface ILogBase<T>
    {
        /// <summary>
        /// Gets a severity fo the log.
        /// </summary>
        T Severity { get; }

        /// <summary>
        /// Gets an additional data to know the log for detail.
        /// </summary>
        [MaybeNull]
        object AddtinalData { get; }
    }
}
