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
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            //timer.Tick += new EventHandler(OnSpaceDownHandler);

            this.KeyDown += new KeyEventHandler(OnSpaceDownHandler);
        }

        private void OnSpaceDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Thickness currMargin = llama.Margin;
                currMargin.Top += 12;
                llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top - 12, llama.Margin.Right, llama.Margin.Bottom + 12);
            }
        }
    }
}
