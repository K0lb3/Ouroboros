namespace SRPG
{
    using System;

    [EventActionInfo("会話/テロップ閉じる(2D)", "表示されているテロップを閉じます", 0x555555, 0x444488)]
    public class Event2dAction_EndTelop : EventAction
    {
        public Event2dAction_EndTelop()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            int num;
            num = EventTelopBubble.Instances.Count - 1;
            goto Label_0026;
        Label_0012:
            EventTelopBubble.Instances[num].Close();
            num -= 1;
        Label_0026:
            if (num >= 0)
            {
                goto Label_0012;
            }
            base.ActivateNext();
            return;
        }
    }
}

