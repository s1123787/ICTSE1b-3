﻿using System;
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

            //Create the timer
            countdownTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {

                mw.TimerLabel.Text = playTime.ToString(@"ss");                
                if (playTime == TimeSpan.Zero)
                {
                    OnTijdIsOp(); //when time is 0 this event will be raised
                    countdownTimer.Stop();                    
                }
                //Update time left
                playTime = playTime.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
        }

        //Method to notify subscribers the timer has hit 0
        protected virtual void OnTijdIsOp()
        {   
            //check if there are any subscribers to this event
            tijdIsOp?.Invoke(this, EventArgs.Empty);
        }

        //Method to reset the timer
        public void Restart()
        {
            playTime = TimeSpan.FromSeconds(Seconds);
            //timer label will be empty the moment it will restart
            mainWindow.TimerLabel.Text = String.Empty;

            //timer will be started again
            countdownTimer.Start();
        }

        //Method to pause the timer
        public void Pause()
        {
            countdownTimer.Stop();
        }

        //Method to resume the timer
        public void Resume()
        {
            countdownTimer.Start();
        }
    }
}
