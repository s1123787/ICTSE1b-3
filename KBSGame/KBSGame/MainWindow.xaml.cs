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
            game = new Game(GameCanvas);
            playTime = TimeSpan.FromSeconds(10);
            countdownTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimerLabel.Content = playTime.ToString(@"mm\:ss");
                if (playTime == TimeSpan.Zero) { countdownTimer.Stop(); GameOver(); }
                playTime = playTime.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            //speler aanmaken
            speler = new Player(GameCanvas);

            //key eventhandler toevoegen
            this.KeyDown += new KeyEventHandler(OnKeyDown);

            List<Obstakel> ObstacleList = new List<Obstakel>();
            List<String> Typelijst = new List<String>();
            Typelijst.Add("Boom");
            Typelijst.Add("Bom");
            Typelijst.Add("Boom");
            Typelijst.Add("Bom");
            Typelijst.Add("Boom");
            Typelijst.Add("Boom");
            Typelijst.Add("Bom");
            Typelijst.Add("Boom");
            Typelijst.Add("Bom");
            Typelijst.Add("Boom");
            for (int i = 0; i < 5; i++)
            {
                ObstacleList.Add(new Obstakel(GameCanvas, Typelijst[i]));
                Thread.Sleep(25);

            }
            for (int i = 0; i < ObstacleList.Count; i++)
            {
                GameCanvas.Children.Add(ObstacleList[i].rect);
            }
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
                    speler.MoveRight(GameCanvas);
                    break;
                case Key.Left:
                    speler.MoveLeft(GameCanvas);
                    break;
                case Key.Down:
                    speler.MoveDown(GameCanvas);
                    break;
                case Key.Up:
                    speler.MoveUp(GameCanvas);
                    break;

            }
        }
    }
}
