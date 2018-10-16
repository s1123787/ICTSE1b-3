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
        public DispatcherTimer timer = new DispatcherTimer();
        public DispatcherTimer timer2 = new DispatcherTimer();
        private int Seconde { get; set; }
        

        public delegate void ActivateEndPoint(object souce, EventArgs e);
        public event ActivateEndPoint activeEndPoint;

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
        public Model.Timer GameTimer { get; set; }
        public int CollectedCoins { get; set; }

        GameOverOverlay gameOverOverlay;

        public bool GameWon;
        public bool GameLost;
        public bool playing;
        public bool EndPointIsShown = false;
        
        public double testx;
        public double testy;

        public PauseOverlay pauseOverlay;
        private bool overBom = true, overBom2 = true;
        public bool pauseActivated = false;

        public Rectangle r, r2;
        Image i, i2;
      
        TextBlock pause = new TextBlock();

        public Image image, explosion;

        public Game(MainWindow mw, Canvas canvas, int aantalBoom, int aantalBom, int aantalMoving, int aantalCoin, int s)
        {
            playing = true;
            Seconde = s;
            StartPoint = new StartPoint(canvas);
            
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
            mw.esqKeyIsPressed += OnEsqKeyIsPressed;
            mw.enterKeyIsPressed += OnEnterKeyIsPressed;
            activeEndPoint += OnActivateEndpoint;
            CollectedCoins = 0;
        }

        public void OnPlayerCollectCoin(object source, GameEventArgs e)
        {
            
            double x = e.x;
            double y = e.y;
            i2 = new Image
            {
                Height = 48,
                Width = 48
            };
            i2.Source = new BitmapImage(new Uri("pack://application:,,,/Images/soul-sand48x48.png"));
            Canvas.SetLeft(i2, x + 1);
            Canvas.SetTop(i2, y + 1);
            Canvas.SetZIndex(i2, 0);
            Obstakels.waardes.Remove($"{x}{y}c");
            GameCanvas.Children.Add(i2);

            //coin counter
            CollectedCoins++;
            mainWindow.CoinCounter.Content = CollectedCoins;
            
            if(CollectedCoins == 5)
            {
                activeEndPoint?.Invoke(this, EventArgs.Empty);
            }

        }

        public void OnActivateEndpoint(object source, EventArgs e)
        {
            EndPoint = new EndPoint(GameCanvas);
            EndPointIsShown = true;
            Player.endPointReached += OnEndPointReached;
        }

        public void OnPlayerWalkedOverBomb(object source, GameEventArgs e)
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
                    i = new Image
                    {
                        Height = 40,
                        Width = 40
                    };
                    i.Source = new BitmapImage(new Uri("pack://application:,,,/Images/soul-sand40x40.png"));
                    Canvas.SetLeft(i, x + 5);
                    Canvas.SetTop(i, y + 5);
                    Canvas.SetZIndex(i, 0);
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

        public void Timer_Tick(object sender, EventArgs e)
        {
            if (playing && overBom2) 
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
                Canvas.SetZIndex(explosion, 5);
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

        public void Timer2_Tick(object sender, EventArgs e)
        {
            GameCanvas.Children.Remove(explosion);
            GameCanvas.Children.Add(i);
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
                pauseActivated = true;
            }
        }

        public void OnEnterKeyIsPressed(object source, EventArgs e)
        {
            if (!playing && pauseActivated == true) {
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

            //set collected coins 0
            CollectedCoins = 0;
            mainWindow.CoinCounter.Content = CollectedCoins;

            //disable endpoint             
            if (EndPointIsShown)
            {                
                EndPoint.Delete(GameCanvas);
                EndPoint = null;
                Player.endPointReached -= OnEndPointReached;
                EndPointIsShown = false;
            }
        }

        public void Restart()
        {
            Player.Reset();
            obstakels.Reset();
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, aantalCoin, GameCanvas, this);
            FreezePlayer = false;
            gameOverOverlay = null;
            timer.Tick -= Timer_Tick;
            timer2.Tick -= Timer2_Tick;
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
            GameWon = true;
            GameWonOverlay gameWonOverlay = new GameWonOverlay(mainWindow, GameCanvas, this);
            playing = false;
            GameTimer.Pauze();
        }
    }
}
