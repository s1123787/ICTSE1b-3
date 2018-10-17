using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KBSGame.Model
{
    //bomb is a generalisation of Obstacle
    class Bomb : Obstakel
    {
        public Bomb()
        {
            //image is a attribute of obstacle
            image = new Image();
            image.Width = 50;
            image.Height = 50;

            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/landmine-sprite.png");

            myBitmapImage.DecodePixelWidth = 50;
            myBitmapImage.EndInit();

            image.Source = myBitmapImage;
            //assign the position where do bomb need to be placed on the screen
            base.AssignPosition("b");
        }
    }
}
