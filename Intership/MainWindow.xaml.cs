// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Intership
{
    using System.Collections.Generic;
    using System.Timers;
    using System.Windows;
    using Intership.Figures;

    /// <summary>
    /// Main class which work with view.
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<Figures.Figure> figures;
        private System.Timers.Timer aTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            figures = new List<Figures.Figure>();
            this.SetTimer();
        }

        private void SetTimer()
        {
            this.aTimer = new Timer(100);
            this.aTimer.Elapsed += this.OnTimedEvent;
            this.aTimer.AutoReset = true;
            this.aTimer.Enabled = true;
            this.aTimer.Start();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            foreach (var item in figures)
            {
                this.Dispatcher.Invoke(item.Move);
            }
        }

        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            RectangleFig rectangle = new (this.Canva);
            figures.Add(rectangle);
            this.TVRectangle.Items.Add($"Rectangle №{this.TVRectangle.Items.Count + 1}");
        }

        private void CircleBtn_Click(object sender, RoutedEventArgs e)
        {
            Circle circle = new (this.Canva);
            figures.Add(circle);
            this.TVCircle.Items.Add($"Circle №{this.TVCircle.Items.Count + 1}");
        }

        private void TriangleBtn_Click(object sender, RoutedEventArgs e)
        {
            Triangle triangle = new (this.Canva);
            figures.Add(triangle);
            this.TVTriangle.Items.Add($"Triangle №{this.TVTriangle.Items.Count + 1}");
        }
    }
}
