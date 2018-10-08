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
            int rand = random.Next(0, 3);
            switch(rand)
            {
                case 0:
                    Console.WriteLine("Moving Left");
                    MoveObstakelLeft();
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
                    Console.WriteLine("Moving Down");
                    MoveObstakelDown();
                    break;
                //case 4:
                //    MoveObstakelLeftUp();
                //    break;
                //case 5:
                //    MoveObstakelLeftDown();
                //    break;
                //case 6:
                //    MoveObstakelLeftUp();
                //    break;
                //case 7:
                //    MoveObstakelRightUp();
                //    break;
            }
        }
        
        public void MoveObstakelRight()
        {
            //get current position x
            int x = (int)Canvas.GetLeft(image);

            /* Right screen boundry */
            if (x == 750)
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
            //get current position x
            int x = (int) Canvas.GetLeft(image);

            Console.WriteLine($"Curr X:{x}");

            /* Left screen boundry */
            if (x == 0)
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
            int y = (int)Canvas.GetTop(image);

            /* Bottom boundry */
            if (y == 550)
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
            int y = (int)Canvas.GetTop(image);

            /* Top boundry */
            if (y == 0)
            {
                return;
            }
            else
            {
                Canvas.SetTop(image, y -= MovingStepSize);
            }
        }

        //public void MoveObstakelRightUp()
        //{
        //    //get current position x & y
        //    double x = Canvas.GetLeft(image);
        //    double y = Canvas.GetTop(image);

        //    if (x == 5.00 || y == 5.00)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        //set new position 
        //        Canvas.SetLeft(image, x += MovingStepSize);
        //        Canvas.SetTop(image, y -= MovingStepSize);
        //    }
        //}

        //public void MoveObstakelRightDown()
        //{
        //    //get current position x & y
        //    double x = Canvas.GetLeft(image);
        //    double y = Canvas.GetTop(image);

        //    if (x == 5.00 || y == 5.00)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        //set new position 
        //        Canvas.SetLeft(image, x += MovingStepSize);
        //        Canvas.SetTop(image, y += MovingStepSize);
        //    }
        //}

        //public void MoveObstakelLeftUp()
        //{
        //    //get current position x & y
        //    double x = Canvas.GetLeft(image);
        //    double y = Canvas.GetTop(image);

        //    if (x == 5.00 || y == 5.00)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        //set new position 
        //        Canvas.SetLeft(image, x -= MovingStepSize);
        //        Canvas.SetTop(image, y -= MovingStepSize);
        //    }
        //}

        //public void MoveObstakelLeftDown()
        //{
        //    //get current position x & y
        //    double x = Canvas.GetLeft(image);
        //    double y = Canvas.GetTop(image);

        //    if (x == 5.00 || y == 5.00)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        //set new position 
        //        Canvas.SetLeft(image, x -= MovingStepSize);
        //        Canvas.SetTop(image, y += MovingStepSize);
        //    }
        //}  
    }
}
