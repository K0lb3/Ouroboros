namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(0, "Start", 0, 0), NodeType("System/アセットを待つ", 0x7fe5), Pin(10, "Finished", 1, 0)]
    public class FlowNode_WaitAssets : FlowNode
    {
        public const int PINID_START = 0;
        public const int PINID_FINISHED = 10;
        private const float MIN_WAIT_TIME = 0.1f;
        private float mWaitTime;

        public FlowNode_WaitAssets()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0018;
            }
            base.set_enabled(1);
            this.mWaitTime = 0.1f;
        Label_0018:
            return;
        }

        private void Update()
        {
            if (AssetManager.IsLoading != null)
            {
                goto Label_004C;
            }
            this.mWaitTime = Mathf.Max(this.mWaitTime - Time.get_unscaledDeltaTime(), 0f);
            if (this.mWaitTime > 0f)
            {
                goto Label_0057;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
            goto Label_0057;
        Label_004C:
            this.mWaitTime = 0.1f;
        Label_0057:
            return;
        }
    }
}

