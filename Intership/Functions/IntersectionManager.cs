namespace Intership.Figures
{
    using System;
    using Point = System.Windows.Point;

    /// <summary>
    /// Event manager.
    /// </summary>
    internal class IntersectionManager
    {
        /// <summary>
        /// Shape intersection event.
        /// </summary>
        public event EventHandler<IntersectionEventArgs> Intersection;

        /// <summary>
        /// Method generate IntersectionEventArgs.
        /// </summary>
        /// <param name="intersectionPoint"> Point of intersection.</param>
        public void SimulateIntersection(Point intersectionPoint)
        {
            IntersectionEventArgs e = new IntersectionEventArgs(intersectionPoint);

            this.OnIntersection(e);
        }

        /// <summary>
        /// Method invoke intersection event.
        /// </summary>
        /// <param name="e"> EventArgs.</param>
        protected virtual void OnIntersection(IntersectionEventArgs e)
        {
            var temp = this.Intersection;

            temp?.Invoke(this, e);
        }
    }
}
