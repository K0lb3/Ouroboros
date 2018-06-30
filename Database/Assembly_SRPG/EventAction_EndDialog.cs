namespace SRPG
{
    using System;

    [EventActionInfo("会話/閉じる", "表示されている吹き出しを閉じます", 0x555588, 0x5555aa)]
    public class EventAction_EndDialog : EventAction
    {
        [StringIsActorID]
        public string ActorID;

        public EventAction_EndDialog()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            int num;
            num = EventDialogBubble.Instances.Count - 1;
            goto Label_0056;
        Label_0012:
            if (string.IsNullOrEmpty(this.ActorID) != null)
            {
                goto Label_0042;
            }
            if ((EventDialogBubble.Instances[num].BubbleID == this.ActorID) == null)
            {
                goto Label_0052;
            }
        Label_0042:
            EventDialogBubble.Instances[num].Close();
        Label_0052:
            num -= 1;
        Label_0056:
            if (num >= 0)
            {
                goto Label_0012;
            }
            base.ActivateNext();
            return;
        }
    }
}

