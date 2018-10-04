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
        public EndPoint(Canvas canvas)
        {
            Rectangle rect = new Rectangle();
            rect.StrokeThickness = 3;
            rect.Stroke = Brushes.Red;
            rect.Width = 50;
            rect.Height = 50;
            Canvas.SetLeft(rect, 750);
            Canvas.SetTop(rect, 550);
            canvas.Children.Add(rect);

            Image myImage = new Image();
            myImage.Width = 50;

            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/flag.png");

            myBitmapImage.DecodePixelWidth = 50;
            myBitmapImage.EndInit();

            myImage.Source = myBitmapImage;

            Canvas.SetTop(myImage, 550);
            Canvas.SetLeft(myImage, 743);
            canvas.Children.Add(myImage);
        }
    }
}
