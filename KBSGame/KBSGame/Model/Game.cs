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
    class Game
    {
        DispatcherTimer timer = new DispatcherTimer();
        TimeSpan playTime;

        public StartPoint StartPoint { get; set; }
        public EndPoint EndPoint { get; set; }
        public Player Player { get; private set; }
        public Obstakels obstakels { get; set; }
        public MainWindow mainWindow { get; set; }
        public Canvas GameCanvas { get; private set; }
        private int aantalBoom;
        private int aantalBom;
        private int aantalMoving;

        private double testx;
        private double testy;
        Rectangle r;
        
        public Game(MainWindow mw, Canvas canvas, int aantalBoom, int aantalBom, int aantalMoving)
        {
            StartPoint = new StartPoint(canvas);
            EndPoint = new EndPoint(canvas);
            Player = new Player(canvas);
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, canvas);
            mainWindow = mw;
            GameCanvas = canvas;
            this.aantalBoom = aantalBoom;
            this.aantalBom = aantalBom;
            Player.walkedOverBomb += OnPlayerWalkedOverBomb;
        }

        public void GameOver()
        {
            GameOverOverlay gameOverOverlay = new GameOverOverlay(mainWindow, GameCanvas);
        }

        public void GameWon()
        {
            GameWonOverlay gameWonOverlay = new GameWonOverlay(mainWindow, GameCanvas);
        }

        public void OnPlayerWalkedOverBomb(object source, GameOverEventArgs e)
        {

            double x = e.x;
            double y = e.y;
            testx = e.bomx;
            testy = e.bomy;
            r = new Rectangle();
            r.Fill = Brushes.LightGray;
            r.Height = 40;
            r.Width = 40;
            Canvas.SetLeft(r, x + 5);
            Canvas.SetTop(r, y + 5);
            Canvas.SetZIndex(r, 0);
            Obstakels.waardes.Remove($"{x}{y}b");
            GameCanvas.Children.Add(r);
            //canvas.Children.Add(r);
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += Timer_Tick;
            if(timer != null)
            {
                timer.Start();
            }           



        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Player.x == testx && Player.y == testy)
            {
                //throw event to stop the game
            }
        }

        public void Restart()
        {
            Player.Reset();
            obstakels.Reset();
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, GameCanvas);
        }

    }
}
