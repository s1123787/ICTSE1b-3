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
    public class PauseOverlay
    {
        private MainWindow MainWindow;
        private Canvas GameCanvas;
        private Rectangle background;
        private Image pauseSprite;
        private Button restart, menu, resume;
        private Game game;

        public PauseOverlay(MainWindow mw, Canvas canvas, Game g)
        {
            game = g;
            MainWindow = mw;
            GameCanvas = canvas;
            background = new Rectangle
            {
                Width = 400,
                Height = 250,
                Fill = Brushes.DimGray,
                Opacity = 0.90
            };
            pauseSprite = new Image
            {
                Width = 125,
                Height = 45,
                Name = "PauseSprite"
        };

            pauseSprite.Source = new BitmapImage(new Uri("pack://application:,,,/Images/pause-sprite.png"));

            //Create new button template for restart button
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

            restart.Click += Restart_Click;

            //Create new button template for menu button
            ControlTemplate menuButtonTemplate = new ControlTemplate(typeof(Button));
            var menuButtonImage = new FrameworkElementFactory(typeof(Image));
            menuButtonImage.SetValue(Image.SourceProperty, new BitmapImage(new Uri("pack://application:,,,/Images/menu-button.png", UriKind.RelativeOrAbsolute)));
            menuButtonTemplate.VisualTree = menuButtonImage;

            //Create new button to go to the game menu
            menu = new Button
            {
                Width = 125,
                Height = 45,
                Name = "menuButton",
                Template = menuButtonTemplate
            };

            menu.Click += Menu_Click;

            ControlTemplate resumeButtonTemplate = new ControlTemplate(typeof(Button));
            var resumeButtonImage = new FrameworkElementFactory(typeof(Image));
            resumeButtonImage.SetValue(Image.SourceProperty, new BitmapImage(new Uri("pack://application:,,,/Images/menu-button.png", UriKind.RelativeOrAbsolute)));
            resumeButtonTemplate.VisualTree = resumeButtonImage;

            resume = new Button
            {
                Width = 125,
                Height = 45,
                Name = "resumeButton",
                Template = resumeButtonTemplate
            };

            resume.Click += Resume_Click;

            //Adding to canvas
            Canvas.SetTop(pauseSprite, 150);
            Canvas.SetLeft(pauseSprite, 333);
            GameCanvas.Children.Add(pauseSprite);
            Panel.SetZIndex(pauseSprite, 99);
            Canvas.SetTop(background, 140);
            Canvas.SetLeft(background, 200);
            GameCanvas.Children.Add(background);
            Canvas.SetTop(resume, 190);
            Canvas.SetLeft(resume, 333);
            GameCanvas.Children.Add(resume);
            Canvas.SetTop(restart, 240);
            Canvas.SetLeft(restart, 333);
            GameCanvas.Children.Add(restart);
            Canvas.SetTop(menu, 290);
            Canvas.SetLeft(menu, 333);
            GameCanvas.Children.Add(menu);
        }

        public void continueGame()
        {
            removeObjects();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            //Method to reset the game
            game.PlayAgain();
            //Clean up victory overlay
            removeObjects();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            //Closes the application
            Application.Current.Shutdown();
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            game.playing = true;
            game.GameTimer.Herstart();
            game.FreezePlayer = false;
            removeObjects();
        }
        private void removeObjects()
        {
            GameCanvas.Children.Remove(background);
            GameCanvas.Children.Remove(restart);
            GameCanvas.Children.Remove(menu);
            GameCanvas.Children.Remove(pauseSprite);
            GameCanvas.Children.Remove(resume);
        }
    }
}
