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
    class Obstakel
    {
        public int x { get; private set; }
        public int y { get; private set; }
        private int width = 50;
        private int height = 50;
        public Rectangle rect = new Rectangle();
        private string Type;


        public Obstakel(Canvas canvas, String z)
        {
            SetType(z);
            rect.Width = 50;
            rect.Height = 50;
            AssignPosition();
            //canvas.Children.Add(rect);
            //rect = null;
        }
        public void SetType(string z)
        {
            if (z == "Bom")
            {
                this.Type = "Bom";
                rect.Fill = Brushes.DarkRed;
                rect.Opacity = 0.5;
                return;
            }
            else if (z == "Boom")
            {
                this.Type = "Boom";
                rect.Fill = Brushes.ForestGreen;
                rect.Opacity = 0.5;
                return;
            }
            return;
        }

        public void AssignPosition()
        {
            Random random = new Random();
            while ((x <= 2 && y <= 2) || (x >= 13 && y >= 9))
            {
                x = random.Next(0, 16);
                y = random.Next(0, 12);
            }
            x = x * 50;
            y = y * 50;
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
        }
    }
}
