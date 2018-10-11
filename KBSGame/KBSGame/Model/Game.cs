using KBSGame.GameObjects;
using KBSGame.Model;
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
using System.Windows.Threading;

namespace KBSGame
{
    public class Game
    {
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        private int Seconde { get; set; }

        private StartPoint StartPoint { get; set; }
        private EndPoint EndPoint { get; set; }
        public Player Player { get; set; }
        public Obstakels obstakels { get; set; }
        public MainWindow mainWindow { get; set; }
        public Canvas GameCanvas { get; private set; }
        public Boolean FreezePlayer { get; set; }
        private int aantalBoom;
        private int aantalBom;
        private int aantalMoving;
        private int aantalCoin;
        public KBSGame.Model.Timer GameTimer { get; set; }
        private int CollectedCoins = 0 ;

        GameOverOverlay gameOverOverlay;

        public bool GameWon;
        public bool GameLost;
        public bool playing;
        
        private double testx;
        private double testy;

        private PauseOverlay pauseOverlay;
        private bool overBom = true, overBom2 = true;

        Rectangle r;
        TextBlock pause = new TextBlock();

        Image image, explosion;

        public Game(MainWindow mw, Canvas canvas, int aantalBoom, int aantalBom, int aantalMoving, int aantalCoin, int s)
        {
            playing = true;
            Seconde = s;
            StartPoint = new StartPoint(canvas);
            EndPoint = new EndPoint(canvas);
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, aantalCoin, canvas, this);

            Player = new Player(canvas, this);
            mainWindow = mw;
            GameCanvas = canvas;
            this.aantalBoom = aantalBoom;
            this.aantalBom = aantalBom;
            this.aantalMoving = aantalMoving;
            this.aantalCoin = aantalCoin;
            Player.walkedOverBomb += OnPlayerWalkedOverBomb; //hier subscribed de methode OnPlayerWalkedOverBomb op de event walkedOverBomb 
            Player.collectCoin += OnPlayerCollectCoin;
            GameTimer = new Model.Timer(Seconde, this, mw); //hier wordt de timer aangemaakt die de meegegeven seconden gebruikt            
            GameTimer.tijdIsOp += OnPlayerTijdIsOp; //hier subscribe je op de event van timer
            Player.endPointReached += OnEndPointReached;
            mw.esqKeyIsPressed += OnEsqKeyIsPressed;
            mw.enterKeyIsPressed += OnEnterKeyIsPressed;
        }

        public void OnPlayerCollectCoin(object source, GameOverEventArgs e)
        {
            double x = e.x;
            double y = e.y;
            r = new Rectangle();
            r.Fill = Brushes.LightGray;
            r.Height = 48;
            r.Width = 48;
            Canvas.SetLeft(r, x + 1);
            Canvas.SetTop(r, y + 1);
            Canvas.SetZIndex(r, 0);
            Obstakels.waardes.Remove($"{x}{y}c");
            GameCanvas.Children.Add(r);

            //coin counter
            CollectedCoins++;
            mainWindow.CoinCounter.Content = CollectedCoins;

        }

        public void OnPlayerWalkedOverBomb(object source, GameOverEventArgs e)
        {
            //this if statement because other wise it don't work
            if (overBom)
            {
                overBom = false;
                Player.walkedOverBomb -= OnPlayerTijdIsOp;
                double x = e.x;
                double y = e.y;
                testx = e.bomx;
                testy = e.bomy;
                #region
                r = new Rectangle();
                r.Fill = Brushes.LightGray;
                r.Height = 40;
                r.Width = 40;
                Canvas.SetLeft(r, x + 5);
                Canvas.SetTop(r, y + 5);
                Canvas.SetZIndex(r, 0);
                #endregion
                Obstakels.waardes.Remove($"{x}{y}b");
                #region
                image = new Image();
                image.Width = 50;
                image.Height = 50;

                BitmapImage myBitmapImage = new BitmapImage();

                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/landmine-sprite.png");

                myBitmapImage.DecodePixelWidth = 50;
                myBitmapImage.EndInit();

                image.Source = myBitmapImage;
                Canvas.SetLeft(image, x);
                Canvas.SetTop(image, y);
                #endregion
                GameCanvas.Children.Add(image);
                timer.Interval = new TimeSpan(0, 0, 0, 1);
                timer.Tick += Timer_Tick;
                if (timer != null)
                {
                    timer.Start();
                }

            }
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {            
            if (overBom2)
            {
                overBom2 = false;
                explosion = new Image();
                explosion.Width = 150;
                explosion.Height = 150;

                BitmapImage myBitmapImage = new BitmapImage();

                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/explosion-sprite.png");

                myBitmapImage.DecodePixelWidth = 50;
                myBitmapImage.EndInit();

                explosion.Source = myBitmapImage;
                Canvas.SetLeft(explosion, testx - 50);
                Canvas.SetTop(explosion, testy - 50);
                GameCanvas.Children.Add(explosion);
                timer.Tick -= Timer_Tick;
                timer.Stop();
                if ((Player.x == testx || Player.x == testx + 50 || Player.x == testx - 50) && (Player.y == testy || Player.y == testy + 50 || Player.y == testy - 50) && GameLost == false && GameWon == false)
                {
                    GameOver();
                }
                timer2.Interval = new TimeSpan(0, 0, 0, 1);
                timer2.Tick += Timer2_Tick; ;
                if (timer2 != null)
                {
                    timer2.Start();
                }
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            GameCanvas.Children.Remove(explosion);
            GameCanvas.Children.Add(r);
            overBom = true;
            overBom2 = true;
            timer2.Tick -= Timer2_Tick;
            if ((Player.x == testx || Player.x == testx + 50 || Player.x == testx - 50) && (Player.y == testy || Player.y == testy + 50 || Player.y == testy - 50) && GameLost == false && GameWon == false)
            {
                GameOver();
            }
        }

        public void OnPlayerTijdIsOp(object source, EventArgs e)
        {
            GameOver();          
        }

        public void OnEndPointReached(object source, EventArgs e)
        {
            GameVictory();
        }

        public void OnEsqKeyIsPressed(object source, EventArgs e)
        {
            if (playing)
            {
                pauseOverlay = new PauseOverlay(mainWindow, GameCanvas, this);
                GameTimer.Pauze();
                FreezePlayer = true;
                playing = false;
            }
        }

        public void OnEnterKeyIsPressed(object source, EventArgs e)
        {
            if (!playing)
            {
                playing = true;
                pauseOverlay.continueGame();
                GameTimer.Herstart();
                FreezePlayer = false;
            }
        }

        public void PlayAgain()
        {
            Player.Reset();
            obstakels.Reset();
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, aantalCoin, GameCanvas, this);
            
            FreezePlayer = false;
            GameTimer.Restart();
            playing = true;
            GameWon = false;
            GameLost = false;
            CollectedCoins = 0;
            mainWindow.CoinCounter.Content = CollectedCoins;
        }

        public void Restart()
        {
            Player.Reset();
            obstakels.Reset();
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, aantalCoin, GameCanvas, this);
            FreezePlayer = false;
            gameOverOverlay = null;
        }

        public void GameOver()
        {
            FreezePlayer = true;
            GameLost = true;
            gameOverOverlay = new GameOverOverlay(mainWindow, GameCanvas, this);
            playing = false;
            GameTimer.countdownTimer.Stop();
        }

        public void GameVictory()
        {
            FreezePlayer = true;
            GameLost = false;
            GameWonOverlay gameWonOverlay = new GameWonOverlay(mainWindow, GameCanvas, this);
            playing = false;
            GameTimer.Pauze();
        }
    }
}
