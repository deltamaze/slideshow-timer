namespace blazor.Store
{
    using System;
    using System.Timers;

    public class ModalState : RenderEntity
    {
        public string ModalContentClass { get; private set; }
        public string ContentClass { get; private set; }

        public ModalState(Action callerOnChange) : base(callerOnChange)
        {
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

        public void ResetModalDisplay() // when reloading timer, don't re-animate modal
        {
            ContentClass = ""; // contentClass.Replace("modal-active","");
            ModalContentClass = "";
        }
    }
}