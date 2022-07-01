using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Intership.Figures
{
    public class Triangle :Figure
    {
        public Triangle(Canvas Canva) : base(Canva) { }
        public override void Draw()
        {
            Polygon triangle = new();
            PointCollection points = new();
            points.Add(new Point(0,100));
            points.Add(new Point(50,0));
            points.Add(new Point(100,100));
            triangle.Points = points;
            triangle.Stroke = new SolidColorBrush(Colors.Black);
            Width = triangle.Width = 100;
            Height = triangle.Height = 100;
            triangle.StrokeThickness = 2;
            FigureNum = canvas.Children.Add(triangle);
            Move();
        }
    }
}
