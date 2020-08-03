namespace blazor.Store
{
    using System;
    using System.Timers;

    public class StopwatchState : RenderEntity
    {
        public int CurrentHours { get; private set; }
        public int CurrentMinutes { get; private set; }
        public int CurrentSeconds { get; private set; }

        private readonly System.Timers.Timer sysTimer;
        public event Action TimerTickNotifier;

        public StopwatchState(Action callerOnChange) : base(callerOnChange)
        {
            // Create a timer with a one second interval.
            sysTimer = new System.Timers.Timer(1000);
            sysTimer.Elapsed += OnTimedEvent;
            sysTimer.AutoReset = true;
            // initial values
            ResetStopwatch();
        }

        public void StartStopwatch()
        {
            IncrementStopwatch(); // for design reasons, looks good to immediately increment
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
            CurrentHours = 0;
            CurrentMinutes = 0;
            CurrentSeconds = 0;

            onChangeCallback.Invoke();
        }

        public bool IsRunning()
        {
            return sysTimer.Enabled;
        }

        public void IncrementStopwatch()
        {
            if (CurrentSeconds != 59)
            {
                CurrentSeconds += 1;
            }
            else if (CurrentMinutes != 59)
            {
                CurrentMinutes += 1;
                CurrentSeconds = 0;
            }
            else
            {
                CurrentHours += 1;
                CurrentMinutes = 0;
                CurrentMinutes = 0;
            }

            onChangeCallback.Invoke();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            TimerTickNotifier?.Invoke();
        }
    }
}
