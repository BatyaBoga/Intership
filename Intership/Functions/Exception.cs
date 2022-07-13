namespace Intership.Functions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Generic exception.
    /// </summary>
    /// <typeparam name="TExceptionArgs">Exception arguments.</typeparam>
    [Serializable]
    public sealed class Exception<TExceptionArgs> : Exception, ISerializable
        where TExceptionArgs : ExceptionArgs
    {
        private const string CArgs = "Args";

        private readonly TExceptionArgs mArgs;

        /// <summary>
        /// Initializes a new instance of the <see cref="Exception{TExceptionArgs}"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner Exception.</param>
        public Exception(string message = null, Exception innerException = null)
        : this(null, message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exception{TExceptionArgs}"/> class.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner Exception.</param>
        public Exception(TExceptionArgs args, string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            this.mArgs = args;
        }

        private Exception(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
            this.mArgs = (TExceptionArgs)info.GetValue(
            CArgs, typeof(TExceptionArgs));
        }

        /// <summary>
        /// Gets mArgs.
        /// </summary>
        public TExceptionArgs Args
        {
            get
            {
                return this.mArgs;
            }
        }

        /// <inheritdoc/>
        public override string Message
        {
            get
            {
                string baseMsg = base.Message;
                return (this.mArgs == null) ? baseMsg : baseMsg + " (" + this.mArgs.Message + ")";
            }
        }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(CArgs, this.mArgs);
            base.GetObjectData(info, context);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            Exception<TExceptionArgs> other = obj as Exception<TExceptionArgs>;

            if (obj == null)
            {
                return false;
            }

            return object.Equals(this.mArgs, other.mArgs) && base.Equals(obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
