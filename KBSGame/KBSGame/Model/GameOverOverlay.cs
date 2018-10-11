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
    public class GameOverOverlay : Overlay
    {
        private Image GameOverSprite;
        private Button again;

        public GameOverOverlay(MainWindow mw, Canvas canvas, Game g) : base(mw, canvas, g)
        {
            GameOverSprite = new Image
            {
                Width = 400,
                Height = 200,
                Name = "GameOverSprite"
            };

            //TimerLabel.Content = "00:00";
            //Create audio player to play sound effect on game over
            SoundPlayer audio = new SoundPlayer(Properties.Resources.game_over_sound_effect);
            audio.Play();

            //create background worker to sync audio playback with overlay
            var backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += (s, e) =>
            {
                Thread.Sleep(150);
            };

            backgroundWorker.RunWorkerCompleted += (s, e) =>
            {
                //Set source of GameOverSprite
                GameOverSprite.Source = new BitmapImage(new Uri("pack://application:,,,/Images/game-over-sprite.png"));

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
               

                //Add everything to the screen
                Canvas.SetTop(background, 140);
                Canvas.SetLeft(background, 201);
                GameCanvas.Children.Add(background);
                Panel.SetZIndex(background, 99);
                Canvas.SetTop(GameOverSprite, 140);
                Canvas.SetLeft(GameOverSprite, 201);
                GameCanvas.Children.Add(GameOverSprite);
                Panel.SetZIndex(GameOverSprite, 99);
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

        //Actions to perform when play again button is clicked
        private void Again_Click(object sender, RoutedEventArgs e)
        {
            //Method to reset the game
            game.PlayAgain();
            //Clean up game over overlay
            removeObjects();
        }
        public void removeObjects()
        {
            base.RemoveObjects();
            GameCanvas.Children.Remove(GameOverSprite);
            GameCanvas.Children.Remove(again);
        }
    }
}
