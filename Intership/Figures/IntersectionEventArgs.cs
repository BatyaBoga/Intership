namespace Intership.Figures
{
    using System;
    using Point = System.Windows.Point;

    /// <summary>
    /// Arguments for event.
    /// </summary>
    public class IntersectionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionEventArgs"/> class.
        /// </summary>
        /// <param name="intersectionPoint"> Intersection point.</param>
        public IntersectionEventArgs(Point intersectionPoint)
        {
            this.IntersectionPoint = intersectionPoint;
        }

        /// <summary>
        /// Gets point where shape was intersection.
        /// </summary>
        public Point IntersectionPoint { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"X:{this.IntersectionPoint.X} Y:{this.IntersectionPoint.Y}";
        }
    }
}
