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
        public MediaElement gif;
        protected int MovingStepSize = 50;
        public int MovingX { get; private set; }
        public int MovingY { get; private set; }
        private Game game;
        private bool hits = false;
        public int x;
        public int y;

        public MovingObstacle(Game game, bool debug = false)
        {
            this.game = game;

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


            if(!debug)
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
            if(game.GameLost == false && game.GameWon == false && game.playing == true)
            {
                //get current position x
                x = (int)Canvas.GetLeft(image);
                y = (int)Canvas.GetTop(image);

                Random random = new Random();
                int rand = random.Next(0, 4);
                switch (rand)
                {
                    case 0:
                        if(CheckGridAvailability(x, y + MovingStepSize, x, y))
                        {
                            MoveObstakelDown();
                        }
                        break;
                    case 1:
                        if (CheckGridAvailability(x + MovingStepSize, y, x, y))
                        {
                            MoveObstakelRight();
                        }
                        break;
                    case 2:
                        if (CheckGridAvailability(x, y - MovingStepSize, x, y))
                        {
                            MoveObstakelUp();
                        }
                        break;
                    case 3:
                        if (CheckGridAvailability(x - MovingStepSize, y, x, y))
                        {
                            MoveObstakelLeft();
                        }
                        break;
                }
            }
        }

        public void MoveObstakelRight()
        {
            if (x != null && y != null)
            { 
                //get current position x
                int x = (int)Canvas.GetLeft(image);
                int y = (int)Canvas.GetTop(image);
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
            if (x != null && y != null)
            {
                //get current position x
                int x = (int)Canvas.GetLeft(image);
                int y = (int)Canvas.GetTop(image);
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
            if (x != null && y != null)
            {
                //get current position x
                int x = (int)Canvas.GetLeft(image);
                int y = (int)Canvas.GetTop(image);
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
            if (x != null && y != null)
            {
                //get current position x
                int x = (int)Canvas.GetLeft(image);
                int y = (int)Canvas.GetTop(image);
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

        public bool CheckGridAvailability(int x, int y, int currentX, int currentY)
        {
            string XYString = x.ToString() + y.ToString();

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
