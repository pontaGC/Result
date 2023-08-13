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
        public bool HasException => Exception != null;

        /// <inheritdoc />
        [MaybeNull]
        public Exception Exception { get; protected set; }

        /// <inheritdoc />
        [NotNull]
        public IEnumerable<TCode> Codes => Resultcodes;

        /// <inheritdoc />
        [NotNull]
        public IReadOnlyDictionary<TCode, object> AdditionalData => EditableAdditionalData;

        /// <summary>
        /// Gets a substance of the <c>Codes</c>.
        /// </summary>
        [NotNull]
        protected List<TCode> Resultcodes { get; } = new List<TCode>();

        /// <summary>
        /// Gets a substance of the <c>AdditionalData</c>.
        /// </summary>
        [NotNull]
        protected Dictionary<TCode, object> EditableAdditionalData { get; } = new Dictionary<TCode, object>();

        /// <summary>
        /// Gets a void value.
        /// </summary>
        public ResultVoidType Value => ResultVoidType.Instance;

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
        public ResultCodes<TCode> AddAdditionalData(TCode key, object value)
        {
            if (key is null)
            {
                return this;
            }

            if (EditableAdditionalData.ContainsKey(key))
            {
                EditableAdditionalData[key] = value;
            }
            else
            {
                EditableAdditionalData.Add(key, value);
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
    public class ResultCodes<TCode, TValue> : IResultCodes<TCode, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultCodes{TCode, TValue}"/> class.
        /// </summary>
        /// <param name="isSuccess">The value indicating the result is successful.</param>
        /// <param name="value">The value obtained as a result.</param>
        protected ResultCodes(bool isSuccess, TValue value) 
        {
            this.IsSuccess = isSuccess;
            this.Value = value;
        }

        #region Properties

        /// <inheritdoc />
        public bool IsSuccess { get; }

        /// <inheritdoc />
        public bool IsFailed => !this.IsSuccess;

        /// <inheritdoc />
        public bool HasException => Exception != null;

        /// <inheritdoc />
        [MaybeNull]
        public Exception Exception { get; protected set; }

        /// <inheritdoc />
        [NotNull]
        public IEnumerable<TCode> Codes => Resultcodes;

        /// <inheritdoc />
        [NotNull]
        public IReadOnlyDictionary<TCode, object> AdditionalData => EditableAdditionalData;

        /// <summary>
        /// Gets a substance of the <c>Codes</c>.
        /// </summary>
        [NotNull]
        protected List<TCode> Resultcodes { get; } = new List<TCode>();

        /// <summary>
        /// Gets a substance of the <c>AdditionalData</c>.
        /// </summary>
        [NotNull]
        protected Dictionary<TCode, object> EditableAdditionalData { get; } = new Dictionary<TCode, object>();

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
        public ResultCodes<TCode, TValue> AddCodes(IEnumerable<TCode> codes)
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
        public ResultCodes<TCode, TValue> AddCodes(TCode code)
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
        public ResultCodes<TCode, TValue> WithException(Exception exception)
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
        public ResultCodes<TCode, TValue> AddAdditionalData(TCode key, object value)
        {
            if (key is null)
            {
                return this;
            }

            if (EditableAdditionalData.ContainsKey(key))
            {
                EditableAdditionalData[key] = value;
            }
            else
            {
                EditableAdditionalData.Add(key, value);
            }

            return this;
        }

        #endregion
    }

    #endregion
}
