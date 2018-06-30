namespace SRPG
{
    using System;

    [EventActionInfo("BGM再生(2D)", "BGMを再生します", 0x555555, 0x444488)]
    public class Event2dAction_BGM : EventAction_BGM
    {
        public Event2dAction_BGM()
        {
            base..ctor();
            return;
        }
    }
}

