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
    class EndPoint
    {
        Rectangle rect;
        Image sprite;

        public EndPoint(Canvas canvas)
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

            //Add the rectangle and image to the screen
            Canvas.SetLeft(rect, 750);
            Canvas.SetTop(rect, 550);
            canvas.Children.Add(rect);
            Canvas.SetTop(sprite, 550);
            Canvas.SetLeft(sprite, 750);
            canvas.Children.Add(sprite);
        }

        //Method to remove the endpoint from the screen
        public void Delete(Canvas canvas)
        {
            canvas.Children.Remove(rect);
            canvas.Children.Remove(sprite);
        }
    }
}
