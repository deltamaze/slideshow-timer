namespace blazor.Entities
{
    using System;
    using System.Timers;

    public class Modal : RenderEntity
    {
        public string ModalContentClass { get; private set; }
        public string ContentClass { get; private set; }

        public Modal(Action callerOnChange) : base(callerOnChange)
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
    }
}