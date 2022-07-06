namespace Intership.Figures;

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

/// <summary>
/// Сlass represents rectangle shape type.
/// </summary>
public class RectangleFig : Figure
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RectangleFig"/> class.
    /// </summary>
    /// <param name="canva">Canvas for display.</param>
    public RectangleFig(Canvas canva)
        : base(canva)
    {
    }

    /// <inheritdoc/>
    public override void Draw()
    {
        Rectangle rectangle = new ();
        this.width = rectangle.Width = 200;
        this.height = rectangle.Height = 100;
        rectangle.Stroke = new SolidColorBrush(Colors.Black);
        rectangle.StrokeThickness = 2;
        this.figureNum = this.canvas.Children.Add(rectangle);
    }
}
