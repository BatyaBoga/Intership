using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Intership.Figures
{
    public class Rectangle : Figure
    {
        System.Windows.Shapes.Rectangle rectangle;
        public Rectangle(Canvas Canva) : base(Canva) 
        {
            rectangle = new();
        }
     
      
        public override void Draw()
        {
            rectangle.Stroke = new SolidColorBrush(Colors.Black);
            rectangle.Width = 200;
            rectangle.Height = 100;
            rectangle.StrokeThickness = 2;
            canvas.Children.Add(rectangle);
            canvas.Children[canvas.Children.Count-1].RenderTransform = new TranslateTransform(Pos.X, Pos.Y);
        }

        public override void Move()
        {
           Pos = new System.Windows.Point(Pos.X + MovePos.X, Pos.Y + MovePos.Y);
           
          
        }
    }
}
