using System;
using KBSGame.GameObjects;
using KBSGame.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace KBSGame.Model
{
    public class Timer
    {
        public delegate void TijdIsOp(object source, EventArgs e);
        public event TijdIsOp tijdIsOp;
        public DispatcherTimer countdownTimer;
        private TimeSpan playTime;
        public int Seconds { get; set; }
        private MainWindow mainWindow;

        public Timer(int s, Game g, MainWindow mw)
        {
            mainWindow = mw;
            Seconds = s;

            playTime = TimeSpan.FromSeconds(Seconds);

            countdownTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                mw.TimerLabel.Text = playTime.ToString(@"ss");
                /* if (playing == false)
                {
                    countdownTimer.Stop();
                } */
                if (playTime == TimeSpan.Zero)
                {
                    OnTijdIsOp();
                    countdownTimer.Stop();
                    /* if (!GameWon)
                    {
                        game.FreezePlayer = true;
                        GameLost = true;
                        game.GameOver();
                    } */
                }
                playTime = playTime.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
        }

        protected virtual void OnTijdIsOp()
        {            
            tijdIsOp?.Invoke(this, EventArgs.Empty);
        }

        public void Restart()
        {
            playTime = TimeSpan.FromSeconds(Seconds);
            //timer label wordt op empty gezet zodat je geen verwarring krijgt met wat timer aangeeft
            mainWindow.TimerLabel.Text = String.Empty;

            countdownTimer.Start();
        }        
        public void Pauze()
        {
            countdownTimer.Stop();
        }

        public void Herstart()
        {
            countdownTimer.Start();
        }
        
    }
}
