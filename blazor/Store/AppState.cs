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

        private readonly System.Threading.SynchronizationContext context;
        public System.Threading.SynchronizationContext Context
        {
            get { return this.context; }
        }

        public AppState()
        {
            AppTimer = new Timer(NotifyStateChanged);
        }
        public void SetNumber(int newNumber)
        {
            SelectedNumber = newNumber;
            NotifyStateChanged();
        }
        public void StopTimer()
        {

        }
        public void ResetTimer()
        {

        }
        private void NotifyStateChanged() => OnChange?.Invoke();
        
    }
}


