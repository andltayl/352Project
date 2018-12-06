using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace _352Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameScreen : Window
    {
        //for movement of llama
        private double gravity = 0.1;
        private double velocity = 0;                    //how quickly llama is dropping
        private double leapDist = 3.5;
        //timer variable
        private int minutes = 0;
        private int seconds = 0;
        //Difficulty settings
        private int difNum = 0;
        private double distBetweenFence = 5; //Distance between each fence       demo = 5           Med = 1     Hard = 1
        private Fence allFences;
        //NOTE: All bottom fences are even # and top fences are odd #
        //timers-- outside so collide stops them
        private DispatcherTimer gravTimer = new DispatcherTimer();
        private DispatcherTimer timeTimer = new DispatcherTimer();
        private DispatcherTimer genTimer = new DispatcherTimer();
        string selectedLlama;

        public GameScreen(int difficulty, string selectedSkin)
        {
            difNum = difficulty;
            selectedLlama = selectedSkin;
            changeDiff();

            InitializeComponent();

            ImageSource skin = new ImageSourceConverter().ConvertFromString(selectedLlama) as ImageSource;
            llama.Source = skin;

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
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
            }
            if (seconds == 0 && minutes == 0)
            {
                time.Text = "0:00";
            }
            else if (minutes == 0)
            {
                time.Text = "0:" + seconds.ToString();
            }
            time.Text = minutes.ToString() + ":" + seconds.ToString();
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
            else if (e.Key == Key.P)
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
            else if (e.Key == Key.Escape)
            {
                gravTimer.Stop();
                timeTimer.Stop();
                genTimer.Stop();
                //add to open a popup to retry or go to new window
                GameOver(allFences.score);
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
            //moves all fences to left by variable -> approaching
            for (int i = 0; i < allFences.fences.Count; i++)
            {
                if (allFences.fences[i].Margin.Left < -20)
                { Gameshow.Children.Remove(allFences.fences[i]); }
                else
                {
                    allFences.moveFence(i);
                    bool collide = allFences.CollisionDetect(llama, i);
                    if (collide)
                    {
                        gravTimer.Stop();
                        timeTimer.Stop();
                        genTimer.Stop();
                        //add to open a popup to retry or go to new window
                        GameOver(allFences.score);
                    }
                    else
                    {
                        ScoreBoard.Text = allFences.score.ToString();
                    }
                }
            }
        }

        //upon start use difficulty selected
        private void changeDiff()
        {
            switch (difNum)
            {
                //Easy
                case 1:
                    allFences = new EasyFence();
                    distBetweenFence = 5;
                    break;
                //Medium
                case 2:
                    allFences = new NormalFence();
                    distBetweenFence = 2.5;
                    break;
                //Hard
                default:
                    allFences = new HardFence();
                    distBetweenFence = 1;
                    break;
            }
        }

        //transition to High Scores screen
        void GameOver(int carry)
        {
          
            //points changed for difficulty
            if (difNum == 1) { carry /= 2; }    //Easy
            else if (difNum == 2) { }           //Normal
            else { carry *= 2; }                //Hard

            HighScores h = new HighScores(carry, difNum, selectedLlama);
            h.Show();
            this.Close();
        }
    }
}
