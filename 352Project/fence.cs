using System;
using System.Collections.Generic;
//for Thickness in Margins
using System.Windows;
//for window controls, Grid & Image
using System.Windows.Controls;
//for Image manipulation
using System.Windows.Media;
//for BitmapImage
using System.Windows.Media.Imaging;

namespace _352Project
{
    class Fence
    {
        //-----------------------------------------PUBLIC-----------------------------------------//
        //holds all fences
        public List<Image> fences = new List<Image>();
        public int score = 0;
        //----------------------------------------PROTECTED----------------------------------------//
        //how fast fences move
        protected double approaching;   //approaching demo = 3
        //space between fences
        protected int wOfBetween;       //wOfBetween demo = 80
        //points given between difficulty
        protected int sumTotal;   //sumTotal demo = 100
        
        //----------------------------------------PRIVATE----------------------------------------//
        private double pipeWidth = 30;
        private int sum = 0;

        //----------------------------------------ACCESSORS----------------------------------------//
        //to return last fence to be edited
        public Image LastFence
        {
            get { return fences[fences.Count - 1];  }
        }
        //to return approaching
        public double Approaching
        {
            get { return approaching; }
        }

        //----------------------------------------FUNCTIONS----------------------------------------//
        //Constructors
        public Fence(int disBetween, double movement, int pointsNeededToScore)
        {
            //settings for difficulty
            approaching = movement;
            wOfBetween = disBetween;
            sumTotal = pointsNeededToScore;
        }

        //Make a fence and add to the list
        public void genFence(bool Top, Grid Gameshow, Image llama)
        {
            fences.Add(new Image());
            //Source
            BitmapImage fencePic = new BitmapImage();
            fencePic.BeginInit();
            //fencePic.UriSource = new Uri("G:/Users/Soul/Documents/GitHub/352Project/tempFence.png");
            fencePic.UriSource = new Uri("pack://application:,,,/Resources/tempFence.png");
            fencePic.EndInit();
            fences[fences.Count - 1].Source = fencePic;
            //Stretch
            fences[fences.Count - 1].Stretch = Stretch.Fill;
            //Margins
            //size need so llama can jump thru with little room 
            double sizeTest = (Gameshow.ActualHeight + llama.ActualHeight + (wOfBetween * 2)) / 2;
            //random sizes of fences
            Random random = new Random();
            double spaceChanger = (-100) + (random.NextDouble() * (100 * 2)); //between 100 up or down on fence positions
            double fenceTopLen = sizeTest - spaceChanger;
            double fenceBottomLen = sizeTest + spaceChanger;
            //NOTE: All bottom fences are even # and top fences are odd #
            //Thickness(Left,Top,Right,Bottom)
            if (Top)
            {
                fences[fences.Count - 1].Margin = new Thickness(Gameshow.ActualWidth, -1, -pipeWidth, fenceTopLen);
                //flip
                fences[fences.Count - 1].RenderTransformOrigin = new Point { X = 0.5, Y = 0.5 };
                fences[fences.Count - 1].RenderTransform = new ScaleTransform() { ScaleY = -1 };
                fences[fences.Count - 1].UpdateLayout();
            }
            else
            {
                fences[fences.Count - 1].Margin = new Thickness(Gameshow.ActualWidth, fenceBottomLen, -pipeWidth, -1);
            }
        }
        
        //Move a certain fence 
        public void moveFence(int i)
        {
            fences[i].Margin = new Thickness(fences[i].Margin.Left-approaching, fences[i].Margin.Top, fences[i].Margin.Right + approaching, fences[i].Margin.Bottom);
        }

        public bool CollisionDetect(Image llama, int i)
        {
            //if llama is intersecting with pipe's vertical
            if ((llama.Margin.Right <= fences[i].Margin.Right + pipeWidth) && (llama.Margin.Left <= fences[i].Margin.Left + pipeWidth))
            {
                double fenceHeight = 420 - fences[i].Margin.Top - fences[i].Margin.Bottom;
                //if top fence
                if ((fences[i].Margin.Top == (-1)) && llama.Margin.Top <= fences[i].Margin.Top + fenceHeight) { return true; }
                //if bottom fence
                else if ((fences[i].Margin.Bottom == (-1)) && llama.Margin.Bottom <= fences[i].Margin.Bottom + fenceHeight) { return true; }
                //if not hit update scoreBoard
                else
                {
                    sum += 1;
                    if (sum == sumTotal)
                    {
                        sum = 0;
                        score += 1;
                    }
                    return false;
                }
            }
            else { return false; }
        }
    }
}
