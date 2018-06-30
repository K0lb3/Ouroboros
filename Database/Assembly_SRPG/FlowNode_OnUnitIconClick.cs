namespace SRPG
{
    using System;

    [NodeType("Event/OnUnitIconClick", 0xe57f), Pin(1, "Clicked", 1, 0)]
    public class FlowNode_OnUnitIconClick : FlowNode
    {
        public FlowNode_OnUnitIconClick()
        {
            base..ctor();
            return;
        }

        public void Click()
        {
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

