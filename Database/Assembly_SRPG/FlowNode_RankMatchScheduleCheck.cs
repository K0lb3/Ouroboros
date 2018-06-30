namespace SRPG
{
    using GR;
    using System;

    [Pin(12, "Rewarding", 1, 12), Pin(11, "Aggregating", 1, 11), Pin(10, "Open", 1, 10), Pin(1, "Check", 0, 1), NodeType("Multi/RankMatchScheduleCheck", 0x7fe5)]
    public class FlowNode_RankMatchScheduleCheck : FlowNode
    {
        private const int PIN_IN_CHECK = 1;
        private const int PIN_OUT_OPEN = 10;
        private const int PIN_OUT_AGGREGATING = 11;
        private const int PIN_OUT_REWARDING = 12;

        public FlowNode_RankMatchScheduleCheck()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            if (pinID != 1)
            {
                goto Label_004A;
            }
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.RankMatchScheduleId <= 0)
            {
                goto Label_0027;
            }
            base.ActivateOutputLinks(10);
            goto Label_004A;
        Label_0027:
            if (manager.RankMatchRankingStatus != 1)
            {
                goto Label_0041;
            }
            base.ActivateOutputLinks(11);
            goto Label_004A;
        Label_0041:
            base.ActivateOutputLinks(12);
        Label_004A:
            return;
        }
    }
}

