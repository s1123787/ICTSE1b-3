﻿using KBSGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KBSGame
{
    public class Obstakel
    {
        public int x { get; private set; }
        public int y { get; private set; }
        private int width = 50;
        private int height = 50;
        public Rectangle rect;
        private string Type;
        Random random = new Random();

        public Obstakel(String z)
        {
            rect = new Rectangle();
            SetType(z);
            rect.Width = 50;
            rect.Height = 50;
            //Canvas.SetLeft(rect, 800);
            //Canvas.SetTop(rect, 600);
            AssignPosition();
            //canvas.Children.Add(rect);
            //rect = null;
        }
        public void SetType(string z)
        {
            if (z == "Bom")
            {
                this.Type = "Bom";
                rect.Fill = Brushes.DarkRed;
                rect.Opacity = 0.5;
                
            }
            else if (z == "Boom")
            {
                this.Type = "Boom";
                rect.Fill = Brushes.ForestGreen;
                rect.Opacity = 0.5;
                
            }
            
        }

        public void AssignPosition()
        {            
            bool niet = true;
            while ((x <= 100 && y <= 100) || (x >= 650 && y >= 450) || Obstakels.waardes.Contains($"{x}{y}"))
            {
                x = random.Next(0, 15) * 50;
                y = random.Next(0, 11) * 50;
            }
            Obstakels.waardes.Add($"{x}{y}");

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
        }
    }
}