namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;

    public class RankingQuestParam
    {
        public int schedule_id;
        public RankingQuestType type;
        public string iname;
        public int reward_id;
        public RankingQuestRewardParam rewardParam;
        public RankingQuestScheduleParam scheduleParam;

        public RankingQuestParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_RankingQuestParam json)
        {
            this.schedule_id = json.schedule_id;
            if (((int) Enum.GetNames(typeof(RankingQuestType)).Length) <= json.type)
            {
                goto Label_0039;
            }
            this.type = json.type;
            goto Label_0043;
        Label_0039:
            DebugUtility.LogError("定義されていない列挙値が指定されようとしました");
        Label_0043:
            this.iname = json.iname;
            this.reward_id = json.reward_id;
            return 1;
        }

        public static RankingQuestParam FindRankingQuestParam(string targetQuestID, int scheduleID, RankingQuestType type)
        {
            RankingQuestParam param;
            GameManager manager;
            <FindRankingQuestParam>c__AnonStorey2E6 storeye;
            storeye = new <FindRankingQuestParam>c__AnonStorey2E6();
            storeye.scheduleID = scheduleID;
            storeye.type = type;
            storeye.targetQuestID = targetQuestID;
            param = null;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0031;
            }
            return param;
        Label_0031:
            if (manager.RankingQuestParams != null)
            {
                goto Label_003E;
            }
            return param;
        Label_003E:
            return manager.RankingQuestParams.Find(new Predicate<RankingQuestParam>(storeye.<>m__267));
        }

        [CompilerGenerated]
        private sealed class <FindRankingQuestParam>c__AnonStorey2E6
        {
            internal int scheduleID;
            internal RankingQuestType type;
            internal string targetQuestID;

            public <FindRankingQuestParam>c__AnonStorey2E6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__267(RankingQuestParam param)
            {
                return (((param.schedule_id != this.scheduleID) || (param.type != this.type)) ? 0 : (param.iname == this.targetQuestID));
            }
        }
    }
}

