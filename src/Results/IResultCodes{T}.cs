using System.Diagnostics.CodeAnalysis;

namespace Results
{
    /// <summary>
    /// The result.
    /// </summary>
    /// <typeparam name="TCode">The type of the result code.</typeparam>
    public interface IResultCodes<TCode> : IResultCodes<TCode, ResultVoidType>
    {
    }

    /// <summary>
    /// The result.
    /// </summary>
    /// <typeparam name="TCode">The type of the result code.</typeparam>
    /// <typeparam name="TValue">The type of <c>Value</c> obtained as a result.</typeparam>
    public interface IResultCodes<TCode, TValue>
    {
        /// <summary>
        /// Gets a value indicating whether or not the result is success.
        /// </summary>
        /// <value>The opposite value of <c>IsFailed</c>.</value>
        bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether or not the result is success.
        /// </summary>
        /// <value>The opposite value of <c>IsSuccess</c>.</value>
        bool IsFailed { get; }

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

        /// <summary>
        /// Gets a collection of the result code.
        /// </summary>
        [NotNull]
        IEnumerable<TCode> Codes { get; }

        /// <summary>
        /// Gets the additional data.
        /// </summary>
        [NotNull]
        IReadOnlyDictionary<TCode, object> AdditionalData { get; }
        /// <summary>
        /// Gets a value obtained as an action result.
        /// </summary>
        TValue Value { get; }
    }
}
