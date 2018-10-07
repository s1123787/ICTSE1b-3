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
        public MainWindow mw { get; set; }
        public Canvas GameCanvas { get; private set; }
        public Image GameOverSprite;
        Button opnieuw, afsluiten;
        private int aantalBoom;
        private int aantalBom;


        public Game(MainWindow mw, Canvas canvas, int aantalBoom, int aantalBom)
        {
            StartPoint = new StartPoint(canvas);
            EndPoint = new EndPoint(canvas);
            Player = new Player(canvas);
            obstakels = new Obstakels(aantalBoom, aantalBom, canvas);
            GameCanvas = canvas;
            GameOverSprite = new Image();
            this.mw = mw;
            this.aantalBoom = aantalBoom;
            this.aantalBom = aantalBom;
        }

        public void GameOver()
        {
            //TimerLabel.Content = "00:00";
            SoundPlayer audio = new SoundPlayer(Properties.Resources.game_over_sound_effect);
            audio.Play();

            //create background worker to sync audio playback with text
            var backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += (s, e) =>
            {
                Thread.Sleep(150);
            };

            backgroundWorker.RunWorkerCompleted += (s, e) =>
            {
                #region gameover sprite
                GameOverSprite.Width = 400;
                GameOverSprite.Height = 200;

                BitmapImage myBitmapImage = new BitmapImage();

                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/game-over-sprite.png");

                myBitmapImage.DecodePixelWidth = 400;
                myBitmapImage.EndInit();

                GameOverSprite.Source = myBitmapImage;
                GameOverSprite.Name = "GameOverSprite";

                Canvas.SetTop(GameOverSprite, 140);
                Canvas.SetLeft(GameOverSprite, 201);
                GameCanvas.Children.Add(GameOverSprite);
                #endregion
                #region buttons
                opnieuw = new Button();
                opnieuw.Content = "Opnieuw";
                opnieuw.Name = "opnieuwButton";
                opnieuw.Width = 125;
                opnieuw.Click += Opnieuw_Click;
                Canvas.SetTop(opnieuw, 300);
                Canvas.SetLeft(opnieuw, 251);
                GameCanvas.Children.Add(opnieuw);

                afsluiten = new Button();
                afsluiten.Content = "Afsluiten";
                afsluiten.Name = "afsluitButton";
                afsluiten.Width = 125;
                afsluiten.Click += Afsluiten_Click;
                Canvas.SetTop(afsluiten, 300);
                Canvas.SetLeft(afsluiten, 424);
                GameCanvas.Children.Add(afsluiten);
                #endregion
            };

            backgroundWorker.RunWorkerAsync();
        }

        private void Afsluiten_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Opnieuw_Click(object sender, RoutedEventArgs e)
        {
            mw.PlayAgain();
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
