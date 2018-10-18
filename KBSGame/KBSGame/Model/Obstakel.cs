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
            //generate a random number from 0 to 750 and 0 to 550 with steps of 50
            x = random.Next(0, 15) * 50;
            y = random.Next(0, 11) * 50;
            //check if the x and y are not to close to end and start point and check if it isn't already taken
            while ((x <= 100 && y <= 100) || (x >= 650 && y >= 450) || Obstakels.waardes.Contains($"{x}{y}b") || Obstakels.waardes.Contains($"{x}{y}t") || Obstakels.waardes.Contains($"{x}{y}m") || Obstakels.waardes.Contains($"{x}{y}c"))
            {
                x = random.Next(0, 15) * 50;
                y = random.Next(0, 11) * 50;
            }
            //add the position to the list where all obstacles are placed
            Obstakels.waardes.Add($"{x}{y}{soort}");

            //add the obstacle to the canvas with the generated positions
            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
        }        
    }
}
