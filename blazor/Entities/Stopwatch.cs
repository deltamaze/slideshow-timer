using System;
using System.Timers;

namespace blazor.Entities
{
    public class Stopwatch
    {
        public int CurrentHour { get; private set; }
        public int CurrentMinute { get; private set; }
        public int CurrentSecond { get; private set; }

        private readonly System.Timers.Timer sysTimer;
        private readonly Action OnChangeCallback;
        public event Action TimerTickNotifier;

        public Stopwatch(Action callerOnChange)
        {
            // pass reference from the user of this class
            OnChangeCallback = callerOnChange;

            // Create a timer with a one second interval.
            sysTimer = new System.Timers.Timer(100);
            sysTimer.Elapsed += OnTimedEvent;
            sysTimer.AutoReset = true;
            // initial values
            ResetStopwatch();
        }
        public void StartStopwatch()
        {
            //only start if component is listening to changes
            if (TimerTickNotifier != null)
            {
                sysTimer.Start();
            }
        }
        public void PauseStopwatch()
        {
            sysTimer.Stop();
        }
        public void ResetStopwatch()
        {
            PauseStopwatch();
            CurrentHour = 0;
            CurrentMinute = 0;
            CurrentSecond = 0;

            OnChangeCallback.Invoke();

        }
        public void IncrementStopwatch()
        {
            if (CurrentSecond != 59)
            {
                CurrentSecond += 1;
            }
            else if (CurrentMinute != 59)
            {
                CurrentMinute += 1;
                CurrentSecond = 0;
            }
            else
            {
                CurrentHour += 1;
                CurrentMinute = 0;
                CurrentMinute = 0;
            }
            OnChangeCallback.Invoke();
        }


        #region private methods
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            TimerTickNotifier?.Invoke();
        }
        #endregion

    }
}
