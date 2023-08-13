namespace Results
{
    /// <summary>
    /// The result.
    /// </summary>
    /// <typeparam name="T">The type of <c>Value</c> obtained as a result.</typeparam>
    public interface IResult<T> : IResult
    {
        /// <summary>
        /// Gets a value obtained as a action result.
        /// </summary>
        T Value { get; }
    }
}
