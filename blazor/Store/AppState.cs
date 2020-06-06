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
        public event Action OnChange;
        public Timer AppTimer;
        public Stopwatch AppStopwatch;
        public Modal AppModal;
        public ViewEnum AppView;

        public AppState()
        {
            AppTimer = new Timer(NotifyStateChanged);
            AppStopwatch = new Stopwatch(NotifyStateChanged);
            AppModal = new Modal(NotifyStateChanged);
            AppView = ViewEnum.Stopwatch;
        }
        
        public UpdateView(ViewEnum newView)
        {
            AppView = newView;
            NotifyStateChanged();
        }
        
        

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}