namespace SRPG
{
    using GR;
    using System;

    [EventActionInfo("SE再生(2D)", "SEを再生します", 0x555555, 0x444488)]
    public class Event2dAction_SE : EventAction
    {
        public string SE;

        public Event2dAction_SE()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SE, 0f);
            base.ActivateNext();
            return;
        }
    }
}

