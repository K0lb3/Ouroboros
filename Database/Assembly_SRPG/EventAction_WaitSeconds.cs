namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("待機/秒数を指定", "指定した時間の間スクリプトの実行を停止します。", 0x555555, 0x444488)]
    public class EventAction_WaitSeconds : EventAction
    {
        public float WaitSeconds;
        private float mTimer;

        public EventAction_WaitSeconds()
        {
            this.WaitSeconds = 1f;
            base..ctor();
            return;
        }

        public override void GoToEndState()
        {
            this.mTimer = 0f;
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

