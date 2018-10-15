using System;
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


        public Tree()
        {

            image = new Image();
            image.Width = 50;
            image.Height = 50;

            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();

            Random random = new Random();
            int i = random.Next(0, 3);
            if (i == 0)
            {
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/tomb-stone-sprite.png");
            }
            else if (i == 1)
            {
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/tomb-stone2-sprite.png");
            }
            else if (i == 2)
            {
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/weeping-angel-sprite.png");
            }

            myBitmapImage.DecodePixelWidth = 50;
            myBitmapImage.EndInit();

            image.Source = myBitmapImage;

            base.AssignPosition("t");
        }


    }
}
