using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KBSGame
{
    public class EndPoint
    {
        public Rectangle rect;
        public Image sprite;
        public int X, Y;

        public EndPoint()
        {
            //Create rectangle to identify finish
            rect = new Rectangle
            {
                Width = 50,
                Height = 50,
                Stroke = Brushes.Red,
                StrokeThickness = 3
            };
            
            //Create image of flag for finish
            sprite = new Image
            {
                Width = 50,
                Height = 50
            };
            sprite.Source = new BitmapImage(new Uri("pack://application:,,,/Images/flag-sprite.png"));

            //Set coardinate values
            X = 750;
            Y = 550;
        }
    }
}
