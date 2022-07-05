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
        //TreeViewItem item = new TreeViewItem();
        //TreeViewItem item = new TreeViewItem();

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
            this.AddToFigures(new RectangleFig(this.Canva), this.TVRectangle, "Rectangle");
        }

        private void CircleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.AddToFigures(new Circle(this.Canva), this.TVCircle, "Rectangle");
        }

        private void TriangleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.AddToFigures(new Triangle(this.Canva), this.TVTriangle, "Rectangle");
        }

        private void AddToFigures(object figure, TreeViewItem TV, string Header)
        {
            if (figure is Figure)
            {
                figures.Add((Figure)figure);
                var TvItem = new TreeViewItem();
                TvItem.Header = $"{Header} {TV.Items.Count + 1}";
                TvItem.Tag = figure;
                TV.Items.Add(TvItem);
            }
            else
            {
                throw new Exception("Object not a Figure");
            }
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {

            if (((TreeViewItem)this.TV.SelectedItem).Tag is Figure)
            {
                if (figures.Contains((Figure)((TreeViewItem)this.TV.SelectedItem).Tag))
                {
                    figures.Remove((Figure)((TreeViewItem)this.TV.SelectedItem).Tag);
                    this.StartBtn.Content = "Start";
                }
                else
                {
                    figures.Add((Figure)((TreeViewItem)this.TV.SelectedItem).Tag);
                    this.StartBtn.Content = "Stop";
                }
            }
        }

        private void TV_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (((TreeViewItem)this.TV.SelectedItem).Tag is Figure)
            {
                if (figures.Contains((Figure)((TreeViewItem)this.TV.SelectedItem).Tag))
                {
                    this.StartBtn.Content = "Stop";
                }
                else
                {
                    this.StartBtn.Content = "Start";
                }
            }
        }
    }
}
