namespace SRPG.AnimEvents
{
    using System;

    public class TargetState : AnimEvent
    {
        public StateTypes State;

        public TargetState()
        {
            base..ctor();
            return;
        }

        public enum StateTypes
        {
            Stand,
            Down,
            Kirimomi
        }
    }
}

