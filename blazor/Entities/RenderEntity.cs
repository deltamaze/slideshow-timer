namespace blazor.Entities
{
    using System;
    using System.Timers;

    public class RenderEntity
    {

        private readonly Action onChangeCallback;

        public RenderEntity(Action callerOnChange)
        {
            // pass reference from the user of this class
            onChangeCallback = callerOnChange;
        }
    }
}