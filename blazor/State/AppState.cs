using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazor.State
{
    public class AppState
    {
        public int SelectedNumber { get; private set; }
        public int TimeAmount { get; private set; }
        public int TimeLeft { get; private set; }
        public int StopWatchTime { get; private set; }

        public event Action OnChange;

        public void SetNumber(int newNumber)
        {
            SelectedNumber = newNumber;
            NotifyStateChanged();
        }
        public void StartTimer()
        {

        }
        public void StopTimer()
        {

        }
        public void ResetTimer()
        {

        }
        public void SetTimer()
        {

        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}


