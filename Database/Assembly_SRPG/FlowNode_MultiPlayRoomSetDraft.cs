namespace SRPG
{
    using System;

    [NodeType("Multi/MultiPlayRoomSetDraft", 0x7fe5), Pin(2, "Output", 1, 2), Pin(0, "Set Normal", 0, 0), Pin(1, "Set Draft", 0, 1)]
    public class FlowNode_MultiPlayRoomSetDraft : FlowNode
    {
        private const int PIN_INPUT_SET_NORMAL = 0;
        private const int PIN_INPUT_SET_DRAFT = 1;
        private const int PIN_OUTPUT_FINISH = 2;

        public FlowNode_MultiPlayRoomSetDraft()
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
                goto Label_001F;
            }
            goto Label_002A;
        Label_0014:
            GlobalVars.IsVersusDraftMode = 0;
            goto Label_002A;
        Label_001F:
            GlobalVars.IsVersusDraftMode = 1;
        Label_002A:
            base.ActivateOutputLinks(2);
            return;
        }
    }
}

