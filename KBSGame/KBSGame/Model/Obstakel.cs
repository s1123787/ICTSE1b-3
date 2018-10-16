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
using System.Windows.Threading;

namespace KBSGame
{
    public class Obstakel
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public Image image;        
        Random random = new Random();        
        
        public void AssignPosition(string soort)
        {  
            x = random.Next(0, 15) * 50;
                y = random.Next(0, 11) * 50;
            while ((x <= 100 && y <= 100) || (x >= 650 && y >= 450) || Obstakels.waardes.Contains($"{x}{y}b") || Obstakels.waardes.Contains($"{x}{y}t") || Obstakels.waardes.Contains($"{x}{y}m") || Obstakels.waardes.Contains($"{x}{y}c"))
            {
                x = random.Next(0, 15) * 50;
                y = random.Next(0, 11) * 50;
            }
            Obstakels.waardes.Add($"{x}{y}{soort}");

            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
        }        
    }
}
