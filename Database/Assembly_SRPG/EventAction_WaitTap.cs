namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/待機/秒数(タップ)を指定", "指定した時間の間スクリプトの実行を停止します。", 0x555555, 0x444488)]
    public class EventAction_WaitTap : EventAction
    {
        [HideInInspector]
        public float WaitSeconds;
        public bool waitTap;
        private float mTimer;

        public EventAction_WaitTap()
        {
            this.WaitSeconds = 1f;
            base..ctor();
            return;
        }

        public override bool Forward()
        {
            if (this.waitTap == null)
            {
                goto Label_0013;
            }
            base.ActivateNext();
            return 1;
        Label_0013:
            return 0;
        }

        public override void GoToEndState()
        {
            this.mTimer = 0f;
            this.waitTap = 0;
            return;
        }

        public override void OnActivate()
        {
            this.mTimer = this.WaitSeconds;
            return;
        }

        public override void SkipImmediate()
        {
            this.mTimer = 0f;
            return;
        }

        public override void Update()
        {
            if (this.waitTap != null)
            {
                goto Label_0034;
            }
            this.mTimer -= Time.get_deltaTime();
            if (this.mTimer > 0f)
            {
                goto Label_0034;
            }
            base.ActivateNext();
            return;
        Label_0034:
            return;
        }
    }
}

