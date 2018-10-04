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
            Rectangle rect = new Rectangle();
            rect.StrokeThickness = 3;
            rect.Stroke = Brushes.Black;
            rect.Width = 50;
            rect.Height = 50;
            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, 0);
            canvas.Children.Add(rect);

        }
    }
}
