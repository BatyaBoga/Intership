namespace Intership.Functions
{
    using System;
    using Point = System.Windows.Point;

    /// <summary>
    /// OutOfBoundaryException Arguments.
    /// </summary>
    [Serializable]
    public sealed class OutOfBoundaryArgs : ExceptionArgs
    {
        private readonly Point report;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutOfBoundaryArgs"/> class.
        /// </summary>
        /// <param name="report">The point that went beyond the border.</param>
        public OutOfBoundaryArgs(Point report)
        {
            this.report = report;
        }

        /// <summary>
        /// Gets point in string format.
        /// </summary>
        public string ReportMsg
        {
            get
            {
                return $"X:{this.report.X} Y:{this.report.Y}";
            }
        }

        /// <summary>
        /// Gets message of exception.
        /// </summary>
        public override string Message
        {
            get
            {
                return (this.ReportMsg == null) ? base.Message : $"The point of the figure by coordinates({this.ReportMsg}) went beyond the border of the canvas";
            }
        }
    }
}
