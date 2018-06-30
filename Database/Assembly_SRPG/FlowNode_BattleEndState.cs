namespace SRPG
{
    using System;

    [Pin(1, "Cancel", 0, 1), Pin(0, "Accept", 0, 0), NodeType("Battle/EndState", 0x7fe5)]
    public class FlowNode_BattleEndState : FlowNode
    {
        public FlowNode_BattleEndState()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0015;
            }
            SceneBattle.Instance.GotoNextState();
            goto Label_0026;
        Label_0015:
            if (pinID != 1)
            {
                goto Label_0026;
            }
            SceneBattle.Instance.GotoPreviousState();
        Label_0026:
            return;
        }
    }
}

