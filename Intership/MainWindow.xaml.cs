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
        public static List<Figures.Figure> figures;
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
            
        }

        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {
            Figures.Rectangle r = new(Canva);
            figures.Add(r);
            aTimer.Start();
        }
        private  void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {

            
            foreach (var item in figures)
            {
                this.Dispatcher.Invoke(item.Move);
                ///this.Dispatcher.Invoke(Canva.Children);
            }
        }

    }
}
