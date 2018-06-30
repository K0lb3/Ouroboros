namespace SRPG
{
    using System;

    [NodeType("Battle/PlayBGM", 0x7fe5), Pin(1, "停止", 0, 1), Pin(0, "再生", 0, 0)]
    public class FlowNode_BattlePlayBGM : FlowNode
    {
        public FlowNode_BattlePlayBGM()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0025;
            }
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0046;
            }
            SceneBattle.Instance.PlayBGM();
            goto Label_0046;
        Label_0025:
            if (pinID != 1)
            {
                goto Label_0046;
            }
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0046;
            }
            SceneBattle.Instance.StopBGM();
        Label_0046:
            return;
        }
    }
}

