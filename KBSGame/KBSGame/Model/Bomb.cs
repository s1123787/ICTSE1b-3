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
        public Bomb()
        {
            image = new Image
            {
                Width = 50,
                Height = 50
            };
            
            BitmapImage bitmapImage = new BitmapImage(new Uri("pack://application:,,,/Images/landmine-sprite.png"));

            image.Source = bitmapImage;
            base.AssignPosition("b");
        }
    }
}
