﻿using KBSGame.Model;
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
        List<Obstakel> obstakels = new List<Obstakel>();
        List<string> type = new List<string>();
        public static List<string> waardes = new List<string>();
        Canvas Canvas;
        Game game;
       
        public Obstakels(int aantalBoom, int aantalBom, int aantalMoving, Canvas canvas, Game game)
        {
            
            for (int i = 0; i < aantalBoom; i++)
            {
                Tree t = new Tree();
                obstakels.Add(t);
                Thread.Sleep(25);
            }

            for (int i = 0; i < aantalBom; i++)
            {
                Bomb b = new Bomb();
                obstakels.Add(b);
                Thread.Sleep(25);
            }
            for (int i = 0; i < aantalMoving; i++)
            {
                MovingObstacle mo = new MovingObstacle(game);
                obstakels.Add(mo);
                Thread.Sleep(25);
            }


            for (int i = 0; i < obstakels.Count; i++)
            {
                canvas.Children.Add(obstakels[i].image);
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
