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
    public class GameWonOverlay
    {
        private MainWindow MainWindow;
        private Canvas GameCanvas;
        private Image VictorySprite;
        private Rectangle background;
        private Button again, menu;
        private Game game;

        public GameWonOverlay(MainWindow mw, Canvas canvas, Game g)
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
            VictorySprite = new Image
            {
                Width = 400,
                Height = 200,
                Name = "VictorySprite"
            };

            //TimerLabel.Content = "00:00";
            //Create audio player to play sound effect on victory
            SoundPlayer audio = new SoundPlayer(Properties.Resources.game_won_sound_effect);
            audio.Play();

            //create background worker to sync audio playback with overlay
            var backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += (s, e) =>
            {
                Thread.Sleep(150);
            };

            backgroundWorker.RunWorkerCompleted += (s, e) =>
            {
                //Set source of VictorySprite
                VictorySprite.Source = new BitmapImage(new Uri("pack://application:,,,/Images/victory-sprite.png"));

                //Create new button template for restart button
                ControlTemplate againButtonTemplate = new ControlTemplate(typeof(Button));
                var againButtonImage = new FrameworkElementFactory(typeof(Image));
                againButtonImage.SetValue(Image.SourceProperty, new BitmapImage(new Uri("pack://application:,,,/Images/play-again-button.png", UriKind.RelativeOrAbsolute)));
                againButtonTemplate.VisualTree = againButtonImage;

                //Create new button to restart the game
                again = new Button
                {
                    Width = 125,
                    Height = 45,
                    Name = "againButton",
                    Template = againButtonTemplate
                };

                //Subscribe the button to the method it needs to run
                again.Click += Again_Click;

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

                //Subscribe the button to the method it needs to run
                menu.Click += Menu_Click;

                //Add everything to the screen
                Canvas.SetTop(background, 140);
                Canvas.SetLeft(background, 201);
                GameCanvas.Children.Add(background);
                Panel.SetZIndex(background, 99);
                Canvas.SetTop(VictorySprite, 140);
                Canvas.SetLeft(VictorySprite, 201);
                GameCanvas.Children.Add(VictorySprite);
                Panel.SetZIndex(VictorySprite, 99);
                Canvas.SetTop(again, 300);
                Canvas.SetLeft(again, 251);
                GameCanvas.Children.Add(again);
                Panel.SetZIndex(again, 99);
                Canvas.SetTop(menu, 300);
                Canvas.SetLeft(menu, 424);
                GameCanvas.Children.Add(menu);
                Panel.SetZIndex(menu, 99);
            };

            backgroundWorker.RunWorkerAsync();
        }

        //Actions to perform when menu button is clicked
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            //Shutsdown the application
            Application.Current.Shutdown();
        }

        //Actions to perform when play again button is clicked
        private void Again_Click(object sender, RoutedEventArgs e)
        {
            //Method to reset the game
            game.PlayAgain();
            //Clean up victory overlay
            GameCanvas.Children.Remove(background);
            GameCanvas.Children.Remove(VictorySprite);
            GameCanvas.Children.Remove(again);
            GameCanvas.Children.Remove(menu);
        }
    }
}
