namespace SRPG
{
    using System;

    [EventActionInfo("会話/表示 (非同期)", "会話の文章を表示します。", 0x555588, 0x5555aa)]
    public class EventAction_DialogAsync : EventAction_Dialog
    {
        public EventAction_DialogAsync()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            base.OnActivate();
            base.ActivateNext();
            return;
        }

        protected override void OnFinish()
        {
        }
    }
}

