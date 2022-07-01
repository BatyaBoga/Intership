using System.Windows.Controls;
using System.Windows.Media;
using Intership.Commands;
using Point = System.Windows.Point;

namespace Intership.Figures
{
    public abstract class Figure
    {
        private Point _pos;

        private Point _movePos;

        protected int FigureNum;

        protected double Width;

        protected double Height;

        protected Canvas canvas;


        public Figure(Canvas canvas)
        {
            this.canvas = canvas;
            _pos = new Point((int)(canvas.ActualWidth / 2 - 100), (int)canvas.ActualHeight / 2 - 50);
            _movePos = new Point(Random.RandomFromRangeWithExceptions(-4, 8, 0), Random.RandomFromRangeWithExceptions(-4, 8, 0));
            Draw();
        }
        public virtual void Move()
        {
            if (_pos.X <= 1 || _pos.X + Width >= canvas.ActualWidth - 1)
                _movePos.X = -_movePos.X;

            if (_pos.Y <= 1 || _pos.Y + Height >= canvas.ActualHeight - 1)
                _movePos.Y = -_movePos.Y;

            _pos = new Point(_pos.X + _movePos.X, _pos.Y + _movePos.Y);
           canvas.Children[FigureNum].RenderTransform = new TranslateTransform(_pos.X, _pos.Y);
           
        }
        public abstract void Draw();
    }
}
