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
    class StartPoint
    {
        public StartPoint(Canvas canvas)
        {
            //Create rectangle to identify start
            Rectangle rect = new Rectangle
            {
                Width = 50,
                Height = 50,
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };

            //Add the rectangle to the screen
            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, 0);
            canvas.Children.Add(rect);
        }
    }
}
