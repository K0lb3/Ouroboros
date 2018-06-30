namespace SRPG
{
    using System;

    [EventActionInfo("New/同期(アニメーション)", "非同期アニメーションが完了するのを待ちます", 0x555555, 0x444488)]
    public class EventAction_WaitAsyncAnimation : EventAction
    {
        [StringIsActorList]
        public string ActorID;

        public EventAction_WaitAsyncAnimation()
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
            EventAction_PlayAnimation3 animation;
            EventAction_PlayAnimation4 animation2;
            num = 0;
            goto Label_00C6;
        Label_0007:
            if ((base.Sequence.Actions[num] == this) == null)
            {
                goto Label_0024;
            }
            goto Label_00D9;
        Label_0024:
            if (base.Sequence.Actions[num].enabled == null)
            {
                goto Label_00C2;
            }
            if ((base.Sequence.Actions[num] as EventAction_PlayAnimation3) == null)
            {
                goto Label_0081;
            }
            animation = base.Sequence.Actions[num] as EventAction_PlayAnimation3;
            if ((animation.ActorID == this.ActorID) == null)
            {
                goto Label_00C2;
            }
            return;
            goto Label_00C2;
        Label_0081:
            if ((base.Sequence.Actions[num] as EventAction_PlayAnimation4) == null)
            {
                goto Label_00C2;
            }
            animation2 = base.Sequence.Actions[num] as EventAction_PlayAnimation4;
            if ((animation2.ActorID == this.ActorID) == null)
            {
                goto Label_00C2;
            }
            return;
        Label_00C2:
            num += 1;
        Label_00C6:
            if (num < ((int) base.Sequence.Actions.Length))
            {
                goto Label_0007;
            }
        Label_00D9:
            base.ActivateNext();
            return;
        }
    }
}

