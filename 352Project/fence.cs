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
    abstract class Fence
    {
        //-----------------------------------------PUBLIC-----------------------------------------//
        //holds all fences
        public List<Image> fences = new List<Image>();
        public int score = 0;
        public string resourceImgDir = "pack://application:,,,/Resources/";
        //----------------------------------------PROTECTED----------------------------------------//

        //----------------------------------------PRIVATE----------------------------------------//
        protected double pipeWidth = 30;
        protected int sum = 0;
        //----------------------------------------ACCESSORS----------------------------------------//
        public Image LastFence { get { return fences[fences.Count - 1]; } }     //to return last fence to be edited

        //----------------------------------------FUNCTIONS----------------------------------------//
        //Constructors
        public Fence() { }
        //Make a fence and add to the list
        public abstract void genFence(bool Top, Grid Gameshow, Image llama);
        //Move a certain fence 
        public abstract void moveFence(int i);
        //true if collided, false if fine
        public abstract bool CollisionDetect(Image llama, int i);
    }
}
