using KBSGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KBSGame
{
    public class Obstakel
    {
        public int x { get; private set; }
        public int y { get; private set; }
        private int width = 50;
        private int height = 50;
        //public Rectangle rect;
        public Image image;
        private string Type;
        Random random = new Random();

        public Obstakel(String z)
        {
            image = new Image();
            //rect = new Rectangle();
            SetType(z);
            //rect.Width = 50;
            //rect.Height = 50;
            ////Canvas.SetLeft(rect, 800);
            ////Canvas.SetTop(rect, 600);
            AssignPosition();
            ////canvas.Children.Add(rect);
            ////rect = null;
        }
        public void SetType(string z)
        {
            if (z == "Bom")
            {
                this.Type = "Bom";

                
                image.Width = 50;
                image.Height = 50;

                BitmapImage myBitmapImage = new BitmapImage();

                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/tnt-sprite.png");

                myBitmapImage.DecodePixelWidth = 50;
                myBitmapImage.EndInit();

                image.Source = myBitmapImage;

                //rect.Fill = Brushes.DarkRed;
                //rect.Opacity = 0.5;
                
            }
            else if (z == "Boom")
            {
                this.Type = "Boom";

                image.Width = 50;
                image.Height = 50;

                BitmapImage myBitmapImage = new BitmapImage();

                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/tree-sprite.png");

                myBitmapImage.DecodePixelWidth = 50;
                myBitmapImage.EndInit();

                image.Source = myBitmapImage;

                //rect.Fill = Brushes.ForestGreen;
                //rect.Opacity = 0.5;
                
            }
            else if (z == "moving")
            {
                this.Type = "Boom";

                image.Width = 50;
                image.Height = 50;

                BitmapImage myBitmapImage = new BitmapImage();

                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/moving-sprite.png");

                myBitmapImage.DecodePixelWidth = 50;
                myBitmapImage.EndInit();

                image.Source = myBitmapImage;

                MoveObstakelRandom();
            }
        }

        public void AssignPosition()
        {            
            bool niet = true;
            while ((x <= 100 && y <= 100) || (x >= 650 && y >= 450) || Obstakels.waardes.Contains($"{x}{y}"))
            {
                x = random.Next(0, 15) * 50;
                y = random.Next(0, 11) * 50;
            }
            Obstakels.waardes.Add($"{x}{y}");

            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
        }

        public void MoveObstakelRandom()
        {

        }
    }
}
