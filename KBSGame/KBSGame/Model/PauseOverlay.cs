using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KBSGame.Model
{
    public class PauseOverlay : Overlay
    {
        private Image pauseSprite;
        private Button restart, resume;

        public PauseOverlay(Game g) : base(g)
        {
            //Create new image for sprite
            pauseSprite = new Image
            {
                Width = 200,
                Height = 100,
                Name = "PauseSprite"
            };
            //Set source of pauseSprite
            pauseSprite.Source = new BitmapImage(new Uri("pack://application:,,,/Images/pause-sprite.png"));

            ControlTemplate restartButtonTemplate = new ControlTemplate(typeof(Button));
            var restartButtonImage = new FrameworkElementFactory(typeof(Image));
            restartButtonImage.SetValue(Image.SourceProperty, new BitmapImage(new Uri("pack://application:,,,/Images/restart-button.png", UriKind.RelativeOrAbsolute)));
            restartButtonTemplate.VisualTree = restartButtonImage;
            //Create new button to restart the game
            restart = new Button
            {
                Width = 125,
                Height = 45,
                Name = "restartButton",
                Template = restartButtonTemplate
            };
            //Subscribe the restart button to the method it needs to run
            restart.Click += Restart_Click;

            //Create button template for restart button
            ControlTemplate resumeButtonTemplate = new ControlTemplate(typeof(Button));
            var resumeButtonImage = new FrameworkElementFactory(typeof(Image));
            resumeButtonImage.SetValue(Image.SourceProperty, new BitmapImage(new Uri("pack://application:,,,/Images/resume-button.png", UriKind.RelativeOrAbsolute)));
            resumeButtonTemplate.VisualTree = resumeButtonImage;

            //Create new button to resume game

            resume = new Button
            {
                Width = 125,
                Height = 45,
                Name = "resumeButton",
                Template = resumeButtonTemplate
            };
            //Subscribe the resume button to the method it needs to run
            resume.Click += Resume_Click;

            //Adding to canvas
            Canvas.SetTop(background, 140);
            Canvas.SetLeft(background, 200);
            GameCanvas.Children.Add(background);
            Panel.SetZIndex(background, 99);
            Canvas.SetTop(pauseSprite, 120);
            Canvas.SetLeft(pauseSprite, 300);
            GameCanvas.Children.Add(pauseSprite);
            Panel.SetZIndex(pauseSprite, 99);
            Canvas.SetTop(resume, 210);
            Canvas.SetLeft(resume, 333);
            GameCanvas.Children.Add(resume);
            Panel.SetZIndex(resume, 99);
            Canvas.SetTop(restart, 260);
            Canvas.SetLeft(restart, 333);
            GameCanvas.Children.Add(restart);
            Panel.SetZIndex(restart, 99);
            Canvas.SetTop(menu, 310);
            Canvas.SetLeft(menu, 333);
            GameCanvas.Children.Add(menu);
            Panel.SetZIndex(menu, 99);
        }

        //Method to resume playing
        public void continueGame()
        {
            removeObjects();
        }

        //Actions to perform when restart button is clicked
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            //Method to reset the game
            game.PlayAgain();
            //Clean up victory overlay
            removeObjects();
        }

        //Actions to perform when resume button is clicked
        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            game.playing = true;
            game.GameTimer.Resume();
            game.FreezePlayer = false;
            continueGame();
        }

        //Method to remove the overlay once a button is clicked
        public void removeObjects()
        {
            base.RemoveObjects();
            GameCanvas.Children.Remove(resume);
            GameCanvas.Children.Remove(pauseSprite);
            GameCanvas.Children.Remove(restart);
        }
    }
}
