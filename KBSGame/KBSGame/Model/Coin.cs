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
            image = new Image();
            image.Width = 50;
            image.Height = 50;

            BitmapImage bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("pack://application:,,,/Images/coin.png");

            bitmapImage.DecodePixelWidth = 50;
            bitmapImage.EndInit();

            image.Source = bitmapImage;
            base.AssignPosition("c");
        }
    }
}
