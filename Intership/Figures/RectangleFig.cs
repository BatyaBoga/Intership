using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Intership.Figures
{
    public class RectangleFig : Figure
    {
        public RectangleFig(Canvas Canva) : base(Canva) { }
        public override void Draw()
        {
            Rectangle rectangle = new();
            Width = rectangle.Width = 200;
            Height = rectangle.Height = 100;
            rectangle.Stroke = new SolidColorBrush(Colors.Black);
            rectangle.StrokeThickness = 2;
            FigureNum = canvas.Children.Add(rectangle);
            Move();
        }
    }
}
