using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private double velocity = 0;                    //how quickly llama is dropping
        private double leapDist = 4;
       
        //timer variable
        private int minutes = 0;
        private int seconds = 0;

        int carry = 0;

        //for movement and generating of fences
        private List<Image> fences = new List<Image>();
        private const double approaching = 0.8;         //how fast fences move
        private const int distBetweenFence = 80;              //space between fences


        //NOTE: All bottom fences are even # and top fences are odd #
        //timers-- outside so collide stops them
        private DispatcherTimer gravTimer = new DispatcherTimer();
        private DispatcherTimer timeTimer = new DispatcherTimer();
        private DispatcherTimer genTimer = new DispatcherTimer();

        public GameScreen()
        {
            InitializeComponent();

            //time for constant drop of llama
            //dropping llama
            gravTimer.Tick += new EventHandler(gravityConstant);
            //moving fences
            gravTimer.Tick += new EventHandler(fenceMovement);
            gravTimer.Interval = TimeSpan.FromMilliseconds(0.5);
            gravTimer.Start();
            
            //timer of generating of fences
            genTimer.Tick += new EventHandler(GenerateFence);
            genTimer.Interval = TimeSpan.FromSeconds(distBetweenFence);
            genTimer.Start();

            //setting up timer for time display
            timeTimer.Tick += new EventHandler(timeSetter);
            timeTimer.Interval = TimeSpan.FromSeconds(1);
            timeTimer.Start();

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
            if(seconds == 0 && minutes == 0)
            {
                time.Text = "0:00";
            }
            else if(minutes == 0)
            {
                time.Text = "0:" + seconds.ToString();
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
            else if( e.Key == Key.P)
            {
                if (timeTimer.IsEnabled)
                {
                    gravTimer.Stop();
                    timeTimer.Stop();
                    genTimer.Stop();
                }
                else
                {
                    gravTimer.Start();
                    timeTimer.Start();
                    genTimer.Start();
                }
            }
        }

        private void GenerateFence(object sender, EventArgs e)
        {
            //NOTE: All bottom fences are even # and top fences are odd #
            //creating new bottom fence control
            allFences.fences.Add(new Image());
            //Top Fence
            allFences.genFence(true, Gameshow, llama);
            //Adding to Grid
            Gameshow.Children.Add(allFences.LastFence);
            //creating new top fence control
            allFences.fences.Add(new Image());
            //Bottom Fence
            allFences.genFence(false, Gameshow, llama);
            //Adding to Grid
            Gameshow.Children.Add(allFences.LastFence);
        }

        private void fenceMovement(object sender, EventArgs e)
        {

            //score half-adder
            bool sum = false;
          

            double pipeWidth = 30;
            List<int> remove = new List<int>();

            //moves all fences to left by variable -> approaching
            for(int i=0; i< allFences.fences.Count; i++)
            {
                if(allFences.fences[i].Margin.Left < -20)
                { Gameshow.Children.Remove(allFences.fences[i]); }
                else
                {

                    fences[i].Margin = new Thickness(fences[i].Margin.Left - approaching, fences[i].Margin.Top, fences[i].Margin.Right + approaching, fences[i].Margin.Bottom);
                    
                    //if llama is intersecting with pipe's vertical
                    if((llama.Margin.Right <= fences[i].Margin.Right+pipeWidth) && (llama.Margin.Left <= fences[i].Margin.Left+pipeWidth))
                    {
                        double fenceHeight = 420 - fences[i].Margin.Top - fences[i].Margin.Bottom;
                        //if top fence
                        if((fences[i].Margin.Top == (-1)) && llama.Margin.Top <= fences[i].Margin.Top+fenceHeight)
                        {
                            gravTimer.Stop();
                            timeTimer.Stop();
                            genTimer.Stop();
                            GameOver(carry);
                        }
                        //if bottom fence
                        else if((fences[i].Margin.Bottom == (-1)) && llama.Margin.Bottom <= fences[i].Margin.Bottom+fenceHeight)
                        {
                            gravTimer.Stop();
                            timeTimer.Stop();
                            genTimer.Stop();
                            GameOver(carry);
                        }
                        //if not hit update scoreBoard
                        else
                        {
                            sum = !sum;
                            if(!sum)
                            {
                                carry++;
                                ScoreBoard.Text = (carry).ToString();
                            }
                        }

                    }
                }
            }
        }
        //transition to High Scores screen
        void GameOver(int carry)
        {
            MessageBox.Show("GAME OVER");
            HighScores h = new HighScores(carry);
            h.Show();
            this.Close();
        }
    }
}
