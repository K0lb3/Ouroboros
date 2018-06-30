namespace SRPG
{
    using System;

    [EventActionInfo("New/ボイス再生", "ボイスを再生します。", 0x445555, 0x448888)]
    internal class Event2dAction_Voice : EventAction_Voice
    {
        public Event2dAction_Voice()
        {
            base..ctor();
            return;
        }
    }
}

