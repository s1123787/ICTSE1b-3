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
        public delegate void ResumeIsPressed(object source, EventArgs e);
        public event ResumeIsPressed resumeIsPressed;
        public delegate void RestartIspressed(object source, EventArgs e);
        public event RestartIspressed restartIsPressed;
        public Image pauseSprite;
        public Button restart, resume;
        public int pauseSpriteX, pauseSpriteY, resumeX, resumeY, restartX, restartY, menuX, menuY;

        public PauseOverlay()
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

            //Create new button to resume the game
            resume = new Button
            {
                Width = 125,
                Height = 45,
                Name = "resumeButton",
                Template = resumeButtonTemplate
            };
            //Subscribe the resume button to the method it needs to run
            resume.Click += Resume_Click;

            //Create new button template for menu button
            ControlTemplate menuButtonTemplate = new ControlTemplate(typeof(Button));
            var menuButtonImage = new FrameworkElementFactory(typeof(Image));
            menuButtonImage.SetValue(Image.SourceProperty, new BitmapImage(new Uri("pack://application:,,,/Images/menu-button-left.png", UriKind.RelativeOrAbsolute)));
            menuButtonTemplate.VisualTree = menuButtonImage;

            //Create new button to go to the game menu
            menu = new Button
            {
                Width = 125,
                Height = 45,
                Name = "menuButton",
                Template = menuButtonTemplate
            };
            //Subscribe the menu button to the method it needs to run
            menu.Click += Menu_Click;

            //Set coardinate values
            pauseSpriteX = 300;
            pauseSpriteY = 120;
            resumeX = 333;
            resumeY = 210;
            restartX = 333;
            restartY = 260;
            menuX = 333;
            menuY = 310;
        }

        //Actions to perform when restart button is clicked
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OnRestartIsPressed();
            //Method to reset the game
            //game.PlayAgain();
            //Clean up victory overlay
            //game.RemovePauseOverlay(this, game.GameCanvas);
        }

        //Actions to perform when resume button is clicked
        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            OnResumeIsPressed();
        }

        //Actions to perform when menu button is clicked
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            OnPressedOnMenu();
        }

        protected virtual void OnResumeIsPressed()
        {
            resumeIsPressed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnRestartIsPressed()
        {
            restartIsPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
