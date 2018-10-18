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
        private bool randomLevel;
        
        public double bombx;
        public double bomby;

        public PauseOverlay pauseOverlay;
        private bool overBomb = true, overBomb2 = true;
        public bool pauseActivated = false;

        public Rectangle r, r2;
        Image i, i2;
      
        TextBlock pause = new TextBlock();

        public Image image, explosionImage;
        public BitmapImage myBitmapImage = new BitmapImage();

        private int aantal = 0;
        private bool explosion = false, explosionIsGoingToTakePlace = false;

        public Game(MainWindow mw, Canvas canvas, int aantalBoom, int aantalBom, int aantalMoving, int aantalCoin, int s, bool rl)
        {
            randomLevel = rl;
            playing = true;
            Seconde = s;
            StartPoint = new StartPoint(canvas);
            
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, aantalCoin, canvas, this, randomLevel);

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
            mw.escKeyIsPressed += OnEscKeyIsPressed;
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
            //check if there is any other bomb active
            if (overBomb)
            {
                explosionIsGoingToTakePlace = true; //to let know that a mine is going to explode
                overBomb = false;
                //initialize the position of player when is walked over bomb
                double x = e.x; 
                double y = e.y;
                //initialize the position of bomb when player walked on it
                bombx = e.bomx;
                bomby = e.bomy;
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
                Obstakels.waardes.Remove($"{x}{y}b"); //remove the bomb from list so it can't be activated again
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
                GameCanvas.Children.Add(image); //let the player know he stept on bomb to make bomb visible
                timer.Interval = new TimeSpan(0, 0, 0, 1);
                timer.Tick += Timer_Tick;
                if (timer != null && playing)
                {
                    timer.Start(); //start timer who's event is called in one second
                }
            }            
        }

        public void Timer_Tick(object sender, EventArgs e)
        {

            //this checked if there is any bomb active
            if (playing && overBomb2) 
            {
                overBomb2 = false;
                #region
                explosionImage = new Image();
                explosionImage.Width = 150;
                explosionImage.Height = 150;

                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri("pack://application:,,,/Images/explosion-sprite.png");

                myBitmapImage.DecodePixelWidth = 50;
                myBitmapImage.EndInit();

                explosionImage.Source = myBitmapImage;
                Canvas.SetLeft(explosionImage, bombx - 50);
                Canvas.SetTop(explosionImage, bomby - 50);
                Canvas.SetZIndex(explosionImage, 5);
                #endregion
                GameCanvas.Children.Add(explosionImage); //add the explosion to the canvas               
                explosion = true; //explosion is taking place
                explosionIsGoingToTakePlace = false; //explosion has already taken place

                timer.Tick -= Timer_Tick;
                timer.Stop();
                //check if player is near the exploding bomb to check if it game over
                if ((Player.x == bombx || Player.x == bombx + 50 || Player.x == bombx - 50) && (Player.y == bomby || Player.y == bomby + 50 || Player.y == bomby - 50) && GameLost == false && GameWon == false)
                {
                    GameOver();
                }
                else //when player is not game over it will check it for the next second
                {
                    timer2.Interval = new TimeSpan(0, 0, 0, 0, 10);
                    timer2.Tick += Timer2_Tick; //this event will check every 10 miliseconds
                    if (timer2 != null)
                    {
                        timer2.Start();
                    }
                }
            }
        }

        public void Timer2_Tick(object sender, EventArgs e)
        {
            //check if second is over
            if (aantal < 100)
            {
                aantal++;              
                if ((Player.x == bombx || Player.x == bombx + 50 || Player.x == bombx - 50) && (Player.y == bomby || Player.y == bomby + 50 || Player.y == bomby - 50) && GameLost == false && GameWon == false)
                {
                    GameOver();
                    timer2.Tick -= Timer2_Tick;
                }
            } else if (aantal >= 100) //second is over and player isn't hit by explosion so it can move on and explosion will be deleted
            {
                ExplosionEnded();
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

        public void OnEscKeyIsPressed(object source, EventArgs e)
        {
            //Check if the game is active
            if (playing)
            {
                pauseOverlay = new PauseOverlay(mainWindow, GameCanvas, this);
                GameTimer.Pause();
                FreezePlayer = true;
                playing = false;
                pauseActivated = true;
            }
        }

        public void OnEnterKeyIsPressed(object source, EventArgs e)
        {
            //Check if the game is not active and the pause screen is activated
            if (!playing && pauseActivated == true) {
                playing = true;
                pauseOverlay.continueGame();
                GameTimer.Resume();
                FreezePlayer = false;
            }
        }

        public void PlayAgain()
        {
            Player.Reset();
            obstakels.Reset();

            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, aantalCoin, GameCanvas, this, randomLevel);
            
            FreezePlayer = false;
            GameTimer.Restart();
            playing = true;
            GameWon = false;
            GameLost = false;

            //check if explosion is on so it can be deleted from screen
            if (explosion)
            {
                ExplosionEnded();
            }

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

       public void GameOver()
        {
            FreezePlayer = true;
            GameLost = true;
            GameWon = false;
            gameOverOverlay = new GameOverOverlay(mainWindow, GameCanvas, this);
            playing = false;
            GameTimer.countdownTimer.Stop();
            //check if explosion will take place so it can be deleted and not shown on screen
            if (explosionIsGoingToTakePlace)
            {
                ExplosionEnded();
            }
        }

        public void GameVictory()
        {
            FreezePlayer = true;
            GameLost = false;
            GameWon = true;
            GameWonOverlay gameWonOverlay = new GameWonOverlay(mainWindow, GameCanvas, this);
            playing = false;
            GameTimer.Pause();
        }

        public void ExplosionEnded()
        {
            GameCanvas.Children.Remove(explosionImage);
            GameCanvas.Children.Add(i); //this will ad the background over the bomb
            explosion = false; 
            overBomb = true;
            overBomb2 = true;
            timer2.Tick -= Timer2_Tick; //delete subscriber
            aantal = 0; //set the amount to check if second is over to zero
            timer.Tick -= Timer_Tick; //delete the subscriber
            explosionIsGoingToTakePlace = false;
        }
    }
}
