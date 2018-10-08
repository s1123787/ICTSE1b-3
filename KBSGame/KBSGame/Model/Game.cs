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
        private int aantalBoom;
        private int aantalBom;
        private int aantalMoving;
        
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
        }

        public void GameOver()
        {
            GameOverOverlay gameOverOverlay = new GameOverOverlay(mainWindow, GameCanvas);
        }

        public void GameWon()
        {
            GameWonOverlay gameWonOverlay = new GameWonOverlay(mainWindow, GameCanvas);
        }

        public void Restart()
        {
            Player.Reset();
            obstakels.Reset();
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, GameCanvas);
        }

    }
}
