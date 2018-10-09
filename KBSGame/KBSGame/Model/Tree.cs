﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KBSGame.Model
{
    public class Tree : Obstakel
    {
        //public int x { get; private set; }
        //public int y { get; private set; }


        public Tree(string z): base (z)
        {

            image = new Image();
            image.Width = 50;
            image.Height = 50;

            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/tree-sprite.png");

            myBitmapImage.DecodePixelWidth = 50;
            myBitmapImage.EndInit();

            image.Source = myBitmapImage;

            base.AssignPosition("t");
        }


    }
}