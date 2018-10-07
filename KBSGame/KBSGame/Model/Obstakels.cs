﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace KBSGame.GameObjects
{
    class Obstakels
    {
        List<Obstakel> obstakels = new List<Obstakel>();
        List<string> type = new List<string>();
        public static List<string> waardes = new List<string>();
        Canvas Canvas;
       
        public Obstakels(int aantalBoom, int aantalBom, Canvas canvas)
        {
            Canvas = canvas;
            for(int i = 0; i < aantalBoom; i++)
            {
                type.Add("Boom");
            }
            for (int i=0; i< aantalBom; i++)
            {
                type.Add("Bom");
            }
            for(int i = 0; i < type.Count; i++)
            {
                Obstakel o = new Obstakel(type[i]);
                obstakels.Add(o);
                Thread.Sleep(25);
            }
            for(int i = 0; i < obstakels.Count; i++)
            {
                canvas.Children.Add(obstakels[i].image);
            }
        }

        public void Reset()
        {
            for (int i = 0; i < obstakels.Count; i++)
            {
                Canvas.Children.Remove(obstakels[i].image);
            }
        }
    }
}
