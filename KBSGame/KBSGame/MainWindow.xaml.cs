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
        DispatcherTimer countdownTimer;
        TimeSpan playTime;
        Player speler;
        

        public MainWindow()
        {
            InitializeComponent();

            game = new Game(GameCanvas, 10, 10);
            speler = game.Player;

            //timer
            
           
            /*
            playTime = TimeSpan.FromSeconds(10);
            countdownTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimerLabel.Content = playTime.ToString(@"mm\:ss");
                if (playTime == TimeSpan.Zero) { countdownTimer.Stop(); GameOver(); }
                playTime = playTime.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            */

            
            

            //key eventhandler toevoegen
            this.KeyDown += new KeyEventHandler(OnKeyDown);

            

        }

        public void GameOver()
        {
            TimerLabel.Content = "00:00";
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

        private void OnKeyDown(object sender, KeyEventArgs e)
        {


            switch (e.Key)
            {
                case Key.Right:
                    speler.MoveRight();
                    break;
                case Key.Left:
                    speler.MoveLeft();
                    break;
                case Key.Down:
                    speler.MoveDown();
                    break;
                case Key.Up:
                    speler.MoveUp();
                    break;

            }
            if (speler.CheckEndPoint() == true)
            {
                //show endpoint dialog 
                EndPointModal dlg = new EndPointModal();
                dlg.Owner = this;
                if (dlg.ShowDialog() == true)
                {
                    game = null;
                   // game = new Game(GameCanvas, 10, 10);
                }
                return;
            }


        }

    }
}
