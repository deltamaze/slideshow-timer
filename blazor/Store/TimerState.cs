using System;
using System.Timers;

namespace blazor.Store
{
    public class TimerState : RenderEntity
    {
        public int StartingHour { get; private set; }
        public int StartingMinute { get; private set; }
        public int StartingSecond { get; private set; }
        public int CurrentHour { get; private set; }
        public int CurrentMinute { get; private set; }
        public int CurrentSecond { get; private set; }
        public bool TimerFinished { get; private set; }

        private readonly System.Timers.Timer sysTimer;
        public event Action TimerTickNotifier;

        public TimerState(Action callerOnChange) : base(callerOnChange)
        {
            // Create a timer with a one second interval.
            sysTimer = new System.Timers.Timer(100);
            sysTimer.Elapsed += OnTimedEvent;
            sysTimer.AutoReset = true;
            // initial values
            TimerFinished = false;
            StartingHour = 0;
            StartingMinute = 0;
            StartingSecond = 0;
            ResetTimer();
        }

        public void SetTimer(int hour, int minute, int second)
        {
            StartingHour = hour;
            StartingMinute = minute;
            StartingSecond = second;
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
            CurrentHour = StartingHour;
            CurrentMinute = StartingMinute;
            CurrentSecond = StartingSecond;

            onChangeCallback.Invoke();
        }

        public void DecrementTimer()
        {
            if (CurrentSecond != 0)
            {
                CurrentSecond -= 1;
            }
            else if (CurrentMinute != 0)
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

            onChangeCallback.Invoke();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            TimerTickNotifier?.Invoke();
        }
    }
}
