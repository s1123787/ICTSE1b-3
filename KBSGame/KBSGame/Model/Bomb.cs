using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KBSGame.Model
{
    class Bomb : Obstakel
    {
        public Bomb(int StaticX = 0, int StaticY = 0)
        {
            image = new Image
            {
                Width = 50,
                Height = 50
            };
            
            BitmapImage bitmapImage = new BitmapImage(new Uri("pack://application:,,,/Images/landmine-sprite.png"));


            //myBitmapImage.BeginInit();
            //myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/landmine-sprite.png");

            //myBitmapImage.DecodePixelWidth = 50;
            //myBitmapImage.EndInit();

            //image.Source = myBitmapImage;

            if(StaticX != -1 && StaticY != -1)
            {
                base.AssignStaticPosition("b", StaticX, StaticY);
            } 
            else { 
                base.AssignPosition("b");
            }
            image.Source = bitmapImage;
        }
    }
}
