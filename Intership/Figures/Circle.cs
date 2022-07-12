namespace Intership.Figures
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Сlass represents circle shape type.
    /// </summary>
    public class Circle : Figure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="canva"> Canvas for display.</param>
        public Circle(Canvas canva)
            : base(canva)
        {
        }

        /// <inheritdoc/>
        public override void Draw()
        {
            Ellipse ellipse = new ();
            this.width = ellipse.Width = 100;
            this.height = ellipse.Height = 100;
            ellipse.Stroke = new SolidColorBrush(Colors.Black);
            ellipse.StrokeThickness = 2;
            this.figureNum = this.canvas.Children.Add(ellipse);
        }

        /// <summary>
        /// Intersection check.
        /// </summary>
        /// <param name="figure">Figure.</param>
        public override void IsIntersection(Figure figure)
        {
            this.IntersectionCheck<Circle>(figure);
        }
    }
}
