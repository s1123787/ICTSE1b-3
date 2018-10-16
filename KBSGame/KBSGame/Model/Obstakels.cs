using KBSGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace KBSGame.GameObjects
{
    public class Obstakels
    {
        public List<Obstakel> obstakels = new List<Obstakel>();
        List<string> type = new List<string>();
        public static List<string> waardes = new List<string>();
        Canvas Canvas;

        public Obstakels(int aantalBoom, int aantalBom, int aantalMoving, int aantalCoin, Canvas canvas, Game game)
        {
         
            for (int i = 0; i < aantalBoom; i++)
            {
                Tree t = new Tree();
                obstakels.Add(t);
                canvas.Children.Add(t.image);
                Thread.Sleep(25);
            }

            for (int i = 0; i < aantalBom; i++)
            {
                Bomb b = new Bomb();
                obstakels.Add(b);
                //canvas.Children.Add(b.image);
                Thread.Sleep(25);
            }
            for (int i = 0; i < aantalMoving; i++)
            {
                MovingObstacle mo = new MovingObstacle(game);
                obstakels.Add(mo);
                canvas.Children.Add(mo.image);
                Thread.Sleep(25);
            }
            for(int i = 0; i < aantalCoin; i++)
            {
                Coin c = new Coin();
                obstakels.Add(c);
                canvas.Children.Add(c.image);
                Thread.Sleep(25);
            }            

            Canvas = canvas;

        }

        public void Reset()
        {
            for (int i = 0; i < obstakels.Count; i++)
            {
                Canvas.Children.Remove(obstakels[i].image);
                waardes.Clear();
            }
        }
    }
}
