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
    class MovingObstacle : Obstakel
    {
        public MovingObstacle(string z) : base (z)
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
        }

        public void MoveObstakelRandom(object sender, EventArgs e)
        {
            Random random = new Random();
            int rand = random.Next(0, 4);
            switch (rand)
            {
                case 0:
                    Console.WriteLine("Moving Down");
                    MoveObstakelDown();
                    break;
                case 1:
                    Console.WriteLine("Moving Right");
                    MoveObstakelRight();
                    break;
                case 2:
                    Console.WriteLine("Moving Up");
                    MoveObstakelUp();
                    break;
                case 3:
                    Console.WriteLine("Moving Left");
                    MoveObstakelLeft();
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
            else if ((x == 750) && (y == 500))
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
    }
}
