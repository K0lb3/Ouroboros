namespace SRPG
{
    using System;

    [EventActionInfo("New/立ち絵2/表情切替", "指定したキャラの表情を切り替えます。", 0x555555, 0x444488)]
    public class Event2dAction_ChangeEmotion : EventAction
    {
        public string CharaID;
        public string Emotion;

        public Event2dAction_ChangeEmotion()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            EventStandCharaController2 controller;
            if (string.IsNullOrEmpty(this.CharaID) != null)
            {
                goto Label_0044;
            }
            if (string.IsNullOrEmpty(this.Emotion) != null)
            {
                goto Label_0044;
            }
            controller = EventStandCharaController2.FindInstances(this.CharaID);
            controller.Emotion = this.Emotion;
            controller.UpdateEmotion(this.Emotion);
        Label_0044:
            base.ActivateNext();
            return;
        }

        public override void PreStart()
        {
        }
    }
}

