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
        public Image image;
        public MediaElement gif;
        public string Type { get; private set; }
        Random random = new Random();

        public Obstakel(String z)
        {
            image = new Image();
            gif = new MediaElement();
            SetType(z);
            AssignPosition();
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
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/landmine-sprite.png");

                myBitmapImage.DecodePixelWidth = 50;
                myBitmapImage.EndInit();

                image.Source = myBitmapImage;

                //gif.Width = 50;
                //gif.Height = 50;
                //gif.LoadedBehavior = MediaState.Play;
                //gif.Visibility = System.Windows.Visibility.Visible;
                //gif.Source = new Uri("pack://application:,,,/Images/landmine-sprite.gif");
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
    }
}
