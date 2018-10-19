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
    public class StartPoint
    {
        public Rectangle rect;
        public int X, Y;
        public StartPoint()
        {
            //Create rectangle to identify start
            rect = new Rectangle
            {
                Width = 50,
                Height = 50,
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };

            //Set coardinate values
            X = 0;
            Y = 0;
        }
    }
}
