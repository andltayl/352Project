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

        public HighScores(int _score)
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                Scores[i] = 0;
                Names[i] = "???";
            }

            // highScores = new FileStream("HighScores.txt", FileMode.Open, FileAccess.ReadWrite);
            file = new System.IO.StreamReader("HighScores.txt");



            //print latest score
            yourScore.Text = yourScore.Text + _score.ToString();

            score = _score;

            //print high scores
            getScores();

            file.Close();
            //add high score to list
            if (addToList == true)
            {
                addScore();

            }
        }


        private void getScores()
        {
            while ((line = file.ReadLine()) != null)
            {
                //split name from score
                string[] Info = line.Split(' ');


                int c = 0;  //counter that dictates name or score
                int i = 0;  //counter for Names[] and Scores[]

                //print and store each item in file
                foreach (string info in Info)
                {
                    if (c % 2 == 0)
                    {
                        ScoreListNames.Text = ScoreListNames.Text + info + "\n";
                        Names[i] = info;
                    }
                    else
                    {
                        ScoreListScores.Text = ScoreListScores.Text + info + "\n";
                        Scores[i] = Convert.ToInt32(info);


                        //if player makes it on list
                        if (score > Scores[i] && addToList == false)
                        {
                            addToList = true;
                            position = i;
                        }
                        i++;
                    }

                    c++;
                    if (c > MAX_SIZE - 1)
                        return;
                }
            }
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
            GameScreen m = new GameScreen();
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
            newName = NameInput.Text;
            InputButton.Visibility = Visibility.Hidden;
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

            //save changes
            ScoreListNames.Text = " ";
            ScoreListScores.Text = " ";

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

