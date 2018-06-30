namespace SRPG
{
    using System;

    [EventActionInfo("同期", "非同期処理が完了するのを待ちます", 0x555555, 0x444488)]
    public class EventAction_WaitAsyncActions : EventAction
    {
        public EventAction_WaitAsyncActions()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
        }

        public override void Update()
        {
            int num;
            num = 0;
            goto Label_0040;
        Label_0007:
            if ((base.Sequence.Actions[num] == this) == null)
            {
                goto Label_0024;
            }
            goto Label_0053;
        Label_0024:
            if (base.Sequence.Actions[num].enabled == null)
            {
                goto Label_003C;
            }
            return;
        Label_003C:
            num += 1;
        Label_0040:
            if (num < ((int) base.Sequence.Actions.Length))
            {
                goto Label_0007;
            }
        Label_0053:
            base.ActivateNext();
            return;
        }
    }
}

