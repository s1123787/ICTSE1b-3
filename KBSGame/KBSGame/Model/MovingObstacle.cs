﻿ using System;
using KBSGame.Model;
using KBSGame.GameObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace KBSGame.Model
{
    class MovingObstacle : Obstakel
    {
        public MediaElement gif;
        protected int MovingStepSize = 50;
        public int MovingX { get; private set; }
        public int MovingY { get; private set; }

        public MovingObstacle()
        {
            image = new Image
            {
                Width = 50,
                Height = 50
            };
            gif = new MediaElement
            {
                Width = 50,
                Height = 50,
                LoadedBehavior = MediaState.Play,
                Visibility = System.Windows.Visibility.Visible
            };

            base.AssignPosition("m");

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += MoveObstakelRandom;
            timer.Start();

            image.Width = 50;
            image.Height = 50;

            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/moving-sprite.png");

            myBitmapImage.DecodePixelWidth = 50;
            myBitmapImage.EndInit();

            image.Source = myBitmapImage;
            Canvas.SetZIndex(image, 2);
        }

        public void MoveObstakelRandom(object sender, EventArgs e)
        {
            //get current position x
            int x = (int)Canvas.GetLeft(image);
            int y = (int)Canvas.GetTop(image);
            Random random = new Random();
            int rand = random.Next(0, 4);
            switch (rand)
            {
                case 0:
                    if(CheckGridAvailability(x, y + MovingStepSize))
                    {
                        MoveObstakelDown();
                    }
                    break;
                case 1:
                    if (CheckGridAvailability(x + MovingStepSize, y))
                    {
                        MoveObstakelRight();
                    }
                    break;
                case 2:
                    if (CheckGridAvailability(x, y - MovingStepSize))
                    {
                        MoveObstakelUp();
                    }
                    break;
                case 3:
                    if (CheckGridAvailability(x - MovingStepSize, y))
                    {
                        MoveObstakelLeft();
                    }
                    break;
            }
        }

        public void MoveObstakelRight()
        {
            //get current position x
            int x = (int)Canvas.GetLeft(image);
            int y = (int)Canvas.GetTop(image);

            //Right screen boundry
            if (x == 750)
            {
                return;
            }
            else if ((y == 550) && (x == 700)) // End point boundry
            {
                return;
            }
            else
            {
                Canvas.SetLeft(image, x += MovingStepSize);
            }
        }


        public void MoveObstakelLeft()
        {
            //get current position
            int x = (int)Canvas.GetLeft(image);
            int y = (int)Canvas.GetTop(image);

            //Left screen boundry
            if (x == 0)
            {
                return;
            }
            else if ((y == 0) && (x == 50)) //Start point boundry 
            {
                return;
            }
            else
            {
                Canvas.SetLeft(image, x -= MovingStepSize);
            }
        }

        public void MoveObstakelDown()
        {
            int x = (int)Canvas.GetLeft(image);
            int y = (int)Canvas.GetTop(image);

            // Bottom boundry 
            if (y == 550)
            {
                return;
            }
            else if ((x == 750) && (y == 500)) // End point boundry
            {
                return;
            }
            else
            {
                Canvas.SetTop(image, y += MovingStepSize);

            }
        }

        public void MoveObstakelUp()
        {
            int x = (int)Canvas.GetLeft(image);
            int y = (int)Canvas.GetTop(image);

            // Top boundry 
            if (y == 0)
            {
                return;
            }
            else if ((x == 0) && (y == 50)) // Start point boundry
            {
                return;
            }
            else
            {
                Canvas.SetTop(image, y -= MovingStepSize);
            }
        }

        public bool CheckGridAvailability(int x, int y)
        {
            string XYString = x.ToString() + y.ToString();

            foreach (string waarde in Obstakels.waardes)
            {
                //Check if next grid contains an tree of moving obstakel
                if(waarde.Contains($"{XYString}t") || waarde.Contains($"{XYString}m"))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
