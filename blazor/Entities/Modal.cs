namespace blazor.Entities
{
    using System;
    using System.Timers;

    public class Modal
    {
        public string ModalContentClass { get; private set; }
        public string ContentClass { get; private set; }

        private readonly Action onChangeCallback;

        public Modal(Action callerOnChange)
        {
            // pass reference from the user of this class
            onChangeCallback = callerOnChange;
        }

        public void ShowModal()
        {
            ContentClass = "modal-active";
            ModalContentClass = "one";

            onChangeCallback.Invoke();
        }

        public void HideModal()
        {
            ContentClass = ""; // contentClass.Replace("modal-active","");
            ModalContentClass += " out";

            onChangeCallback.Invoke();
        }
    }
}