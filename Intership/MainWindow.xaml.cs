using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Intership.Figures;
using System.Threading;
using System.Windows.Threading;

namespace Intership
{
    public partial class MainWindow : Window
    {
        private static List<Figures.Figure> figures;
        private System.Timers.Timer aTimer;
        public MainWindow()
        {
            InitializeComponent();
            figures = new List<Figures.Figure>();
            SetTimer();
        }

        private void SetTimer()
        {
            aTimer = new System.Timers.Timer(100);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            aTimer.Start();
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
            RectangleFig rectangle = new(Canva);
            figures.Add(rectangle); 
            TVRectangle.Items.Add($"Rectangle №{TVRectangle.Items.Count +1}");
        }
     

        private void CircleBtn_Click(object sender, RoutedEventArgs e)
        {
            Circle circle = new(Canva);
            figures.Add(circle);
            TVCircle.Items.Add($"Circle №{TVCircle.Items.Count + 1}");
        }

        private void TriangleBtn_Click(object sender, RoutedEventArgs e)
        {
            Triangle triangle = new(Canva);
            figures.Add(triangle);
            TVTriangle.Items.Add($"Triangle №{TVTriangle.Items.Count + 1}");
        }
    }
}
