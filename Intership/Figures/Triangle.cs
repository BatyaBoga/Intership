namespace Intership.Figures
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Сlass represents triangle shape type.
    /// </summary>
    public class Triangle : Figure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        /// <param name="canva"> Canvas for display.</param>
        public Triangle(Canvas canva)
            : base(canva)
        {
        }

        /// <inheritdoc/>
        public override void Draw()
        {
            Polygon triangle = new ();
            PointCollection points = new ();
            points.Add(new Point(0, 100));
            points.Add(new Point(50, 0));
            points.Add(new Point(100, 100));
            triangle.Points = points;
            triangle.Stroke = new SolidColorBrush(Colors.Black);
            this.width = triangle.Width = 100;
            this.height = triangle.Height = 100;
            triangle.StrokeThickness = 2;
            this.figureNum = this.canvas.Children.Add(triangle);
        }
    }
}
