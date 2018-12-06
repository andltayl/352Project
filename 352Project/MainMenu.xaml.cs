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
        int difficultyNum = 2;      //number that correlates to difficulty setting
        int skinNum = 0;            //number corresponding to the skin currently selected
        bool up = false;            //direction llama should move
        int llamaHighPoint = 43;    //starting position of llama (top)
        int llamaLowPoint = 90;     //lowest position for llama path
        string selectedLlama;
    

        public MainMenu()
        {
            llamaSkin basicLlama = new DefaultLlama();


            selectedLlama = basicLlama.getSkin();           
      
            InitializeComponent();
            InitializeComponent();

            

            ImageSource skin = new ImageSourceConverter().ConvertFromString(selectedLlama) as ImageSource;
            llama.Source = skin;
      

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
                case 1: difficultyButton.Content = "Difficulty: Easy";
                    break;
                case 2: difficultyButton.Content = "Difficulty: Medium";
                    break;
                default: difficultyButton.Content = "Difficulty: Hard";
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
            GameScreen m = new GameScreen(difficultyNum, selectedLlama);
            m.Show();
            this.Close();
        }

        //view high scores
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HighScores h = new HighScores(-1, difficultyNum, selectedLlama);
            h.Show();
            this.Close();
        }

        private void Skin_Select_Button(object sender, RoutedEventArgs e)
        {
            //increment difficulty choice on each click
            skinNum++;

      
            
            switch (skinNum)
                    
            {
                case 1:
                    llamaSkin basicLlama = new BrownLlama(new DefaultLlama());
                    selectedLlama = basicLlama.getSkin();
                    ImageSource skin = new ImageSourceConverter().ConvertFromString(selectedLlama) as ImageSource;
                    llama.Source = skin;
                    break;
                case 2:
                    llamaSkin basicLlama1 = new PinkLlama(new DefaultLlama());
                    selectedLlama = basicLlama1.getSkin();
                    ImageSource skin1 = new ImageSourceConverter().ConvertFromString(selectedLlama) as ImageSource;
                    llama.Source = skin1;
                    break;
                default:
                    llamaSkin basicLlama2 = new DefaultLlama();
                    selectedLlama = basicLlama2.getSkin();
                    ImageSource skin2 = new ImageSourceConverter().ConvertFromString(selectedLlama) as ImageSource;
                    llama.Source = skin2;
              
                    skinNum = 0;
                    break;
            }
        }
    }
}
