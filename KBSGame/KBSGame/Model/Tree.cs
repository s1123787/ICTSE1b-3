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
            Canvas.SetZIndex(image, 2);

            BitmapImage bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();

            Random random = new Random();
            int i = random.Next(0, 3);
            if (i == 0)
            {
                bitmapImage.UriSource = new Uri("pack://application:,,,/Images/hell-tree-sprite.png");
            }
            else if (i == 1)
            {
                bitmapImage.UriSource = new Uri("pack://application:,,,/Images/tomb-stone-sprite.png");
            }
            else if (i == 2)
            {
                bitmapImage.UriSource = new Uri("pack://application:,,,/Images/weeping-angel-sprite.png");
            }
            
            bitmapImage.EndInit();

            image.Source = bitmapImage;

            base.AssignPosition("t");
        }
    }
}
