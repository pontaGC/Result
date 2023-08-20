using System.Diagnostics.CodeAnalysis;

namespace Results
{
    #region ResultCode<TCode>

    /// <summary>
    /// The result codes.
    /// </summary>
    /// <typeparam name="TCode">The type of the result code.</typeparam>
    public class ResultCodes<TCode> : IResultCodes<TCode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultCodes{TCode}"/> class.
        /// </summary>
        /// <param name="isSuccess">The value indicating the result is successful.</param>
        protected ResultCodes(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }

        #region Properties

        /// <inheritdoc />
        public bool IsSuccess { get; }

        /// <inheritdoc />
        public bool IsFailed => !this.IsSuccess;

        /// <inheritdoc />
        public bool HasException => this.Exception != null;

        /// <inheritdoc />
        [MaybeNull]
        public Exception Exception { get; protected set; }

        /// <inheritdoc />
        [NotNull]
        public IEnumerable<TCode> Codes => this.Resultcodes;

        /// <inheritdoc />
        [NotNull]
        public IReadOnlyDictionary<string, object> AdditionalData => this.AdditionalDataInternal;

        /// <summary>
        /// Gets a substance of the <c>Codes</c>.
        /// </summary>
        [NotNull]
        protected List<TCode> Resultcodes { get; } = new List<TCode>();

        /// <summary>
        /// Gets a substance of the <c>AdditionalData</c>.
        /// </summary>
        [NotNull]
        protected Dictionary<string, object> AdditionalDataInternal { get; } = new Dictionary<string, object>();

        #endregion

        #region Create instance

        /// <summary>
        /// Creates an instance as a successful result.
        /// </summary>
        /// <returns>The new instance.</returns>
        public static ResultCodes<TCode> Succeeded()
        {
            return new ResultCodes<TCode>(true);
        }

        /// <summary>
        /// Creates an instance as a failure result.
        /// </summary>
        /// <returns>The new instance.</returns>
        public static ResultCodes<TCode> Failed()
        {
            return new ResultCodes<TCode>(false);
        }

        #endregion

        #region Add result codes

        /// <summary>
        /// Adds the result codes.
        /// </summary>
        /// <param name="codes">The result codes to add.</param>
        /// <returns>The self.</returns>
        public ResultCodes<TCode> AddCodes(IEnumerable<TCode> codes)
        {
            if (codes is null)
            {
                return this;
            }

            Resultcodes.AddRange(codes);
            return this;
        }

        /// <summary>
        /// Adds the result code.
        /// </summary>
        /// <param name="code">The result code to add.</param>
        /// <returns>The self.</returns>
        public ResultCodes<TCode> AddCodes(TCode code)
        {
            if (code is null)
            {
                return this;
            }

            Resultcodes.Add(code);
            return this;
        }

        /// <summary>
        /// Registers the exception that occurred.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The self.</returns>
        public ResultCodes<TCode> WithException(Exception exception)
        {
            Exception = exception;
            return this;
        }

        /// <summary>
        /// Adds an additinal data.
        /// </summary>
        /// <param name="key">The key to look up the additional data.</param>
        /// <param name="value">The additional data.</param>
        /// <returns>The self.</returns>
        public ResultCodes<TCode> AddAdditionalData(string key, object value)
        {
            if (key is null)
            {
                return this;
            }

            if (AdditionalDataInternal.ContainsKey(key))
            {
                AdditionalDataInternal[key] = value;
            }
            else
            {
                AdditionalDataInternal.Add(key, value);
            }

            return this;
        }

        #endregion
    }

    #endregion

    #region ResultCode<TCode, TValue>

    /// <summary>
    /// The result codes.
    /// </summary>
    /// <typeparam name="TCode">The type of the result code.</typeparam>
    public class ResultCodes<TCode, TValue> : ResultCodes<TCode>, IResultCodes<TCode, TValue>
    {
        // Inherits ResultCodes<TCode> to be able to cast Result<TCode, TValue> to Result<TCode>.

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultCodes{TCode, TValue}"/> class.
        /// </summary>
        /// <param name="isSuccess">The value indicating the result is successful.</param>
        /// <param name="value">The value obtained as a result.</param>
        protected ResultCodes(bool isSuccess, TValue value) 
            : base(isSuccess)
        {
            this.Value = value;
        }

        #region Properties

        /// <inheritdoc />
        public TValue Value { get; }

        #endregion

        #region Create instance

        /// <summary>
        /// Creates an instance as a successful result.
        /// </summary>
        /// <param name="value">The value obtained as result.</param>
        /// <returns>The new instance.</returns>
        public static ResultCodes<TCode, TValue> Succeeded(TValue value)
        {
            return new ResultCodes<TCode, TValue>(true, value);
        }

        /// <summary>
        /// Creates an instance as a failure result.
        /// </summary>
        /// <param name="value">The value. This is optinal.</param>
        /// <returns>The new instance.</returns>
        public static ResultCodes<TCode, TValue> Failed(TValue value = default)
        {
            return new ResultCodes<TCode, TValue>(false, value);
        }

        #endregion

        #region Add result codes

        /// <summary>
        /// Adds the result codes.
        /// </summary>
        /// <param name="codes">The result codes to add.</param>
        /// <returns>The self.</returns>
        public new ResultCodes<TCode, TValue> AddCodes(IEnumerable<TCode> codes)
        {
            var _ = base.AddCodes(codes);
            return this;
        }

        /// <summary>
        /// Adds the result code.
        /// </summary>
        /// <param name="code">The result code to add.</param>
        /// <returns>The self.</returns>
        public new ResultCodes<TCode, TValue> AddCodes(TCode code)
        {
            var _ = base.AddCodes(code);
            return this;
        }
        
        /// <summary>
        /// Registers the exception that occurred.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The self.</returns>
        public new ResultCodes<TCode, TValue> WithException(Exception exception)
        {
            var _ = base.WithException(exception);
            return this;
        }

        /// <summary>
        /// Adds an additinal data.
        /// </summary>
        /// <param name="key">The key to look up the additional data.</param>
        /// <param name="value">The additional data.</param>
        /// <returns>The self.</returns>
        public new ResultCodes<TCode, TValue> AddAdditionalData(string key, object value)
        {
            var _ = base.AddAdditionalData(key, value);
            return this;
        }

        #endregion
    }

    #endregion
}
