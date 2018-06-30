namespace SRPG
{
    using GR;
    using System;

    [Pin(11, "Disable", 1, 11), NodeType("Multi/MultiPlayRankMatchCheckBP", 0x7fe5), ShowInInspector, Pin(0, "Check", 0, 0), Pin(10, "Enable", 1, 10)]
    public class FlowNode_MultiPlayRankMatchCheckBP : FlowNode
    {
        private const int PIN_IN_CHECK = 0;
        private const int PIN_OUT_ENABLE = 10;
        private const int PIN_OUT_DISABLE = 11;

        public FlowNode_MultiPlayRankMatchCheckBP()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0032;
            }
            if (MonoSingleton<GameManager>.Instance.Player.RankMatchBattlePoint <= 0)
            {
                goto Label_0029;
            }
            base.ActivateOutputLinks(10);
            goto Label_0032;
        Label_0029:
            base.ActivateOutputLinks(11);
        Label_0032:
            return;
        }
    }
}

