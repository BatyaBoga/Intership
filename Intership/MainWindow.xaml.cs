// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Intership
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;
    using Intership.Figures;
    using Intership.Functions;
    using Intership.Localization;
    using Timer = System.Timers.Timer;

    /// <summary>
    /// Main class which work with view.
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static List<Figure> figures;

        private readonly LocalizationManager lm;

        private readonly Timer moveTimer;

        private readonly Timer intersectionTimer;

        private IEnumerable<CultureInfo> cultureInfos;

        private CultureInfo currentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            figures = new List<Figure>();
            this.lm = LocalizationManager.Instance;
            this.ComboBoxLanguage.ItemsSource = this.CultureInfos;
            this.moveTimer = this.SetTimer(100, this.OnMoveEvent);
            this.intersectionTimer = this.SetTimer(500, this.OnInterestionEvent);
        }

        /// <summary>
        /// Gets or sets a property which containing a collection of available cultures.
        /// </summary>
        public IEnumerable<CultureInfo> CultureInfos
        {
            get
            {
                return this.cultureInfos ?? (this.cultureInfos = LocalizationManager.Instance.Cultures);
            }

            set
            {
                if (Equals(value, this.cultureInfos))
                {
                    return;
                }

                this.cultureInfos = value;
            }
        }

        /// <summary>
        /// Gets or sets a property which containing selected culture.
        /// </summary>
        public CultureInfo CurrentCulture
        {
            get
            {
                return this.currentCulture ?? (this.currentCulture = LocalizationManager.Instance.CurrentCulture);
            }

            set
            {
                if (Equals(value, this.currentCulture))
                {
                    return;
                }

                this.currentCulture = value;
                LocalizationManager.Instance.CurrentCulture = value;
            }
        }

        private Timer SetTimer(int milisecond,  ElapsedEventHandler onTimeEvet)
        {
            var timer = new Timer(milisecond);
            timer.Elapsed += onTimeEvet;
            timer.Enabled = true;
            timer.Start();
            return timer;
        }

        private void OnMoveEvent(object source, ElapsedEventArgs e)
        {
            foreach (var item in figures)
            {
                try
                {
                    this.Dispatcher.Invoke(item.Move);
                }
                catch (Exception<OutOfBoundaryArgs> ex)
                {
                    this.Dispatcher.Invoke(item.MoveToMiddleOfCanvas);
                    Log.Error(ex.Message);
                }
            }
        }

        private void OnInterestionEvent(object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < figures.Count; i++)
            {
                for (int j = i + 1; j < figures.Count; j++)
                {
                    figures[i].IsIntersection(figures[j]);
                }
            }
        }

        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.AddToFigures(new RectangleFig(this.Canva), this.TreeViewItemRectangle);
        }

        private void CircleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.AddToFigures(new Circle(this.Canva), this.TreeViewItemCircle);
        }

        private void TriangleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.AddToFigures(new Triangle(this.Canva), this.TreeViewItemTriangle);
        }

        private void AddToFigures(object figure, TreeViewItem tv)
        {
            if (figure is Figure)
            {
                figures.Add((Figure)figure);
                var tvItem = new TreeViewItem();
                tvItem.Header = $"№{tv.Items.Count + 1}";
                tvItem.Tag = figure;
                tv.Items.Add(tvItem);
            }
            else
            {
                throw new Exception("Object not a Figure");
            }
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedItemIsFigure())
            {
                if (this.IsSelectedItemInList())
                {
                    this.StopSelectedFigure();
                    this.StartBtn.Content = this.lm.Localize("StartBtn");
                }
                else
                {
                    this.StartSelectedFigure();
                    this.StartBtn.Content = this.lm.Localize("StopBtn");
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

        private void TV_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.SelectedItemIsFigure())
            {
                if (this.IsSelectedItemInList())
                {
                    this.StartBtn.Content = this.lm.Localize("StopBtn");
                }
                else
                {
                    this.StartBtn.Content = this.lm.Localize("StartBtn");
                }
            }
        }

        private bool SelectedItemIsFigure()
        {
            var treeViewItem = this.TreeViewFigures.SelectedItem as TreeViewItem;

            if (treeViewItem != null)
            {
                return treeViewItem.Tag is Figure;
            }

            return false;
        }

        private bool IsSelectedItemInList()
        {
            return figures.Contains((Figure)((TreeViewItem)this.TreeViewFigures.SelectedItem).Tag);
        }

        private void ComboBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Equals(this.ComboBoxLanguage.SelectedItem, this.currentCulture))
            {
                return;
            }

            this.currentCulture = (CultureInfo)this.ComboBoxLanguage.SelectedItem;
            LocalizationManager.Instance.CurrentCulture = (CultureInfo)this.ComboBoxLanguage.SelectedItem;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedItemIsFigure())
            {
                ((Figure)((TreeViewItem)this.TreeViewFigures.SelectedItem).Tag).AddHandler();
            }
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedItemIsFigure())
            {
                ((Figure)((TreeViewItem)this.TreeViewFigures.SelectedItem).Tag).RemoveHandler();
            }
        }
    }
}
