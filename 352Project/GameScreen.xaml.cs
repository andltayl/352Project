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
    public partial class GameScreen : Window
    {
        //for movement of llama
        private double gravity = 0.08;
        private double velocity = 0;
        private double leapDist = 4;
        //timer variable
        private int minutes = 0;
        private int seconds = 0;
        //for movement and generating of fences
        private List<Image> fences = new List<Image>();
        private const double approaching = 0.8;
        //NOTE: All bottom fences are even # and top fences are odd #


        public GameScreen()
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
            genTimer.Interval = TimeSpan.FromSeconds(3);
            genTimer.Start();
            //timer for time display
            DispatcherTimer t = new DispatcherTimer();
            t.Tick += new EventHandler(timeSetter);
            t.Interval = TimeSpan.FromSeconds(1);
            t.Start();
            //On Spacebar press -> llama jumps
            this.KeyDown += new KeyEventHandler(OnSpaceDownHandler);


        }

        //display time
        private void timeSetter(object sender, EventArgs e)
        {
            seconds++;
            if(seconds == 60)
            {
                minutes++;
                seconds = 0;
            }
            time.Text = minutes.ToString()+":"+seconds.ToString();
        }


        private void gravityConstant(object sender, EventArgs e)
        {
          if (llama.Margin.Top > 0)
          {
              velocity += gravity;

              llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top + velocity, llama.Margin.Right, llama.Margin.Bottom - velocity);
          }

          else
          {
              velocity = gravity;
              llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top + 1, llama.Margin.Right, llama.Margin.Bottom - 1);
          }
        }

        private void OnSpaceDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (velocity > 0)
                    velocity = -leapDist;
                else
                    velocity -= leapDist;
                //velocity -= leapDist;
                //llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top - 50, llama.Margin.Right, llama.Margin.Bottom + 50);
            }
        }

        private void GenerateFence(object sender, EventArgs e)
        {
            //NOTE: All bottom fences are even # and top fences are odd #
            //creating new bottom fence control
            fences.Add(new Image());
            //Top Fence
            genContent(fences[fences.Count - 1], false);
            //Adding to Grid
            Gameshow.Children.Add(fences[fences.Count - 1]);

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
                //size need so llama can jump thru with little room
            double sizeTest = (Gameshow.ActualHeight + llama.ActualHeight+10)/2;
                //random 
            Random random = new Random();
            List<double> fenceSizes = new List<double>(3);
            double spaceChanger = (random.NextDouble()*((sizeTest-20) - 0) + 0);
            fenceSizes.Add(sizeTest - spaceChanger);
            fenceSizes.Add(sizeTest + spaceChanger);
            //Thickness(Left,Top,Right,Bottom)
            //after motion made change left -> Gameshow.Margin.Right, right -> Gameshow.Margin.Right
            if (Top)
            {
                createdFence.Margin = new Thickness(800, -1, -40, fenceSizes[0]);
                //flip
                createdFence.RenderTransformOrigin = new Point { X = 0.5, Y = 0.5 };
                createdFence.RenderTransform = new ScaleTransform() { ScaleY = -1 };
                createdFence.UpdateLayout();
            }
            else
            {
                createdFence.Margin = new Thickness(800, fenceSizes[1], -40, -1);
            }
        }

        private void fenceMovement(object sender, EventArgs e)
        {
            List<int> remove = new List<int>();
            //moves all fences to left by variable -> approaching
            for(int i=0; i< fences.Count; i++)
            {
                if(fences[i].Margin.Left < -20)
                {
                    remove.Add(i);
                }
                else
                {
                    fences[i].Margin = new Thickness(fences[i].Margin.Left - approaching, fences[i].Margin.Top, fences[i].Margin.Right + approaching, fences[i].Margin.Bottom);
                }
            }
            //removes fence once off-screen
            foreach (int i in remove)
            {

                if(i <= fences.Count)
                {
                    Gameshow.Children.Remove(fences[i]);
                }
            }
        }
    }
}
