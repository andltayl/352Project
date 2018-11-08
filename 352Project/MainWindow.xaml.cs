using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _352Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double gravity = 0.08;
        private double velocity = 0;
        private double leapDist = 5;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(gravityConstant);
            timer.Interval = TimeSpan.FromMilliseconds(0.5);
            timer.Start();

            this.KeyDown += new KeyEventHandler(OnSpaceDownHandler);
        }

        private void gravityConstant(object sender, EventArgs e)
        {
            velocity += gravity;
            llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top + velocity, llama.Margin.Right, llama.Margin.Bottom - velocity);
        }

        private void OnSpaceDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                velocity -= leapDist;
                //llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top - 50, llama.Margin.Right, llama.Margin.Bottom + 50);
            }
        }
    }
}
