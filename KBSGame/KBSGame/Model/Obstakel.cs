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
            image = new Image
            {
                Width = 50,
                Height = 50
            };
            gif = new MediaElement
            {
                Width = 50,
                Height = 50,
                LoadedBehavior = MediaState.Play,
                Visibility = System.Windows.Visibility.Visible
            };
            SetType(z);
            AssignPosition();
        }

        public void SetType(string z)
        {
            if (z == "Bom")
            {
                this.Type = "Bom";

                image.Source = new BitmapImage(new Uri("pack://application:,,,/Images/landmine-sprite.png"));
                
                //gif.Source = new Uri("pack://application:,,,/Images/landmine-sprite.gif");
            }
            else if (z == "Boom")
            {
                this.Type = "Boom";
                
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Images/tree-sprite.png"));
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
