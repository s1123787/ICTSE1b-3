using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace KBSGame
{
    class Game
    {
        public StartPoint StartPoint { get; set; }
        public EndPoint EndPoint { get; set; }

        public Game(Canvas canvas)
        {
            StartPoint = new StartPoint(canvas);
            EndPoint = new EndPoint(canvas);
        }

        public void CheckEndPoint(double x, double y)
        {
            if(x == 755 && y == 555)
            {
                
            }
        }
    }
}
