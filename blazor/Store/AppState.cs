using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using blazor.Entities;

namespace blazor.Store
{
    public class AppState
    {
        public int SelectedNumber { get; private set; }

        public int StopWatchTime { get; private set; }

        public event Action OnChange;
        public Timer AppTimer;
        public Stopwatch AppStopwatch;

        public AppState()
        {
            AppTimer = new Timer(NotifyStateChanged);
            AppStopwatch = new Stopwatch(NotifyStateChanged);
        }

        public void SetNumber(int newNumber)
        {
            SelectedNumber = newNumber;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}