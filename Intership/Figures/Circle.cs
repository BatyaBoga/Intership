using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Intership.Figures
{
    public class Circle : Figure
    {
        public Circle(Canvas Canva) : base(Canva) { }
        public override void Draw()
        {
            Ellipse ellipse = new();
            Width = ellipse.Width = 100;
            Height = ellipse.Height = 100;
            ellipse.Stroke = new SolidColorBrush(Colors.Black);
            ellipse.StrokeThickness = 2;
            FigureNum = canvas.Children.Add(ellipse);
            Move();
        }
    }
}
