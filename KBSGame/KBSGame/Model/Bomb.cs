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
    class Bomb : Obstacle
    {
        public BitmapImage bitmapImage = new BitmapImage();

        public Bomb(int StaticX = -1, int StaticY = -1)
        {
            //image is an attribute of obstacle
            image = new Image();
            image.Width = 50;
            image.Height = 50;

            BitmapImage bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("pack://application:,,,/Images/landmine-sprite.png");

            bitmapImage.DecodePixelWidth = 50;
            bitmapImage.EndInit();
            
            image.Source = bitmapImage;
            //assign the position where the bomb needs to be placed on the screen

            if(StaticX != -1 && StaticY != -1)
            {
                //assign the position where the bomb needs to be placed on the screen
                base.AssignStaticPosition("b", StaticX, StaticY);
            }
            else
            {
                //assign the position where the bomb needs to be placed on the screen
                base.AssignPosition("b");
            }

            image.Source = bitmapImage;

        }
    }
}
