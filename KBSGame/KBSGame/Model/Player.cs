﻿using KBSGame.GameObjects;
using KBSGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace KBSGame
{
    public class Player
    {
        public delegate void CollectCoin(object source, GameEventArgs e);
        public delegate void WalkedOverBomb(object source, GameEventArgs e);
        public delegate void EndPointReached(object source, EventArgs e);
        public delegate void WalkedOnMovingObstacle(object source, EventArgs e);

        public event EndPointReached endPointReached;
        public event WalkedOverBomb walkedOverBomb;
        public event CollectCoin collectCoin;
        public event WalkedOnMovingObstacle walkedOnMovingObstacle;
        //private Ellipse player = new Ellipse();
        public Image player;
        public static double x = 5;
        public static double y = 5;
        private int StepSize = 50;
        private bool hits = false;

        //only for testing
        public Ellipse Eplayer { get; set; }


        public Player()
        {
            player = new Image
            {
                Width = 40,
                Height = 40
            };
            BitmapImage bitmapImage = new BitmapImage(new Uri("pack://application:,,,/Images/player.png"));

            player.Source = bitmapImage;
            
            Canvas.SetLeft(player, 5);
            Canvas.SetTop(player, 5);
            Canvas.SetZIndex(player, 1);

           }

        public void MoveRight()
        {
            //get current position x
            x = Canvas.GetLeft(player);
            y = Canvas.GetTop(player);

            if (Obstacles.waardes.Contains($"{x + 45}{y - 5}b"))//contains bomb
            {
                OnPlayerWalkedOverBomb(x + 45, y - 5, x + 50, y);
                Canvas.SetLeft(player, x += StepSize);
                return;
            }
            else if (Obstacles.waardes.Contains($"{x + 45}{y - 5}c"))//contains coin
            {
                OnPlayerCollectCoin(x + 45, y - 5, x + 50, y);
                Canvas.SetLeft(player, x += StepSize);
                return;
            }
            else if (Obstacles.waardes.Contains($"{x + 45}{y - 5}t") || x == 755) //contains a tree
            {
                return;
            }
            else if (Obstacles.waardes.Contains($"{x + 45}{y - 5}m") && Game.GameLost == false) //contains moving obstakel
            {
                if(hits == false)
                {
                    hits = true;
                    OnWalkedOnMovingObstacle();
                    Canvas.SetLeft(player, x += (StepSize));
                    return;
                }
            }
            else //move player
            {
                Canvas.SetLeft(player, x += StepSize);
            }
            if (CheckEndPoint()) //check if endpoint is reached
            {
                OnEndPointReached();
                return;
            }
        }

        public void MoveLeft()
        {
            //get current position x
            x = Canvas.GetLeft(player);
            y = Canvas.GetTop(player);            
            if (Obstacles.waardes.Contains($"{x - 55}{y - 5}b"))//contains bomb
            {
                OnPlayerWalkedOverBomb(x - 55, y - 5, x - 50, y);
                Canvas.SetLeft(player, x -= StepSize);
                return;
            }
            else if (Obstacles.waardes.Contains($"{x - 55}{y - 5}c"))//contains coin
            {
                OnPlayerCollectCoin(x - 55, y - 5, x - 50, y);
                Canvas.SetLeft(player, x -= StepSize);
                return;
            }
            else if (Obstacles.waardes.Contains($"{x - 55}{y - 5}t") || x == 5)//contains a tree
            {
                return;
            }
            else if (Obstacles.waardes.Contains($"{x - 55}{y - 5}m") && Game.GameLost == false) //contains moving obstakel
            {
                if (hits == false)
                {
                    hits = true;
                    OnWalkedOnMovingObstacle();
                    Canvas.SetLeft(player, x -= (StepSize));
                    return;
                }
            }
            else //set new position
            {
                 Canvas.SetLeft(player, x -= StepSize);
            }
            if (CheckEndPoint()) //check if endpoint is reached
            {
                OnEndPointReached();
                return;
            }
        }

        public void MoveDown()
        {
            y = Canvas.GetTop(player);
            x = Canvas.GetLeft(player);            
            if (Obstacles.waardes.Contains($"{x - 5}{y + 45}b"))//contains bomb
            {
                OnPlayerWalkedOverBomb(x - 5, y + 45, x, y + 50);
                Canvas.SetTop(player, y += StepSize);
                return;
            }
            else if (Obstacles.waardes.Contains($"{x - 5}{y + 45}c"))//contains coin
            {
                OnPlayerCollectCoin(x - 5, y + 45, x, y + 50);
                Canvas.SetTop(player, y += StepSize);
                return;
            }
            else if (Obstacles.waardes.Contains($"{x - 5}{y + 45}t") || y == 555)//contains a tree
            {
                return;
            }
            else if (Obstacles.waardes.Contains($"{x - 5}{y + 45}m") && Game.GameLost == false) //contains moving obstakel
            {
                if (hits == false)
                {
                    hits = true;
                    OnWalkedOnMovingObstacle();
                    Canvas.SetTop(player, y += (StepSize));
                    return;
                }
            }
            else //move player
            {
                Canvas.SetTop(player, y += StepSize);
            }
            if (CheckEndPoint()) //check if endpoint is reached
            {
                OnEndPointReached();
                return;
            }
        }

        public void MoveUp()
        {
            y = Canvas.GetTop(player);
            x = Canvas.GetLeft(player);            
            if (Obstacles.waardes.Contains($"{x - 5}{y - 55}b"))//contains bomb
            {
                OnPlayerWalkedOverBomb(x - 5, y - 55, x, y - 50);
                Canvas.SetTop(player, y -= StepSize);
                return;
            }
            else if (Obstacles.waardes.Contains($"{x - 5}{y - 55}c"))//contains coin
            {
                OnPlayerCollectCoin(x - 5, y - 55, x, y - 50);
                Canvas.SetTop(player, y -= StepSize);
                return;
            }
            else if (Obstacles.waardes.Contains($"{x - 5}{y - 55}t") || y == 5)//contains a tree
            {
                return;
            }
            else if (Obstacles.waardes.Contains($"{x - 5}{y - 55}m") && Game.GameLost == false) //contains moving obstakel
            {
                if (hits == false)
                {
                    hits = true;
                    OnWalkedOnMovingObstacle();
                    Canvas.SetTop(player, y -= (StepSize));
                    return;
                }
            }
            else //move player
           {
                Canvas.SetTop(player, y -= StepSize);
            }
            if (CheckEndPoint()) //check if endpoint is reached
            {
                OnEndPointReached();
                return;
            }
        }

        protected virtual void OnPlayerCollectCoin(double xwaarde, double ywaarde, double coinx, double coiny)
        {
            GameEventArgs ge = new GameEventArgs(xwaarde, ywaarde, coinx, coiny);
            collectCoin?.Invoke(this, ge);

        }

        protected virtual void OnPlayerWalkedOverBomb(double xwaarde, double ywaarde, double bomx, double bomy)
        {
            GameEventArgs ge = new GameEventArgs(xwaarde, ywaarde, bomx, bomy);
            walkedOverBomb?.Invoke(this, ge);
        }

        protected virtual void OnEndPointReached()
        {
            endPointReached?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnWalkedOnMovingObstacle()
        {
            walkedOnMovingObstacle?.Invoke(this, EventArgs.Empty);
        }

        public Boolean CheckEndPoint()
        {
            x = Canvas.GetLeft(player);
            y = Canvas.GetTop(player);

            if (x == 755 && y == 555)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Reset()
        {
            Canvas.SetTop(player, 5);
            Canvas.SetLeft(player, 5);
            x = 5;
            y = 5;
            hits = false;
        }

        public string Position()
        {
            return $"{x}{y}";
        }


        
    }
}
