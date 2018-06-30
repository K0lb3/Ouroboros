namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/立ち絵2/立ち絵消去2(2D)", "表示されている立ち絵を消します", 0x555555, 0x444488)]
    public class Event2dAction_EndStandChara3 : EventAction
    {
        public string CharaID;
        public float FadeTime;
        public bool Async;
        private float mTimer;

        public Event2dAction_EndStandChara3()
        {
            this.FadeTime = 1f;
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
            goto Label_006B;
        Label_0047:
            controller = EventStandCharaController2.FindInstances(this.CharaID);
            if ((controller != null) == null)
            {
                goto Label_006B;
            }
            controller.Close(this.FadeTime);
        Label_006B:
            this.mTimer = this.FadeTime;
            if (this.Async == null)
            {
                goto Label_0089;
            }
            base.ActivateNext(1);
        Label_0089:
            return;
        }

        public override void Update()
        {
            this.mTimer -= Time.get_deltaTime();
            if (this.mTimer > 0f)
            {
                goto Label_0040;
            }
            if (this.Async != null)
            {
                goto Label_0038;
            }
            base.ActivateNext();
            goto Label_003F;
        Label_0038:
            base.enabled = 0;
        Label_003F:
            return;
        Label_0040:
            return;
        }
    }
}

