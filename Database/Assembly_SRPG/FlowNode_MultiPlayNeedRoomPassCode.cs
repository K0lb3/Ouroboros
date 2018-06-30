namespace SRPG
{
    using System;

    [Pin(1, "Yes", 1, 0), NodeType("Multi/MultiPlayNeedRoomPassCode", 0x7fe5), Pin(0, "Test", 0, 0), Pin(2, "No", 1, 0)]
    public class FlowNode_MultiPlayNeedRoomPassCode : FlowNode
    {
        public FlowNode_MultiPlayNeedRoomPassCode()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_002A;
            }
            if (MultiPlayAPIRoom.IsLocked(GlobalVars.SelectedMultiPlayRoomPassCodeHash) == null)
            {
                goto Label_0022;
            }
            base.ActivateOutputLinks(1);
            goto Label_002A;
        Label_0022:
            base.ActivateOutputLinks(2);
        Label_002A:
            return;
        }
    }
}

