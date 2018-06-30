namespace SRPG
{
    using System;

    [NodeType("UI/DestroyCanvasAll", 0x7fe5), Pin(0, "In", 0, 0), Pin(1, "Out", 1, 0)]
    public class FlowNode_DestroyCanvasAll : FlowNode
    {
        public FlowNode_DestroyCanvasAll()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0013;
            }
            UIUtility.PopCanvasAll();
            base.ActivateOutputLinks(1);
        Label_0013:
            return;
        }
    }
}

