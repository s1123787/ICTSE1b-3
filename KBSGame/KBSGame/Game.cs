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
        public Muur Muur { get; set; }
        public Val Val { get; set; }

        public Game(Canvas canvas)
        {
            StartPoint = new StartPoint(canvas);
            EndPoint = new EndPoint(canvas);

            //Randomizet Muur-objecten over (tot zover alleen de bovenste) lijn, 50% kans.
            Random rng = new Random();
            int rnd;
                for (int i = 1; i <= 15; i++)
                {
                    rnd = Randomizer.Random(1, 4);
                    if (rnd == 2)
                    {
                        double x = 50 * i;
                        double y = 0;
                        Muur = new Muur(canvas, x, y);
                    }
                }
        }
        private void TimerOnTick(object sender, EventArgs eventArgs)
        {

        }
    }
}
