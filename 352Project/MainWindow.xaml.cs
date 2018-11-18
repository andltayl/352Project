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
        //for movement of llama
        private double gravity = 0.08;
        private double velocity = 0;
        private double leapDist = 5;
        //for score and generating fences
        private int fenceNum = 0;
        //NOTE: All bottom fences are even # and top fences are odd #


        public MainWindow()
        {
            InitializeComponent();

            //time for constant drop of llama
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(gravityConstant);
            timer.Interval = TimeSpan.FromMilliseconds(0.5);
            timer.Start();
            //timer of generating of fences
            DispatcherTimer genTimer = new DispatcherTimer();
            genTimer.Tick += new EventHandler(GenerateFence);
            genTimer.Interval = TimeSpan.FromSeconds(5);
            genTimer.Start();

            //On Spacebar press -> llama jumps
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

        private void GenerateFence(object sender, EventArgs e)
        {
            //NOTE: All bottom fences are even # and top fences are odd #
            //creating new bottom fence control
            Image bottomFence = new Image();
                //Top Fence
            genContent(bottomFence,false);
                //Adding to Grid
            Gameshow.Children.Add(bottomFence);

            //creating new top fence control
            Image topFence = new Image();
                //Bottom Fence
            genContent(topFence, true);
                //Adding to Grid
            Gameshow.Children.Add(topFence);

        }

        private void genContent(Image createdFence, bool Top)
        {
            //Name
            createdFence.Name = "fence_" + fenceNum.ToString();
            //Source
            BitmapImage fencePic = new BitmapImage();
            fencePic.BeginInit();
            //fencePic.UriSource = new Uri("G:/Users/Soul/Documents/GitHub/352Project/tempFence.png");
            fencePic.UriSource = new Uri("pack://application:,,,/Resources/tempFence.png");
            fencePic.EndInit();
            createdFence.Source = fencePic;
            //Stretch
            createdFence.Stretch = Stretch.Fill;
            //Margins
            //Thickness(Left,Top,Right,Bottom)
            //after motion made change left -> Gameshow.Margin.Right, right -> Gameshow.Margin.Right
            if (Top)
            {
                createdFence.Margin = new Thickness(800, -100, Gameshow.Margin.Right-10, 249);
                //flip
                createdFence.RenderTransformOrigin = new Point { X = 0.5, Y = 0.5 };
                createdFence.RenderTransform = new ScaleTransform() { ScaleY = -1 };
                createdFence.UpdateLayout();
            }
            else
            {                
                createdFence.Margin = new Thickness(800, 263, -5, Gameshow.Margin.Bottom-170);
            }
        }
    }
}
