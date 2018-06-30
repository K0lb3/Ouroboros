namespace SRPG
{
    using System;

    [NodeType("SRPG/SkipFriendSupport", 0x7fe5), Pin(1, "Out", 1, 1), Pin(100, "In", 0, 0)]
    public class FlowNode_SkipFriendSupport : FlowNode
    {
        public FlowNode_SkipFriendSupport()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_001B;
            }
            GlobalVars.SelectedSupport.Set(null);
            base.ActivateOutputLinks(1);
        Label_001B:
            return;
        }
    }
}

