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
    class EndPoint
    {
        public EndPoint(Canvas canvas)
        {
            Rectangle rect = new Rectangle();
            rect.StrokeThickness = 3;
            rect.Stroke = Brushes.Red;
            rect.Width = 50;
            rect.Height = 50;
            Canvas.SetLeft(rect, 743);
            Canvas.SetTop(rect, 550);
            canvas.Children.Add(rect);
        }
    }
}
