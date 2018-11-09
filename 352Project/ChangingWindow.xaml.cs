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
using System.Windows.Shapes;

namespace _352Project
{
    /// <summary>
    /// Interaction logic for ChangingWindow.xaml
    /// </summary>
    public partial class ChangingWindow : Window
    {
        int window = 1;
        public ChangingWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (window == 1)
            {
                this.contentControl.Content = new UserControl2();
                window = 2;
            }
            else
            {
                this.contentControl.Content = new UserControl1();
                window = 1;
            }
        }
    }
}
