using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KBSGame
{
    class Muur
    {
        public Muur(Canvas canvas, double x, double y)
        {
            Rectangle rect = new Rectangle();
            rect.Fill = Brushes.Purple;
            rect.Width = 50;
            rect.Height = 50;
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            canvas.Children.Add(rect);
        }
    }
}
