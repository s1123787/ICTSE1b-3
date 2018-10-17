using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace KBSGame.Model
{
    class Coin : Obstakel
    {
        public Coin()
        {
            image = new Image
            {
                Width = 50,
                Height = 50
            };

            BitmapImage bitmapImage = new BitmapImage(new Uri("pack://application:,,,/Images/coin.png"));

            image.Source = bitmapImage;
            base.AssignPosition("c");
        }
    }
}
