namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("立ち絵2/立ち絵消去(2D)", "表示されている立ち絵を消します", 0x555555, 0x444488)]
    public class Event2dAction_EndStandChara2 : EventAction
    {
        private const float WAIT_SECONDS = 1f;
        public string CharaID;
        private float mTimer;

        public Event2dAction_EndStandChara2()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            int num;
            EventStandCharaController2 controller;
            if (string.IsNullOrEmpty(this.CharaID) == null)
            {
                goto Label_0047;
            }
            num = EventStandCharaController2.Instances.Count - 1;
            goto Label_003B;
        Label_0022:
            EventStandCharaController2.Instances[num].Close(0.3f);
            num -= 1;
        Label_003B:
            if (num >= 0)
            {
                goto Label_0022;
            }
            goto Label_006A;
        Label_0047:
            controller = EventStandCharaController2.FindInstances(this.CharaID);
            if ((controller != null) == null)
            {
                goto Label_006A;
            }
            controller.Close(0.3f);
        Label_006A:
            this.mTimer = 1f;
            return;
        }

        public override void Update()
        {
            this.mTimer -= Time.get_deltaTime();
            if (this.mTimer > 0f)
            {
                goto Label_0029;
            }
            base.ActivateNext();
            return;
        Label_0029:
            return;
        }
    }
}

