namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/待機", "待機します。", 0x555555, 0x444488)]
    public class Event2dAction_WaitTap : EventAction
    {
        public bool tapWaiting;
        [HideInInspector]
        public float WaitSeconds;
        private float mTimer;
        private bool waitFrame;

        public Event2dAction_WaitTap()
        {
            this.WaitSeconds = 1f;
            base..ctor();
            return;
        }

        public override bool Forward()
        {
            if (this.waitFrame == null)
            {
                goto Label_001E;
            }
            if (this.tapWaiting == null)
            {
                goto Label_001E;
            }
            base.ActivateNext();
            return 1;
        Label_001E:
            return 0;
        }

        public override void OnActivate()
        {
            this.waitFrame = 0;
            if (this.tapWaiting == null)
            {
                goto Label_0017;
            }
            goto Label_0023;
        Label_0017:
            this.mTimer = this.WaitSeconds;
        Label_0023:
            return;
        }

        public override void Update()
        {
            if (this.waitFrame != null)
            {
                goto Label_0013;
            }
            this.waitFrame = 1;
            return;
        Label_0013:
            if (this.tapWaiting != null)
            {
                goto Label_0047;
            }
            this.mTimer -= Time.get_deltaTime();
            if (this.mTimer > 0f)
            {
                goto Label_0047;
            }
            base.ActivateNext();
            return;
        Label_0047:
            return;
        }
    }
}

