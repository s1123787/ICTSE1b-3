using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;



namespace KBSGame
{
    class Player
    {

        private Ellipse player = new Ellipse();
        private double x = 5;
        private double y = 5;
        private int StepSize = 25;
        private Canvas gameCanvas;

        public Player(Canvas GameCanvas)
        {
            gameCanvas = GameCanvas;
            player.Fill = Brushes.Red;
            player.Width = 40;
            player.Height = 40;
            Canvas.SetLeft(player, 5);
            Canvas.SetTop(player, 5);
            GameCanvas.Children.Add(player);
        }

        public void MoveRight()
        {
            //get current position x
            x = Canvas.GetLeft(player);

            if (x == 755)
            {
                return;
            }
            else
            {
                //remove current postion
                gameCanvas.Children.Remove(player);

                //set new position 
                Canvas.SetLeft(player, x += StepSize);
                gameCanvas.Children.Add(player);
            }
        }

        public void MoveLeft()
        {
            //get current position x
            x = Canvas.GetLeft(player);

            if (x == 5)
            {
                return;
            }
            else
            {
                //remove current position 
                gameCanvas.Children.Remove(player);

                //set new position 
                Canvas.SetLeft(player, x -= StepSize);
                gameCanvas.Children.Add(player);
            }
        }

        public void MoveDown()
        {
            y = Canvas.GetTop(player);

            if (y == 555)
            {
                return;
            }
            else
            {
                gameCanvas.Children.Remove(player);

                Canvas.SetTop(player, y += StepSize);
                gameCanvas.Children.Add(player);
            }
        }

        public void MoveUp()
        {
            y = Canvas.GetTop(player);

            if (y == 5)
            {
                return;
            }
            else
            {
                gameCanvas.Children.Remove(player);

                Canvas.SetTop(player, y -= StepSize);
                gameCanvas.Children.Add(player);
            }
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
    }
}
