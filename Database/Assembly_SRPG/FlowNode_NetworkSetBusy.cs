namespace SRPG
{
    using System;

    [Pin(1, "Reset", 0, 1), Pin(0, "Set", 0, 0), NodeType("Network/SetBusy", 0x7fe5), Pin(100, "Output", 1, 100)]
    public class FlowNode_NetworkSetBusy : FlowNode
    {
        public FlowNode_NetworkSetBusy()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            if (num == null)
            {
                goto Label_0014;
            }
            if (num == 1)
            {
                goto Label_0029;
            }
            goto Label_003E;
        Label_0014:
            Network.IsForceBusy = 1;
            DebugUtility.LogError("Set Busy");
            goto Label_003E;
        Label_0029:
            Network.IsForceBusy = 0;
            DebugUtility.LogError("Reset Busy");
        Label_003E:
            base.ActivateOutputLinks(100);
            return;
        }
    }
}

