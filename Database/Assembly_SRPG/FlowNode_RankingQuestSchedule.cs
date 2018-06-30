namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(0x65, "開催期間中のランキングクエスト（有）", 1, 0x65), Pin(0x66, "開催期間中のランキングクエスト（無）", 1, 0x66), NodeType("System/RankingQuestSchedule"), Pin(100, "開催期間中のランキングクエストはあるか？", 0, 100)]
    public class FlowNode_RankingQuestSchedule : FlowNode
    {
        public const int INPUT_EXIST_OPEN_RANKING_SCHEDULE = 100;
        public const int OUTPUT_EXIST_OPEN_RANKING_SCHEDULE_TRUE = 0x65;
        public const int OUTPUT_EXIST_OPEN_RANKING_SCHEDULE_FALSE = 0x66;

        public FlowNode_RankingQuestSchedule()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            List<RankingQuestParam> list;
            if (pinID != 100)
            {
                goto Label_0044;
            }
            list = RankingQuestScheduleParam.FilterDuplicateRankingQuestIDs(RankingQuestScheduleParam.FindRankingQuestParamBySchedule(5));
            MonoSingleton<GameManager>.Instance.SetAvailableRankingQuestParams(list);
            if (list.Count <= 0)
            {
                goto Label_003B;
            }
            base.ActivateOutputLinks(0x65);
            goto Label_0044;
        Label_003B:
            base.ActivateOutputLinks(0x66);
        Label_0044:
            return;
        }
    }
}

