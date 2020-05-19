using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace blazor.Entities
{
    public class Timer
    {
        public int StartingHour { get; private set; }
        public int StartingMinute { get; private set; }
        public int StartingSecond { get; private set; }
        public int CurrentHour { get; private set; }
        public int CurrentMinute { get; private set; }
        public int CurrentSecond { get; private set; }
        public bool TimerFinished { get; private set; }

        private  System.Timers.Timer sysTimer;
        private Action OnChangeCallback;

        

        public Timer(Action callerOnChange, ISynchronizeInvoke SynchronizingObject)
        {
            // pass reference from the user of this class
            OnChangeCallback = callerOnChange;

            // Create a timer with a one second interval.
            sysTimer = new System.Timers.Timer(100);
            sysTimer.SynchronizingObject = SynchronizingObject;
            sysTimer.Elapsed += OnTimedEvent;
            sysTimer.AutoReset = true;
            // initial values
            TimerFinished = false;
            StartingHour = 0;
            StartingMinute = 0;
            StartingSecond = 0;
            ResetTimer();
        }
        #region public methods
        public void SetTimer(int hour, int minute, int second)
        {
            StartingHour = hour;
            StartingMinute = minute;
            StartingSecond = second;
            ResetTimer();

            OnChangeCallback.Invoke();
        }
        public void StartTimer()
        {
            sysTimer.Start();
        }
        public void PauseTimer()
        {
            sysTimer.Stop();
        }
        public void ResetTimer()
        {
            PauseTimer();
            CurrentHour = StartingHour;
            CurrentMinute = StartingMinute;
            CurrentSecond = StartingSecond;

            OnChangeCallback.Invoke();

        }
        #endregion

        #region private methods
        private void NotifyChange()
        {
            OnChangeCallback.Invoke();
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (CurrentSecond != 0)
            {
                CurrentSecond -= 1;
            } 
            else if ( CurrentMinute != 0)
            {
                CurrentMinute -= 1;
                CurrentSecond = 59;
            }
            else if (CurrentHour != 0)
            {
                CurrentHour -= 1;
                CurrentMinute = 59;
                CurrentMinute = 59;
            }
            else
            {
                TimerFinished = true;
                PauseTimer();
            }


            //SynchronizingObject is not null and the Synchronizing
            //Object Requires and invokation...
            if ((this.sysTimer.SynchronizingObject != null) &&
                          this.sysTimer.SynchronizingObject.InvokeRequired)
                this.sysTimer.SynchronizingObject.BeginInvoke(OnChangeCallback,
                                         new object[] { this, e });
            //Get the Synchronizing Object to invoke the event...
            else
                OnChangeCallback.Invoke(); //Fire the event

            // OnChangeCallback.Invoke();
            //this.BeginInvoke(new InvokeDelegate(NotifyChange()));
        }
        #endregion
    }
}
