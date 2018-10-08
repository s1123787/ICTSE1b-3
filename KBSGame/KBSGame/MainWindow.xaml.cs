using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KBSGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        Game game;
        Player speler;
        TimeSpan playTime;
        int seconds = 4;
        DispatcherTimer countdownTimer;
        bool GameWon;
        bool GameLost;
        bool ShowOverlayOnce = true;

        public MainWindow()
        {
            InitializeComponent();

            game = new Game(this, GameCanvas, 10, 10, 2);
            speler = game.Player;

            //key eventhandler toevoegen
            this.KeyDown += new KeyEventHandler(OnKeyDown);


            //countdown timer
            playTime = TimeSpan.FromSeconds(seconds);
           
            countdownTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimerLabel.Text = playTime.ToString(@"ss");
                if (playTime == TimeSpan.Zero)
                {
                    countdownTimer.Stop();
                    if (!GameWon)
                    {
                        GameLost = true;
                        game.GameOver();
                    }
                }
                playTime = playTime.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            game = new Game(this, GameCanvas, 10, 10, 2);
            speler = game.Player;

            //key eventhandler toevoegen
            this.KeyDown += new KeyEventHandler(OnKeyDown);
        }
        
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                case Key.D:
                    speler.MoveRight();
                    break;
                case Key.Left:
                case Key.A:
                    speler.MoveLeft();
                    break;
                case Key.Down:
                case Key.S:
                    speler.MoveDown();
                    break;
                case Key.Up:
                case Key.W:
                    speler.MoveUp();
                    break;

            }

            if (speler.CheckEndPoint() == true && !GameLost)
            {
                GameWon = true;
                countdownTimer.Stop();
                if (ShowOverlayOnce)
                {
                    ShowOverlayOnce = false;
                    game.GameWon();
                }
                ////show endpoint dialog 
                //EndPointModal dlg = new EndPointModal();
                //dlg.Owner = this;
                //if (dlg.ShowDialog() == true)
                //{
                //    PlayAgain();
                //}
                //return;
            }
        }

        public void PlayAgain()
        {
            game.Restart();
            playTime = TimeSpan.FromSeconds(seconds);
            countdownTimer.Start();
            GameWon = false;
            GameLost = false;
            ShowOverlayOnce = true;
        }

        public void SetTimerLabel(string text)
        {
            TimerLabel.Text = text;
        }
    }
}
