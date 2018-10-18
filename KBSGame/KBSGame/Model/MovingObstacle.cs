 using System;
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
    public class MovingObstacle : Obstakel
    {
        protected int MovingStepSize = 50;
        public int MovingX { get; private set; }
        public int MovingY { get; private set; }
        private Game game;
        private bool hits = false;
        public int OldX;
        public int OldY;
        public int x = -1;
        public int y = -1;

        public DispatcherTimer timer { get; set; }

        public MovingObstacle(Game game, bool debug = false, int StaticX = 0, int StaticY = 0)
        {
            this.game = game;

            image = new Image
            {
                Width = 50,
                Height = 50
            };

            //Only for random map
            if (!debug)
            {
                base.AssignPosition("m");
            }
            else //For unittest & XML map
            {
                base.AssignStaticPosition("m", StaticX, StaticY);
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += MoveObstakelRandom;
            timer.Start();

            image.Width = 50;
            image.Height = 50;

            BitmapImage myBitmapImage = new BitmapImage(new Uri("pack://application:,,,/Images/moving-sprite.png"));

            image.Source = myBitmapImage;
            Canvas.SetZIndex(image, 2);
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public void MoveObstakelRandom(object sender, EventArgs e)
        {
            //Moving obstakle can move
            if(game.GameLost == false && game.GameWon == false && game.playing == true)
            {
                int x = 0;
                int y = 0;

                //get current position x
                x = (int)Canvas.GetLeft(image);
                y = (int)Canvas.GetTop(image);

                Random random = new Random();
                int rand = random.Next(0, 4);
                switch (rand)
                {
                    //down
                    case 0:
                        if(CheckGridAvailability(x, y + MovingStepSize, x, y))
                        {
                            MoveObstakelDown();
                        }
                        break;
                    //Right
                    case 1:
                        if (CheckGridAvailability(x + MovingStepSize, y, x, y))
                        {
                            MoveObstakelRight();
                        }
                        break;
                    //Up
                    case 2:
                        if (CheckGridAvailability(x, y - MovingStepSize, x, y))
                        {
                            MoveObstakelUp();
                        }
                        break;
                    //Left
                    case 3:
                        if (CheckGridAvailability(x - MovingStepSize, y, x, y))
                        {
                            MoveObstakelLeft();
                        }
                        break;
                }
            }
        }

        #region MoveObstakel

        public void MoveObstakelRight()
        {
            if (x != -1 && y != -1)
            { 
                //get current position x
                x = (int)Canvas.GetLeft(image);
                y = (int)Canvas.GetTop(image);
            }

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
                //Create tmp list & update old XY to new values
                var tmpList = Obstakels.waardes.Select(s => s.Replace($"{x}{y}m", $"{x+MovingStepSize}{y}m")).ToList();
                Obstakels.waardes = tmpList;

                Canvas.SetLeft(image, x += MovingStepSize);
            }
        }


        public void MoveObstakelLeft()
        {
            if (x != -1 && y != -1)
            {
                //get current position x
                x = (int)Canvas.GetLeft(image);
                y = (int)Canvas.GetTop(image);
            }

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
                //Create tmp list & update old XY to new values
                var tmpList = Obstakels.waardes.Select(s => s.Replace($"{x}{y}m", $"{x - MovingStepSize}{y}m")).ToList();
                Obstakels.waardes = tmpList;

                Canvas.SetLeft(image, x -= MovingStepSize);
            }
        }

        public void MoveObstakelDown()
        {
            if (x != -1 && y != -1)
            {
                //get current position x
                x = (int)Canvas.GetLeft(image);
                y = (int)Canvas.GetTop(image);
            }

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
                //Create tmp list & update old XY to new values
                var tmpList = Obstakels.waardes.Select(s => s.Replace($"{x}{y}m", $"{x}{y + MovingStepSize}m")).ToList();
                Obstakels.waardes = tmpList;

                Canvas.SetTop(image, y += MovingStepSize);

            }
        }

        public void MoveObstakelUp()
        {
            if (x != -1 && y != -1)
            {
                //get current position x
                x = (int)Canvas.GetLeft(image);
                y = (int)Canvas.GetTop(image);
            }

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
                //Create tmp list & update old XY to new values
                var tmpList = Obstakels.waardes.Select(s => s.Replace($"{x}{y}m", $"{x}{y - MovingStepSize}m")).ToList();
                Obstakels.waardes = tmpList;

                Canvas.SetTop(image, y -= MovingStepSize);
            }
        }
        #endregion 

        public bool CheckGridAvailability(int x, int y, int currentX, int currentY)
        {
            string XYString = x.ToString() + y.ToString();

            //set X & Y for Player
            int playerX = (int)Player.x - 5;
            int playerY = (int)Player.y - 5;
            //check if Player is on Moving obstakel
            if(playerX == currentX && playerY == currentY && hits == false)
            {
                game.GameOver();
                hits = true;
            }

            //check if Moving obstakel hits player
            if (playerX == x && playerY == y && game.GameLost == false && hits == false)
            {
                game.GameOver();
                hits = true;
            }

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
