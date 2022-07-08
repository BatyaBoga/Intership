namespace Intership.Figures
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using Random.Range;
    using Point = System.Windows.Point;

    /// <summary>
    /// Abstract class of Figures.
    /// </summary>
    public abstract class Figure
    {
        private Point pos;

        private Point movePos;

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
            this.pos = new Point((int)((this.canvas.ActualWidth / 2) - 100), ((int)this.canvas.ActualHeight / 2) - 50);
            this.movePos = new Point(Random.RandomFromRangeWithExceptions(-4, 8, 0), Random.RandomFromRangeWithExceptions(-4, 8, 0));
            this.Draw();
            this.Move();
        }

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
            this.canvas.Children[this.figureNum].RenderTransform = new TranslateTransform(this.pos.X, this.pos.Y);
        }

        /// <summary>
        /// This Method draw selected shape.
        /// </summary>
        public abstract void Draw();
    }
}
