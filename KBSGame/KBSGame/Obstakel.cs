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
        private int x;
        private int y;
        private int width = 50;
        private int height = 50;
        public Rectangle rect = new Rectangle();
        private string Type;


        public Obstakel(Canvas canvas, String z)
        {
            SetType(z);
            rect.Width = 50;
            rect.Height = 50;
            Random random = new Random();
            x = random.Next(0, 16);
            x = x * 50;
            Canvas.SetLeft(rect, x);
            y = random.Next(0, 8);
            y = y * 50;
            Canvas.SetTop(rect, y);
            //canvas.Children.Add(rect);
            //rect = null;
        }
        public void SetType(string z)
        {
            if (z == "Bom")
            {
                this.Type = "Bom";
                rect.Fill = Brushes.DarkRed;
                return;
            }
            else if (z == "Boom")
            {
                this.Type = "Boom";
                rect.Fill = Brushes.ForestGreen;
                return;
            }
            return;
        }
    }
}
