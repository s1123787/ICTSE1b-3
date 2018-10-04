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

        public Game(Canvas canvas)
        {
            StartPoint = new StartPoint(canvas);
            EndPoint = new EndPoint(canvas);
            Player = new Player(canvas);

            //place obstacles 
            List<Obstakel> ObstacleList = new List<Obstakel>();
            List<String> Typelijst = new List<String>();
            Typelijst.Add("Boom");
            Typelijst.Add("Bom");
            Typelijst.Add("Boom");
            Typelijst.Add("Bom");
            Typelijst.Add("Boom");
            Typelijst.Add("Boom");
            Typelijst.Add("Bom");
            Typelijst.Add("Boom");
            Typelijst.Add("Bom");
            Typelijst.Add("Boom");

            for (int i = 0; i < 5; i++)
            {
                ObstacleList.Add(new Obstakel(canvas, Typelijst[i]));
                Thread.Sleep(25);
            }

            for (int i = 0; i < ObstacleList.Count; i++)
            {
                canvas.Children.Add(ObstacleList[i].rect);
            }
        }


    }
}
