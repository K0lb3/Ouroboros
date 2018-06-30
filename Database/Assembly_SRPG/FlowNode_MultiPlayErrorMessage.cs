namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(10, "Open", 0, 0), Pin(100, "Closed", 1, 1), NodeType("Multi/MultiPlayErrorMessage", 0x7fe5)]
    public class FlowNode_MultiPlayErrorMessage : FlowNode
    {
        private GameObject winGO;

        public FlowNode_MultiPlayErrorMessage()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnActivate>m__1A9(GameObject go)
        {
            if ((this.winGO != null) == null)
            {
                goto Label_0021;
            }
            this.winGO = null;
            base.ActivateOutputLinks(100);
        Label_0021:
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_002D;
            }
            this.winGO = UIUtility.SystemMessage(null, Network.ErrMsg, new UIUtility.DialogResultEvent(this.<OnActivate>m__1A9), null, 0, -1);
            Network.ResetError();
        Label_002D:
            return;
        }
    }
}

