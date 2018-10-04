using KBSGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace KBSGame
{
    class Game
    {
        public StartPoint StartPoint { get; set; }
        public EndPoint EndPoint { get; set; }
 
        public Player Player { get; private set; }
        public Obstakels obstakels { get; set; }


        public Game(Canvas canvas, int aantalBoom, int aantalBom)
        {
            StartPoint = new StartPoint(canvas);
            EndPoint = new EndPoint(canvas);
            Player = new Player(canvas);
            obstakels = new Obstakels(aantalBoom, aantalBom, canvas);

        }


    }
}
