namespace Intership.Functions
{
    using System;

    /// <summary>
    /// Exception arguments.
    /// </summary>
    [Serializable]
    public abstract class ExceptionArgs
    {
        /// <summary>
        /// Gets exception message.
        /// </summary>
        public virtual string Message
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
