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
        public static List<string> waardes = new List<string>();
        Canvas Canvas;

        public Obstakels(int aantalBoom, int aantalBom, int aantalMoving, int aantalCoin, Canvas canvas, Game game)
        {
            //add the amount of trees to canvas
            for (int i = 0; i < aantalBoom; i++)
            {
                Tree t = new Tree();
                obstakels.Add(t);
                canvas.Children.Add(t.image);
                //use of thread sleep because of the random generator
                Thread.Sleep(25);
            }

            //generate amount of bombs but don't put it on the screen because it is a land mine
            for (int i = 0; i < aantalBom; i++)
            {
                Bomb b = new Bomb();
                obstakels.Add(b);
                //canvas.Children.Add(b.image);
                Thread.Sleep(25);
            }
            //add the amount of moving obstacles to canvas
            for (int i = 0; i < aantalMoving; i++)
            {
                MovingObstacle mo = new MovingObstacle(game);
                obstakels.Add(mo);
                canvas.Children.Add(mo.image);
                Thread.Sleep(25);
            }
            //add the amount of coins to canvas
            for(int i = 0; i < aantalCoin; i++)
            {
                Coin c = new Coin();
                obstakels.Add(c);
                canvas.Children.Add(c.image);
                Thread.Sleep(25);
            }            

            Canvas = canvas;

        }

        //reset all the obstacles that are placed on canvas
        public void Reset()
        {
            for (int i = 0; i < obstakels.Count; i++)
            {
                //remove obstacles from canvas
                Canvas.Children.Remove(obstakels[i].image);
                //remove all of the data in waardes so it is empty
                waardes.Clear();
            }
        }
    }
}
