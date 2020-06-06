using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using blazor.Models;

namespace blazor.Store
{
    public class AppState
    {
        public event Action OnChange;
        public TimerState AppTimer;
        public StopwatchState AppStopwatch;
        public ModalState AppModal;
        public ViewOptionEnum AppView;

        public AppState()
        {
            AppTimer = new TimerState(NotifyStateChanged);
            AppStopwatch = new StopwatchState(NotifyStateChanged);
            AppModal = new ModalState(NotifyStateChanged);
            AppView = ViewOptionEnum.Stopwatch; // default view
        }

        public void UpdateView(ViewOptionEnum newView)
        {
            AppView = newView;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}