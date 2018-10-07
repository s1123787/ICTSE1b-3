using KBSGame.GameObjects;
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
using System.Windows.Threading;

namespace KBSGame
{
    class Game
    {
        //DispatcherTimer countdownTimer;
        //TimeSpan playTime;

        public StartPoint StartPoint { get; set; }
        public EndPoint EndPoint { get; set; }
        public Player Player { get; private set; }
        public Obstakels obstakels { get; set; }
        public MainWindow mainWindow { get; set; }
        public Canvas GameCanvas { get; private set; }
        private Image GameOverSprite { get; set; }
        private Button opnieuw, afsluiten;
        private int aantalBoom;
        private int aantalBom;

        public Game(MainWindow mw, Canvas canvas, int aantalBoom, int aantalBom)
        {
            StartPoint = new StartPoint(canvas);
            EndPoint = new EndPoint(canvas);
            Player = new Player(canvas);
            obstakels = new Obstakels(aantalBoom, aantalBom, canvas);
            mainWindow = mw;
            GameCanvas = canvas;
            GameOverSprite = new Image
            {
                Width = 400,
                Height = 200,
                Name = "GameOverSprite"
            };
            this.aantalBoom = aantalBoom;
            this.aantalBom = aantalBom;
        }

        public void GameOver()
        {
            //TimerLabel.Content = "00:00";
            //Create audio player to play sound effect on gameover
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

                //Create new button to restart the game
                opnieuw = new Button
                {
                    Width = 125,
                    Height = 45,
                    Name = "opnieuwButton"
                };
                //Set the button image
                opnieuw.Content = new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Images/opnieuw-button.png")),
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.Fill,
                    Height = 45,
                    Width = 125
                };
                //Subscribe the button to the method it needs to run
                opnieuw.Click += Opnieuw_Click;

                //Create new button to exit the game
                afsluiten = new Button
                {
                    Width = 125,
                    Height = 45,
                    Name = "afsluitButton"
                };
                //Set the button image
                afsluiten.Content = new Image
                 {
                     Source = new BitmapImage(new Uri("pack://application:,,,/Images/afsluit-button.png")),
                     VerticalAlignment = VerticalAlignment.Center,
                     Stretch = Stretch.Fill,
                     Height = 45,
                     Width = 125
                 };
                //Subscribe the button to the method it needs to run
                afsluiten.Click += Afsluiten_Click;

                //Add everything to the screen
                Canvas.SetTop(GameOverSprite, 140);
                Canvas.SetLeft(GameOverSprite, 201);
                GameCanvas.Children.Add(GameOverSprite);
                Canvas.SetTop(opnieuw, 300);
                Canvas.SetLeft(opnieuw, 251);
                GameCanvas.Children.Add(opnieuw);
                Canvas.SetTop(afsluiten, 300);
                Canvas.SetLeft(afsluiten, 424);
                GameCanvas.Children.Add(afsluiten);
            };

            backgroundWorker.RunWorkerAsync();
        }

        //Actions to perform when afsluiten button is clicked
        private void Afsluiten_Click(object sender, RoutedEventArgs e)
        {
            //Shutsdown the application
            Application.Current.Shutdown();
        }

        //Actions to perform when opnieuw button is clicked
        private void Opnieuw_Click(object sender, RoutedEventArgs e)
        {
            //Method to reset the game
            mainWindow.PlayAgain();
            //Clean up gameover overlay
            GameCanvas.Children.Remove(GameOverSprite);
            GameCanvas.Children.Remove(opnieuw);
            GameCanvas.Children.Remove(afsluiten);
        }

        public void Restart()
        {
            Player.Reset();
            obstakels.Reset();
            obstakels = new Obstakels(aantalBoom, aantalBom, GameCanvas);
        }

    }
}
