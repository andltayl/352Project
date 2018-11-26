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
        private double leapDist = 2;
        //for movement and generating of fences
        private List<Image> fences = new List<Image>();
        private const double approaching = 0.8;
        //NOTE: All bottom fences are even # and top fences are odd #


        public MainWindow()
        {
            InitializeComponent();

            //time for constant drop of llama
            DispatcherTimer timer = new DispatcherTimer();
                //dropping llama
            timer.Tick += new EventHandler(gravityConstant);
                //moving fences
            timer.Tick += new EventHandler(fenceMovement);
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
                //velocity -= leapDist;
                velocity = -leapDist;
                //llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top - 50, llama.Margin.Right, llama.Margin.Bottom + 50);
            }
        }

        private void GenerateFence(object sender, EventArgs e)
        {
            //NOTE: All bottom fences are even # and top fences are odd #
            //creating new bottom fence control
            fences.Add(new Image());
                //Top Fence
            genContent(fences[fences.Count-1],false);
                //Adding to Grid
            Gameshow.Children.Add(fences[fences.Count-1]);

            //creating new top fence control
            fences.Add(new Image());
                //Bottom Fence
            genContent(fences[fences.Count - 1], true);
                //Adding to Grid
            Gameshow.Children.Add(fences[fences.Count - 1]);

        }

        private void genContent(Image createdFence, bool Top)
        {
            //Name
            createdFence.Name = "fence_" + fences.Count.ToString();
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
                createdFence.Margin = new Thickness(800, -100, -40, 249);
                //flip
                createdFence.RenderTransformOrigin = new Point { X = 0.5, Y = 0.5 };
                createdFence.RenderTransform = new ScaleTransform() { ScaleY = -1 };
                createdFence.UpdateLayout();
            }
            else
            {                
                createdFence.Margin = new Thickness(800, 263, -40, -170);
            }
        }

        private void fenceMovement(object sender, EventArgs e)
        {
            foreach(Image i in fences)
            {
                i.Margin = new Thickness(i.Margin.Left- approaching, i.Margin.Top, i.Margin.Right + approaching, i.Margin.Bottom);
            }
        }
    }
}
