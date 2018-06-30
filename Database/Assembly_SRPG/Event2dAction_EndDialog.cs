namespace SRPG
{
    using System;

    [EventActionInfo("会話/閉じる(2D)", "表示されている吹き出しを閉じます", 0x555555, 0x444488)]
    public class Event2dAction_EndDialog : EventAction
    {
        [StringIsActorID]
        public string ActorID;
        private EventDialogBubbleCustom mBubble;

        public Event2dAction_EndDialog()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            int num;
            if (string.IsNullOrEmpty(this.ActorID) == null)
            {
                goto Label_0042;
            }
            num = EventDialogBubbleCustom.Instances.Count - 1;
            goto Label_0036;
        Label_0022:
            EventDialogBubbleCustom.Instances[num].Close();
            num -= 1;
        Label_0036:
            if (num >= 0)
            {
                goto Label_0022;
            }
            goto Label_006F;
        Label_0042:
            this.mBubble = EventDialogBubbleCustom.Find(this.ActorID);
            if ((this.mBubble != null) == null)
            {
                goto Label_006F;
            }
            this.mBubble.Close();
        Label_006F:
            base.ActivateNext();
            return;
        }
    }
}

