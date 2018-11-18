using System;
using System.Drawing;
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
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Window
    {
        private double gravity = 0.1;
        private double velocity = 0;
        private double leapDist = 3.5;
        private int seconds = 0;
     

        public GameScreen()
        {
            //timer for jumps
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(gravityConstant);
            timer.Interval = TimeSpan.FromMilliseconds(0.5);
            timer.Start();

            //timer for time display
            DispatcherTimer t = new DispatcherTimer();
            t.Tick += new EventHandler(timeSetter);
            t.Interval = TimeSpan.FromSeconds(1);
            t.Start();
            this.KeyDown += new KeyEventHandler(OnSpaceDownHandler);
        }

        //display time
        private void timeSetter(object sender, EventArgs e)
        {
            seconds++;
            time.Text = seconds.ToString();
        }
     

        private void gravityConstant(object sender, EventArgs e)
        {
            //let the player go only slightly off screen
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
                if (velocity > 0) // if there is downward momentum cancel it and jump up
                {
                    velocity = 0;
                    velocity -= leapDist;
                }
                else
                {
                    velocity -= leapDist;
                }
                //llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top - 50, llama.Margin.Right, llama.Margin.Bottom + 50);
            }

            //player pauses game
            //if (e.Key == Key.P) 
                //freeze timer?

        }

      
    }
}
