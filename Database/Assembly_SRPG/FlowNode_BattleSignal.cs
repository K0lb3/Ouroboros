namespace SRPG
{
    using System;

    [Pin(2, "Out", 1, 0x3e8), NodeType("Battle/Signal", 0x44dd44), Pin(1, "Stop", 0, 1), Pin(0, "Resume", 0, 0)]
    public class FlowNode_BattleSignal : FlowNode
    {
        public FlowNode_BattleSignal()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_002E;
            }
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0021;
            }
            SceneBattle.Instance.UISignal = 0;
        Label_0021:
            base.ActivateOutputLinks(2);
            goto Label_0058;
        Label_002E:
            if (pinID != 1)
            {
                goto Label_0058;
            }
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0050;
            }
            SceneBattle.Instance.UISignal = 1;
        Label_0050:
            base.ActivateOutputLinks(2);
        Label_0058:
            return;
        }
    }
}

