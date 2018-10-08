using KBSGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KBSGame
{
    public class Obstakel
    {
        public int x { get; private set; }
        public int y { get; private set; }
        private int width = 50;
        private int height = 50;
        //public Rectangle rect;
        public Image image;
        private string Type;
        Random random = new Random();
        private int MovingStepSize = 50;
        public int MovingX { get; private set; }
        public int MovingY { get; private set; }

        public Obstakel(String z)
        {
            image = new Image();
            //rect = new Rectangle();
            SetType(z);
            //rect.Width = 50;
            //rect.Height = 50;
            ////Canvas.SetLeft(rect, 800);
            ////Canvas.SetTop(rect, 600);

            /* Initial assigning of obstakel positions */
            AssignPosition();

            ////canvas.Children.Add(rect);
            ////rect = null;
        }
        public void SetType(string z)
        {
            if (z == "Bom")
            {
                this.Type = "Bom";

                
                image.Width = 50;
                image.Height = 50;

                BitmapImage myBitmapImage = new BitmapImage();

                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/tnt-sprite.png");

                myBitmapImage.DecodePixelWidth = 50;
                myBitmapImage.EndInit();

                image.Source = myBitmapImage;

                //rect.Fill = Brushes.DarkRed;
                //rect.Opacity = 0.5;
                
            }
            else if (z == "Boom")
            {
                this.Type = "Boom";

                image.Width = 50;
                image.Height = 50;

                BitmapImage myBitmapImage = new BitmapImage();

                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/tree-sprite.png");

                myBitmapImage.DecodePixelWidth = 50;
                myBitmapImage.EndInit();

                image.Source = myBitmapImage;

                //rect.Fill = Brushes.ForestGreen;
                //rect.Opacity = 0.5;
                
            }
            else if (z == "moving")
            {
                this.Type = "moving";

                image.Width = 50;
                image.Height = 50;

                BitmapImage myBitmapImage = new BitmapImage();

                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/moving-sprite.png");

                myBitmapImage.DecodePixelWidth = 50;
                myBitmapImage.EndInit();

                image.Source = myBitmapImage;

                /* Start dispatch for movement */
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += MoveObstakelRandom;
                timer.Start();
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

            Canvas.SetLeft(image, x);
            Canvas.SetTop(image, y);
        }

        /* Moving obstakel */
        public void MoveObstakelRandom(object sender, EventArgs e)
        {
            int rand = random.Next(0, 4);
            switch(rand)
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

            /* Right screen boundry */
            if (x == 750)
            {
                return;
            }
            else if((y == 550) && (x == 700)) /* End point boundry */
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
            int x = (int) Canvas.GetLeft(image);
            int y = (int) Canvas.GetTop(image);

            /* Left screen boundry */
            if (x == 0)
            {
                return;
            }
            else if((y == 0) && (x == 50)) /* Start point boundry */
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

            /* Bottom boundry */
            if (y == 550)
            {
                return;
            }
            else if((x == 750) && (y == 500))
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

            /* Top boundry */
            if (y == 0)
            {
                return;
            }
            else if((x == 0) && (y == 50)) /* Start point boundry */
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
