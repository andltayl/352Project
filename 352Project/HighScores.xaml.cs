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
using System.IO;


namespace _352Project
{
    /// <summary>
    /// Interaction logic for HighScores.xaml
    /// </summary>
    public partial class HighScores : Window
    {
        const int MAX_SIZE = 10;


        private int[] Scores = new int[MAX_SIZE];
        private string[] Names = new string[MAX_SIZE];

        //FileStream highScores;
        System.IO.StreamReader file;
        System.IO.StreamWriter outFile;

        string line;                //line from file being read
        int score;                  //score of player
        int position = 0;           //position in high score list
        bool addToList = false;     //add to list if score is high enough
        string newName;             //new name for high score player
        string selectedLlama;

        //difficulty to be used on retry
        private int difficulty;
        public HighScores(int _score, int diffNum, string selectedSkin)
        {
            selectedLlama = selectedSkin;
            difficulty = diffNum;
            InitializeComponent();

            //initialize arrays
            for (int i = 0; i < 10; i++)
            {
                Scores[i] = 0;
                Names[i] = "???";
            }

            //check if file exists
            if (File.Exists("HighScores.txt"))
                file = new System.IO.StreamReader("HighScores.txt");

            //if file doesn't exist, make a new one:
            else
            {
                //File.Create("HighScores.txt");
                System.IO.StreamWriter tw = new StreamWriter("HighScores.txt");
                for (int i = 0; i < MAX_SIZE; i++)
                    tw.WriteLine("??? 0");
                tw.Close();
            }

            //print latest score
            yourScore.Text = yourScore.Text + _score.ToString();

            score = _score;

            //print high scores
            getScores();

            //if score is -1, then user comes from main menu
            if (score == -1)
            {
                GameOverMessage.Text = "Have fun and good luck!";
                yourScore.Visibility = Visibility.Hidden;
                RetryButton.Visibility = Visibility.Hidden;
            }

            

            //add high score to list
            if (addToList == true)
            {
                addScore();

            }
        }


        //get scores from file
        private void getScores()
        {
            file = new System.IO.StreamReader("HighScores.txt");
            int i = 0;  //counter for Names[] and Scores[]

            while ((line = file.ReadLine()) != null)
            {
                //split name from score
                string[] Info = line.Split(' ');

                //print and store each item in file
                for (int k = 0; k < Info.Length; k++)
                {
                    if (k % 2 == 0)
                    {
                        Names[i] = Info[k];
                        ScoreListNames.Text = ScoreListNames.Text + Names[i] + "\n";

                    }
                    else
                    {
                        int t = Convert.ToInt32(Info[k]);
                        Scores[i] = t;
                        ScoreListScores.Text = ScoreListScores.Text + Scores[i].ToString() + "\n";


                        //if player makes it on list
                        if (score > Scores[i] && addToList == false)
                        {
                            addToList = true;
                            position = i;
                        }
                        i++;
                    }


                }
            }

            file.Close();
        }

        //show high score messages
        private void addScore()
        {
            HighScoreMessage.Visibility = Visibility.Visible;
            EnterName.Visibility = Visibility.Visible;
            NameInput.Visibility = Visibility.Visible;
            InputButton.Visibility = Visibility.Visible;
        }

        //retry
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameScreen m = new GameScreen(difficulty, selectedLlama);
            m.Show();
            this.Close();

        }

        //main menu
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainMenu m = new MainMenu();
            m.Show();
            this.Close();
        }

        //adds name to list
        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            newName = NameInput.Text;   //player's name
            
            if (newName.Length > 9)
            {
                tooLong.Visibility = Visibility.Visible;
                return;
            }

            
            //hide OK button and "name too long message" 
            InputButton.Visibility = Visibility.Hidden;
            tooLong.Visibility = Visibility.Hidden;

            //open output file to save list
            outFile = new System.IO.StreamWriter("HighScores.txt");

            //shift items in array 
            for (int i = MAX_SIZE - 1; i > position; i--)
            {
                Scores[i] = Scores[i - 1];
                Names[i] = Names[i - 1];

            }

            //insert new score
            Scores[position] = score;
            Names[position] = newName;

            //reset printed list
            ScoreListNames.Text = null;
            ScoreListScores.Text = null;

            //save changes
            for (int i = 0; i < MAX_SIZE; i++)
            {
                //print new list to screen
                ScoreListNames.Text = ScoreListNames.Text + Names[i] + "\n";
                ScoreListScores.Text = ScoreListScores.Text + Scores[i] + "\n";

                //save list to file
                outFile.WriteLine(Names[i] + " " + Scores[i]);
            }

            outFile.Close();
        }
    }
}