namespace Intership.Figures
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Intership.Functions;
    using Point = System.Windows.Point;
    using Random = Random.Range.Random;

    /// <summary>
    /// Abstract class of Figures.
    /// </summary>
    public abstract class Figure
    {
        /// <summary>
        /// Figure position.
        /// </summary>
        protected Point pos;

        private Point movePos;

        private IntersectionManager intersectionManager;

        /// <summary>
        /// Number of figure in canvas element.
        /// </summary>
        protected int figureNum;

        /// <summary>
        /// Width of shape.
        /// </summary>
        protected double width;

        /// <summary>
        /// Height of shape.
        /// </summary>
        protected double height;

        /// <summary>
        /// Canvas for display.
        /// </summary>
        protected Canvas canvas;

        /// <summary>
        /// Initializes a new instance of the <see cref="Figure"/> class.
        /// </summary>
        /// <param name="canvas">Canvas for display.</param>
        public Figure(Canvas canvas)
        {
            this.canvas = canvas;
            this.pos = this.MidleOfCanvas;
            this.movePos = new Point(Random.RandomFromRangeWithExceptions(-4, 8, 0), Random.RandomFromRangeWithExceptions(-4, 8, 0));
            this.intersectionManager = new IntersectionManager();
            this.Draw();
            this.MoveToMiddleOfCanvas();
        }

        private Point MidleOfCanvas
        {
            get
            {
                return new Point((int)((this.canvas.ActualWidth / 2) - 100), ((int)this.canvas.ActualHeight / 2) - 50);
            }
        }

        /// <summary>
        /// This Method draw selected shape.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Moving the shape to selected position.
        /// </summary>
        public virtual void Move()
        {
            if (this.pos.X <= 1 || this.pos.X + this.width >= this.canvas.ActualWidth - 1)
            {
                this.movePos.X = -this.movePos.X;
            }

            if (this.pos.Y <= 1 || this.pos.Y + this.height >= this.canvas.ActualHeight - 1)
            {
                this.movePos.Y = -this.movePos.Y;
            }

            this.pos = new Point(this.pos.X + this.movePos.X, this.pos.Y + this.movePos.Y);

            if (this.IsValidPlace())
            {
                this.canvas.Children[this.figureNum].RenderTransform = new TranslateTransform(this.pos.X, this.pos.Y);
            }
            else
            {
                throw new Exception<OutOfBoundaryArgs>(new OutOfBoundaryArgs(this.pos));
            }
        }

        /// <summary>
        /// Return figure to middle of canvas.
        /// </summary>
        public void MoveToMiddleOfCanvas()
        {
            this.pos = this.MidleOfCanvas;
            this.canvas.Children[this.figureNum].RenderTransform = new TranslateTransform(this.pos.X, this.pos.Y);
        }

        /// <summary>
        /// Add handler to Intersection event.
        /// </summary>
        public void AddHandler()
        {
            this.intersectionManager.Intersection += this.NewIntersection;
        }

        /// <summary>
        /// Remove handler to Intersection event.
        /// </summary>
        public void RemoveHandler()
        {
            this.intersectionManager.Intersection -= this.NewIntersection;
        }

        /// <summary>
        /// Simulate Intersection event.
        /// </summary>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        public void Simulate(double x, double y)
        {
            this.intersectionManager.SimulateIntersection(new Point(x, y));
        }

        /// <summary>
        /// Intersection check.
        /// </summary>
        /// <param name="figure">Checked figure.</param>
        public abstract void IsIntersection(Figure figure);

        /// <summary>
        /// Event reaction.
        /// </summary>
        /// <param name="sender"> Sender.</param>
        /// <param name="e"> EvenetArgs.</param>
        protected virtual void NewIntersection(object sender, IntersectionEventArgs e)
        {
            ConsoleManager.ShowConsoleWindow();
            Console.Beep();
            Console.WriteLine(e.ToString());
        }

        /// <summary>
        /// Basic verification algorithm.
        /// </summary>
        /// <typeparam name="T">Figure type.</typeparam>
        /// <param name="figure">Figure.</param>
        protected virtual void IntersectionCheck<T>(Figure figure)
            where T : Figure
        {
            T other = figure as T;

            if (other != null)
            {
                double x = this.AxisCheck(figure.pos.X, figure.width, this.pos.X, this.width);
                double y = this.AxisCheck(figure.pos.Y, figure.height, this.pos.Y, this.height);

                if (x > 0 && y > 0)
                {
                    this.Simulate(x, y);
                }
            }

            return;
        }

        private double AxisCheck(double axis, double size, double thisAxis, double thisSize)
        {
            if (axis < thisAxis && axis + size > thisAxis)
            {
                return axis + size;
            }
            else if (axis < thisAxis + thisSize && axis + size > thisAxis + thisSize)
            {
                return axis;
            }
            else
            {
                return -1;
            }
        }

        private bool IsValidPlace()
        {
            if (this.pos.X + this.width >= this.canvas.ActualWidth ||
                this.pos.Y + this.height >= this.canvas.ActualHeight)
            {
                return false;
            }

            return true;
        }
    }
}
