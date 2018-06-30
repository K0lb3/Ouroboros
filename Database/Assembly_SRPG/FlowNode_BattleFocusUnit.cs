namespace SRPG
{
    using System;

    [NodeType("Battle/FocusUnit", 0x7fe5), Pin(0, "フォーカス", 0, 0)]
    public class FlowNode_BattleFocusUnit : FlowNode
    {
        public FlowNode_BattleFocusUnit()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0010;
            }
            SceneBattle.Instance.ResetMoveCamera();
        Label_0010:
            return;
        }
    }
}

