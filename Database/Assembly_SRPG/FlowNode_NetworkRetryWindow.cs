namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(0, "Create", 0, 0), NodeType("System/NetworkRetryWindow", 0x7fe5)]
    public class FlowNode_NetworkRetryWindow : FlowNode
    {
        public GameObject Window;

        public FlowNode_NetworkRetryWindow()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0023;
            }
            if ((this.Window != null) == null)
            {
                goto Label_0023;
            }
            Object.Instantiate<GameObject>(this.Window);
        Label_0023:
            return;
        }
    }
}

