using System;
using System.Timers;

namespace blazor.Store
{
    public class TimerState : RenderEntity
    {
        public int StartingHours { get; private set; }
        public int StartingMinutes { get; private set; }
        public int StartingSeconds { get; private set; }
        public int CurrentHours { get; private set; }
        public int CurrentMinute { get; private set; }
        public int CurrentSeconds { get; private set; }
        public bool TimerFinished { get; private set; }

        private readonly System.Timers.Timer sysTimer;
        public event Action TimerTickNotifier;

        public TimerState(Action callerOnChange) : base(callerOnChange)
        {
            // Create a timer with a one second interval.
            sysTimer = new System.Timers.Timer(1000);
            sysTimer.Elapsed += OnTimedEvent;
            sysTimer.AutoReset = true;
            // initial values
            TimerFinished = false;
            StartingHours = 0;
            StartingMinutes = 10;
            StartingSeconds = 0;
            ResetTimer();
        }

        public void SetTimer(int hour, int minute, int second)
        {
            StartingHours = hour;
            StartingMinutes = minute;
            StartingSeconds = second;
            ResetTimer();

            onChangeCallback.Invoke();
        }

        public void StartTimer()
        {
            // only start if component is listening to changes
            if (TimerTickNotifier != null)
            {
                sysTimer.Start();
            }
        }

        public void PauseTimer()
        {
            sysTimer.Stop();
        }

        public void ResetTimer()
        {
            PauseTimer();
            CurrentHours = StartingHours;
            CurrentMinute = StartingMinutes;
            CurrentSeconds = StartingSeconds;

            onChangeCallback.Invoke();
        }

        public bool IsRunning()
        {
            return sysTimer.Enabled;
        }

        public void DecrementTimer()
        {
            if (CurrentSeconds != 0)
            {
                CurrentSeconds -= 1;
            }
            else if (CurrentMinute != 0)
            {
                CurrentMinute -= 1;
                CurrentSeconds = 59;
            }
            else if (CurrentHours != 0)
            {
                CurrentHours -= 1;
                CurrentMinute = 59;
                CurrentMinute = 59;
            }
            else
            {
                TimerFinished = true;
                PauseTimer();
            }

            onChangeCallback.Invoke();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            TimerTickNotifier?.Invoke();
        }
    }
}
