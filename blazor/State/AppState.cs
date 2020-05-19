using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using blazor.Entities;

namespace blazor.State
{
    public class AppState
    {
        public int SelectedNumber { get; private set; }
        
        public int StopWatchTime { get; private set; }

        public event Action OnChange;
        public Timer AppTimer;

        private ISynchronizeInvoke synchronizingObject;

        public ISynchronizeInvoke SynchronizingObject
        //Property to get or set the object to Sync up against.
        {
            get { return this.synchronizingObject; }
            set { this.synchronizingObject = value; }
        }

        public AppState()
        {
            AppTimer = new Timer(NotifyStateChanged, SynchronizingObject);
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


