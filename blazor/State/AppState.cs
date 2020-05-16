using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blazor.State
{
    public class AppState
    {
        public int SelectedNumber { get; private set; }

        public event Action OnChange;

        public void SetNumber(int newNumber)
        {
            SelectedNumber = newNumber;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}


