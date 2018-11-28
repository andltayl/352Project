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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        int difficultyNum = 0;      //number that correlates to difficulty setting
        bool up = false;            //direction llama should move
        int llamaHighPoint = 43;    //starting position of llama (top)
        int llamaLowPoint = 90;     //lowest position for llama path

        public MainMenu()
        {
          
            InitializeComponent();
            InitializeComponent();

            //timer for llama movement
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(animateLlama);
            timer.Interval = TimeSpan.FromMilliseconds(0.5);
            timer.Start();

        }

        //llama moves up and down
        private void animateLlama(object sender, EventArgs e)
        {
            //if llama hasn't reached minimum point, keep lowering
            if (!up)
           {
                if (llama.Margin.Top == llamaLowPoint)
                    up = true;
                llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top + 1, llama.Margin.Right, llama.Margin.Bottom - 1);
           }

            //otherwise, start moving up
            
            else
            {
                if (llama.Margin.Top == llamaHighPoint)
                    up = false;
                llama.Margin = new Thickness(llama.Margin.Left, llama.Margin.Top - 1, llama.Margin.Right, llama.Margin.Bottom + 1);
            }
        }


        //scroll through difficulties
        public void difficultySelect(object sender, RoutedEventArgs e)
        {
            //increment difficulty choice on each click
            difficultyNum++;

            switch (difficultyNum) {
                case 1: difficultyButton.Content = "Easy";
                    break;
                case 2: difficultyButton.Content = "Normal";
                    break;
                default: difficultyButton.Content = "Hard";
                    difficultyNum = 0;
                    break;
            }

      
        }

        //exit program via quit button
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        //start game
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            GameScreen m = new GameScreen();
            m.Show();
            this.Close();
        }

        //view high scores
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HighScores h = new HighScores(-1);
            h.Show();
            this.Close();
        }
    }
}
