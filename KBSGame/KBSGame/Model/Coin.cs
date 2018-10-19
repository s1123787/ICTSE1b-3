using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace KBSGame.Model
{
    //coin is a generalisation of Obstacle
    class Coin : Obstacle
    {
        public Coin(int StaticX = -1, int StaticY = -1)
        {
            //image is an attribute of obstacle
            image = new Image
            {
                Width = 50,
                Height = 50
            };

            BitmapImage bitmapImage = new BitmapImage(new Uri("pack://application:,,,/Images/coin.png"));

            image.Source = bitmapImage;

            //asssign preset position
            if (StaticX != -1 && StaticY != -1)
            {
                base.AssignStaticPosition("c", StaticX, StaticY);
            }
            //assign random position
            else
            {
                base.AssignPosition("c");
            }
        }
    }
}
