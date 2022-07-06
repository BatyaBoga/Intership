// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Intership
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;
    using Intership.Figures;

    /// <summary>
    /// Main class which work with view.
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<Figure> figures;

        private Timer aTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            figures = new List<Figure>();
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
            this.AddToFigures(new RectangleFig(this.Canva), this.TreeViewItemRectangle, "Rectangle");
        }

        private void CircleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.AddToFigures(new Circle(this.Canva), this.TreeViewItemCircle, "Circle");
        }

        private void TriangleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.AddToFigures(new Triangle(this.Canva), this.TreeViewItemTriangle, "Triangle");
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedItemIsFigure())
            {
                if (IsSelectedItemInList())
                {
                    StopSelectedFigure();
                    this.StartBtn.Content = "Start";
                }
                else
                {
                    StartSelectedFigure();
                    this.StartBtn.Content = "Stop";
                }
            }
        }

        private void TV_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectedItemIsFigure())
            {
                if (IsSelectedItemInList())
                {
                    this.StartBtn.Content = "Stop";
                }
                else
                {
                    this.StartBtn.Content = "Start";
                }
            }
        }

        private void StopSelectedFigure()
        {
            figures.Remove((Figure)((TreeViewItem)this.TreeViewFigures.SelectedItem).Tag);
        }

        private void StartSelectedFigure()
        {
            figures.Add((Figure)((TreeViewItem)this.TreeViewFigures.SelectedItem).Tag);
        }

        private void AddToFigures(object figure, TreeViewItem tv, string header)
        {
            if (figure is Figure)
            {
                figures.Add((Figure)figure);
                var TvItem = new TreeViewItem();
                TvItem.Header = $"{header} {tv.Items.Count + 1}";
                TvItem.Tag = figure;
                tv.Items.Add(TvItem);
            }
            else
            {
                throw new Exception("Object not a Figure");
            }
        }

        private bool SelectedItemIsFigure()
        {
            return ((TreeViewItem)this.TreeViewFigures.SelectedItem).Tag is Figure;
        }

        private bool IsSelectedItemInList()
        {
            return figures.Contains((Figure)((TreeViewItem)this.TreeViewFigures.SelectedItem).Tag);
        }
    }
}
