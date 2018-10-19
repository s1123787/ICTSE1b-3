using KBSGame.GameObjects;
using KBSGame.Model;
using KBSGame.View;
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
        #region 
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
        public bool explosion = false, explosionIsGoingToTakePlace = false;
       #endregion


        public Game(MainWindow mw, Canvas canvas, int aantalBoom, int aantalBom, int aantalMoving, int aantalCoin, int s, bool rl)
        {
            mainWindow = mw;
            GameCanvas = canvas;
            this.aantalBoom = aantalBoom;
            this.aantalBom = aantalBom;
            this.aantalMoving = aantalMoving;
            this.aantalCoin = aantalCoin;
            Seconde = s;
            randomLevel = rl;

            playing = true;
            StartPoint = new StartPoint();
            AddStartPoint(StartPoint, GameCanvas);

            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, aantalCoin, canvas, this, randomLevel);

            Player = new Player(canvas, this);
            Player.walkedOverBomb += OnPlayerWalkedOverBomb; //hier subscribed de methode OnPlayerWalkedOverBomb op de event walkedOverBomb 
            Player.collectCoin += OnPlayerCollectCoin;
            GameTimer = new Model.Timer(Seconde, this); //hier wordt de timer aangemaakt die de meegegeven seconden gebruikt            
            GameTimer.timeIsUp += OnPlayerTimeIsUp; //hier subscribe je op de event van timer
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

            //set coin counter
            CollectedCoins++;
            mainWindow.CoinCounter.Content = CollectedCoins;
            
            //invoke event when all coins are collected
            if(CollectedCoins == 5)
            {
                activeEndPoint?.Invoke(this, EventArgs.Empty);
            }

        }

        public void OnActivateEndpoint(object source, EventArgs e)
        {
            EndPoint = new EndPoint();
            AddEndPoint(EndPoint, GameCanvas);
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
            if (aantal < 25)
            {
                aantal++;              
                if ((Player.x == bombx || Player.x == bombx + 50 || Player.x == bombx - 50) && (Player.y == bomby || Player.y == bomby + 50 || Player.y == bomby - 50) && GameLost == false && GameWon == false)
                {
                    GameOver();
                    timer2.Tick -= Timer2_Tick;
                }
            } else if (aantal >= 25) //second is over and player isn't hit by explosion so it can move on and explosion will be deleted
            {
                ExplosionEnded();
            }
        }

        public void OnPlayerTimeIsUp(object source, EventArgs e)
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
                pauseOverlay = new PauseOverlay(this);
                AddPauseOverlay(pauseOverlay, GameCanvas);
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
                RemovePauseOverlay(pauseOverlay, GameCanvas);
                GameTimer.Resume();
                FreezePlayer = false;
            }
        }

        public void PlayAgain()
        {
            // reset player and obstakels
            Player.Reset();
            obstakels.Reset();
            //create new obastakels
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, aantalCoin, GameCanvas, this, randomLevel);
            //allow player to move again, restart timer
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
                RemoveEndPoint(EndPoint, GameCanvas);
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
            gameOverOverlay = new GameOverOverlay(this);
            AddGameOverOverlay(gameOverOverlay, GameCanvas);
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
            GameWonOverlay gameWonOverlay = new GameWonOverlay(this);
            AddGameWonOverlay(gameWonOverlay, GameCanvas);
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

        //Method to add startpoint to screen
        public void AddStartPoint(StartPoint s, Canvas c)
        {
            //Add everything to the screen
            Canvas.SetTop(s.rect, s.Y);
            Canvas.SetLeft(s.rect, s.X);
            c.Children.Add(s.rect);
        }

        //Method to add the endpoint to the screen
        public void AddEndPoint(EndPoint e, Canvas c)
        {
            //Add everything to the screen
            Canvas.SetTop(e.rect, e.Y);
            Canvas.SetLeft(e.rect, e.X);
            c.Children.Add(e.rect);
            Canvas.SetTop(e.sprite, e.Y);
            Canvas.SetLeft(e.sprite, e.X);
            c.Children.Add(e.sprite);
        }

        //Method to remove the endpoint form the screen
        public void RemoveEndPoint(EndPoint e, Canvas c)
        {
            //Remove endpoint objects from the screen
            c.Children.Remove(e.rect);
            c.Children.Remove(e.sprite);
        }

        //Method to open a main menu
        public void ToMainMenu()
        {
            //Create new menu
            MainMenu mm = new MainMenu();
            //Opens the main menu
            mm.Show();
            //Close the game window
            mainWindow.Close();
        }

        //Method to add pause overlay to the screen
        public void AddPauseOverlay(PauseOverlay o, Canvas c)
        {
            //Add everything to the screen
            Canvas.SetTop(o.background, o.backgroundY);
            Canvas.SetLeft(o.background, o.backgroundX);
            c.Children.Add(o.background);
            Panel.SetZIndex(o.background, 99);
            Canvas.SetTop(o.pauseSprite, o.pauseSpriteY);
            Canvas.SetLeft(o.pauseSprite, o.pauseSpriteX);
            c.Children.Add(o.pauseSprite);
            Panel.SetZIndex(o.pauseSprite, 99);
            Canvas.SetTop(o.resume, o.resumeY);
            Canvas.SetLeft(o.resume, o.resumeX);
            c.Children.Add(o.resume);
            Panel.SetZIndex(o.resume, 99);
            Canvas.SetTop(o.restart, o.restartY);
            Canvas.SetLeft(o.restart, o.restartX);
            c.Children.Add(o.restart);
            Panel.SetZIndex(o.restart, 99);
            Canvas.SetTop(o.menu, o.menuY);
            Canvas.SetLeft(o.menu, o.menuX);
            c.Children.Add(o.menu);
            Panel.SetZIndex(o.menu, 99);
        }

        //Method to remove the pause overlay
        public void RemovePauseOverlay(PauseOverlay o, Canvas c)
        {
            //Remove endpoint objects from the screen
            c.Children.Remove(o.background);
            c.Children.Remove(o.pauseSprite);
            c.Children.Remove(o.resume);
            c.Children.Remove(o.restart);
            c.Children.Remove(o.menu);
        }

        //Method to add game over overlay
        public void AddGameOverOverlay(GameOverOverlay o, Canvas c)
        {
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
                //Add everything to the screen
                Canvas.SetTop(o.background, o.backgroundY);
                Canvas.SetLeft(o.background, o.backgroundX);
                c.Children.Add(o.background);
                Panel.SetZIndex(o.background, 99);
                Canvas.SetTop(o.GameOverSprite, o.GameOverSpriteY);
                Canvas.SetLeft(o.GameOverSprite, o.GameOverSpriteX);
                c.Children.Add(o.GameOverSprite);
                Panel.SetZIndex(o.GameOverSprite, 99);
                Canvas.SetTop(o.again, o.againY);
                Canvas.SetLeft(o.again, o.againX);
                c.Children.Add(o.again);
                Panel.SetZIndex(o.again, 99);
                Canvas.SetTop(o.menu, o.menuY);
                Canvas.SetLeft(o.menu, o.menuX);
                c.Children.Add(o.menu);
                Panel.SetZIndex(o.menu, 99);
            };

            backgroundWorker.RunWorkerAsync();
        }

        //Method to remove the game ovr overlay from the screen
        public void RemoveGameOverOverlay(GameOverOverlay o, Canvas c)
        {
            //Remove overlay objects from the screen
            c.Children.Remove(o.background);
            c.Children.Remove(o.GameOverSprite);
            c.Children.Remove(o.again);
            c.Children.Remove(o.menu);
        }

        //Method to add a victory overlat=y to the screen
        public void AddGameWonOverlay(GameWonOverlay o, Canvas c)
        {
            //Create audio player to play sound effect on game over
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
                //Add everything to the screen
                Canvas.SetTop(o.background, o.backgroundY);
                Canvas.SetLeft(o.background, o.backgroundX);
                c.Children.Add(o.background);
                Panel.SetZIndex(o.background, 99);
                Canvas.SetTop(o.VictorySprite, o.VictorySpriteY);
                Canvas.SetLeft(o.VictorySprite, o.VictorySpriteX);
                c.Children.Add(o.VictorySprite);
                Panel.SetZIndex(o.VictorySprite, 99);
                Canvas.SetTop(o.again, o.againY);
                Canvas.SetLeft(o.again, o.againX);
                c.Children.Add(o.again);
                Panel.SetZIndex(o.again, 99);
                Canvas.SetTop(o.menu, o.menuY);
                Canvas.SetLeft(o.menu, o.menuX);
                c.Children.Add(o.menu);
                Panel.SetZIndex(o.menu, 99);
            };

            backgroundWorker.RunWorkerAsync();
        }

        //Method to remove the victory overlay
        public void RemoveGameWonOverlay(GameWonOverlay o, Canvas c)
        {
            //Remove overlay objects from the screen
            c.Children.Remove(o.background);
            c.Children.Remove(o.VictorySprite);
            c.Children.Remove(o.again);
            c.Children.Remove(o.menu);
        }

        //Method to set the timerlabel content
        public void SetTimerText(string value)
        {
            mainWindow.TimerLabel.Text = value;
        }
    }
}
