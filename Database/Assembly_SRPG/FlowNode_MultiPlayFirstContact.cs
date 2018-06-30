namespace SRPG
{
    using System;

    [Pin(2, "no", 1, 0), NodeType("Multi/MultiPlayFirstContact", 0x7fe5), Pin(0, "exist", 0, 0), Pin(1, "yes", 1, 0)]
    public class FlowNode_MultiPlayFirstContact : FlowNode
    {
        public FlowNode_MultiPlayFirstContact()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0034;
            }
        Label_0027:
            base.ActivateOutputLinks((((SceneBattle.Instance == null) == null) && (SceneBattle.Instance.FirstContact > 0)) ? 1 : 2);
        Label_0034:
            return;
        }
    }
}

