using KBSGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace KBSGame
{
    class Game
    {
        

        public StartPoint StartPoint { get; set; }
        public EndPoint EndPoint { get; set; }
 
        public Player Player { get; private set; }
        public Obstakels obstakels { get; set; }
        public Canvas GameCanvas { get;  set; }
        private int aantalBoom;
        private int aantalBom;
        private int aantalMoving;


        public Game(Canvas canvas, int aantalBoom, int aantalBom, int aantalMoving)
        {
            StartPoint = new StartPoint(canvas);
            EndPoint = new EndPoint(canvas);
            Player = new Player(canvas);
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, canvas);
            this.aantalBoom = aantalBoom;
            this.aantalBom = aantalBom;
            GameCanvas = canvas;
        }

        public void GameOver()
        {
            //TimerLabel.Content = "00:00";
            SoundPlayer audio = new SoundPlayer(Properties.Resources.game_over_sound_effect);
            audio.Play();
            Thread.Sleep(1000);
            TextBlock textBlock = new TextBlock();
            #region textBlock config
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.Text = "Game Over";
            textBlock.Foreground = Brushes.Red;
            textBlock.FontSize = 32;
            textBlock.FontWeight = FontWeights.Bold;
            #endregion
            Canvas.SetLeft(textBlock, 312);
            Canvas.SetTop(textBlock, 220);
            GameCanvas.Children.Add(textBlock);

        }

        public void Restart()
        {
            Player.Reset();
            obstakels.Reset();
            obstakels = new Obstakels(aantalBoom, aantalBom, aantalMoving, GameCanvas);
        }

    }
}
