using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Intership.Figures
{
    public abstract class Figure
    {

        public Point MaxPoint { get; set; }
        public System.Windows.Point Pos { get; set; }
        public System.Windows.Point MovePos { get; set; }

       

        protected Canvas canvas;
        public Figure(Canvas canvas)
        {
            this.canvas = canvas;
            Pos = new System.Windows.Point(canvas.ActualWidth / 2 - 100, canvas.ActualHeight / 2 - 50);
            MovePos = new System.Windows.Point(new Random().Next(-4,4), new Random().Next(-4,4));
            Draw();
        }
        public abstract void Move();
        public abstract void Draw();
    }
}
