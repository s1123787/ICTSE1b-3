using KBSGame.GameObjects;
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
        public event EndPointReached endPointReached;
        public event WalkedOverBomb walkedOverBomb;
        public event CollectCoin collectCoin;
        //private Ellipse player = new Ellipse();
        private Image player;
        public static double x = 5;
        public static double y = 5;
        private int StepSize = 50;
        private Canvas gameCanvas;
        private Game game;
        private bool hits = false;

        //only for testing
        public Ellipse Eplayer { get; set; }


        public Player(Canvas GameCanvas, Game game)
        {
            gameCanvas = GameCanvas;
            this.game = game;
            player = new Image
            {
                Width = 40,
                Height = 40
            };
            BitmapImage myBitmapImage = new BitmapImage(new Uri("pack://application:,,,/Images/player.png"));

            player.Source = myBitmapImage;
            
            Canvas.SetLeft(player, 5);
            Canvas.SetTop(player, 5);
            Canvas.SetZIndex(player, 1);
            gameCanvas.Children.Add(player);

            //testing
            //Eplayer = player;
        }

        public void MoveRight()
        {
            //get current position x
            x = Canvas.GetLeft(player);
            y = Canvas.GetTop(player);

            if (Obstakels.waardes.Contains($"{x + 45}{y - 5}b"))
            {
                OnPlayerWalkedOverBomb(x + 45, y - 5, x + 50, y);
                Canvas.SetLeft(player, x += StepSize);
                return;
            }
            else if (Obstakels.waardes.Contains($"{x + 45}{y - 5}c"))
            {
                OnPlayerCollectCoin(x + 45, y - 5, x + 50, y);
                Canvas.SetLeft(player, x += StepSize);
                return;
            }
            else if (Obstakels.waardes.Contains($"{x + 45}{y - 5}t") || x == 755) //contains a tree
            {
                return;
            }
            else if (Obstakels.waardes.Contains($"{x + 45}{y - 5}m") && game.GameLost == false) //contains moving obstakel
            {
                if(hits == false)
                {
                    hits = true;
                    game.GameOver();
                    Canvas.SetLeft(player, x += (StepSize * 2));
                    return;
                }
            }
            else
            {
                Canvas.SetLeft(player, x += StepSize);
            }
            if (CheckEndPoint())
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
            if (Obstakels.waardes.Contains($"{x - 55}{y - 5}b"))
            {
                OnPlayerWalkedOverBomb(x - 55, y - 5, x - 50, y);
                Canvas.SetLeft(player, x -= StepSize);
                return;
            }
            else if (Obstakels.waardes.Contains($"{x - 55}{y - 5}c"))
            {
                OnPlayerCollectCoin(x - 55, y - 5, x - 50, y);
                Canvas.SetLeft(player, x -= StepSize);
                return;
            }
            else if (Obstakels.waardes.Contains($"{x - 55}{y - 5}t") || x == 5)
            {
                return;
            }
            else if (Obstakels.waardes.Contains($"{x - 55}{y - 5}m") && game.GameLost == false) //contains moving obstakel
            {
                if (hits == false)
                {
                    hits = true;
                    game.GameOver();
                    Canvas.SetLeft(player, x -= (StepSize * 2));
                    return;
                }
            }
            else
            {
                //set new position 
                Canvas.SetLeft(player, x -= StepSize);
            }
            if (CheckEndPoint())
            {
                OnEndPointReached();
                return;
            }
        }

        public void MoveDown()
        {
            y = Canvas.GetTop(player);
            x = Canvas.GetLeft(player);            
            if (Obstakels.waardes.Contains($"{x - 5}{y + 45}b"))
            {
                OnPlayerWalkedOverBomb(x - 5, y + 45, x, y + 50);
                Canvas.SetTop(player, y += StepSize);
                return;
            }
            else if (Obstakels.waardes.Contains($"{x - 5}{y + 45}c"))
            {
                OnPlayerCollectCoin(x - 5, y + 45, x, y + 50);
                Canvas.SetTop(player, y += StepSize);
                return;
            }
            else if (Obstakels.waardes.Contains($"{x - 5}{y + 45}t") || y == 555)
            {
                return;
            }
            else if (Obstakels.waardes.Contains($"{x - 5}{y + 45}m") && game.GameLost == false) //contains moving obstakel
            {
                if (hits == false)
                {
                    hits = true;
                    game.GameOver();
                    Canvas.SetTop(player, y += (StepSize * 2));
                    return;
                }
            }
            else
            {
                Canvas.SetTop(player, y += StepSize);
            }
            if (CheckEndPoint())
            {
                OnEndPointReached();
                return;
            }
        }

        public void MoveUp()
        {
            y = Canvas.GetTop(player);
            x = Canvas.GetLeft(player);            
            if (Obstakels.waardes.Contains($"{x - 5}{y - 55}b"))
            {
                OnPlayerWalkedOverBomb(x - 5, y - 55, x, y - 50);
                Canvas.SetTop(player, y -= StepSize);
                return;
            }
            else if (Obstakels.waardes.Contains($"{x - 5}{y - 55}c"))
            {
                OnPlayerCollectCoin(x - 5, y - 55, x, y - 50);
                Canvas.SetTop(player, y -= StepSize);
                return;
            }
            else if (Obstakels.waardes.Contains($"{x - 5}{y - 55}t") || y == 5)
            {
                return;
            }
            else if (Obstakels.waardes.Contains($"{x - 5}{y - 55}m") && game.GameLost == false) //contains moving obstakel
            {
                if (hits == false)
                {
                    hits = true;
                    game.GameOver();
                    Canvas.SetTop(player, y -= (StepSize * 2));
                    return;
                }
            }
            else
            {
                Canvas.SetTop(player, y -= StepSize);
            }
            if (CheckEndPoint())
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
