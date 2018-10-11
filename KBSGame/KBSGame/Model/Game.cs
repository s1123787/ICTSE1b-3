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
        private KBSGame.Model.Timer GameTimer { get; set; }

        GameOverOverlay gameOverOverlay;

        public bool GameWon;
        public bool GameLost;
        public bool playing;
        
        private double testx;
        private double testy;

        Rectangle r;
        TextBlock pause = new TextBlock();

        public Game(MainWindow mw, Canvas canvas, int aantalBoom, int aantalBom, int aantalMoving, int s)
        {
            playing = true;
            Seconde = s;
            StartPoint = new StartPoint(canvas);
            EndPoint = new EndPoint(canvas);
            Player = new Player(canvas, this);
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, canvas);
            mainWindow = mw;
            GameCanvas = canvas;
            this.aantalBoom = aantalBoom;
            this.aantalBom = aantalBom;
            this.aantalMoving = aantalMoving;            
            Player.walkedOverBomb += OnPlayerWalkedOverBomb; //hier subscribed de methode OnPlayerWalkedOverBomb op de event walkedOverBomb             
            GameTimer = new Model.Timer(Seconde, this, mw); //hier wordt de timer aangemaakt die de meegegeven seconden gebruikt            
            GameTimer.tijdIsOp += OnPlayerTijdIsOp; //hier subscribe je op de event van timer
            Player.endPointReached += OnEndPointReached;
            mw.esqKeyIsPressed += OnEsqKeyIsPressed;
            mw.enterKeyIsPressed += OnEnterKeyIsPressed;
        }

        public void OnPlayerWalkedOverBomb(object source, GameOverEventArgs e)
        {
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
            GameCanvas.Children.Add(r);            
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += Timer_Tick;
            if(timer != null)
            {
                timer.Start();
            }  
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if ((Player.x <= testx+50  || Player.x >= testx-50) && Player.y == testy)
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
            //net als gameover deze code in andere klasse en vervolgens hier aanroepen
            if (playing)
            {
                pause.Text = "Pause";
                pause.Foreground = Brushes.Blue;
                pause.FontSize = 32;
                pause.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(pause, 312);
                Canvas.SetTop(pause, 220);
                GameCanvas.Children.Add(pause);
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
                GameCanvas.Children.Remove(pause);
                GameTimer.Herstart();
                FreezePlayer = false;
            }
        }

        public void PlayAgain()
        {
            Player.Reset();
            obstakels.Reset();
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, GameCanvas);
            FreezePlayer = false;
            GameTimer.Restart();
            playing = true;
            GameWon = false;
            GameLost = false;
        }

        public void Restart()
        {
            Player.Reset();
            obstakels.Reset();
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, GameCanvas);
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
