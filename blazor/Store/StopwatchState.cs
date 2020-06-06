namespace blazor.Store
{
    using System;
    using System.Timers;

    public class StopwatchState : RenderEntity
    {
        public int CurrentHour { get; private set; }
        public int CurrentMinute { get; private set; }
        public int CurrentSecond { get; private set; }

        private readonly System.Timers.Timer sysTimer;
        public event Action TimerTickNotifier;

        public StopwatchState(Action callerOnChange) : base(callerOnChange)
        {
            // Create a timer with a one second interval.
            sysTimer = new System.Timers.Timer(100);
            sysTimer.Elapsed += OnTimedEvent;
            sysTimer.AutoReset = true;
            // initial values
            ResetStopwatch();
        }

        public void StartStopwatch()
        {
            // only start if component is listening to changes
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

            onChangeCallback.Invoke();
        }

        public bool IsRunning()
        {
            return sysTimer.Enabled;
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

            onChangeCallback.Invoke();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            TimerTickNotifier?.Invoke();
        }
    }
}
