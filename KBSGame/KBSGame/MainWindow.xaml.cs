using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        DispatcherTimer countdownTimer;
        TimeSpan playTime;

        public MainWindow()
        {
            InitializeComponent();
            initStartpoint();

            playTime = TimeSpan.FromSeconds(10);

            countdownTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                TimerLabel.Content = playTime.ToString(@"mm\:ss");
                if (playTime == TimeSpan.Zero) { countdownTimer.Stop(); GameOver(); }
                playTime = playTime.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            initEndpoint();
        }

        public void initStartpoint()
        {
            Ellipse cirkel = new Ellipse();
            cirkel.Fill = Brushes.Gray;
            cirkel.Width = 50;
            cirkel.Height = 50;
            Canvas.SetLeft(cirkel, 0);
            Canvas.SetTop(cirkel, 0);
            GameCanvas.Children.Add(cirkel);
        }

        public void initEndpoint()
        {

        }

        public void GameOver()
        {
            TimerLabel.Content = "";
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
    }
}
