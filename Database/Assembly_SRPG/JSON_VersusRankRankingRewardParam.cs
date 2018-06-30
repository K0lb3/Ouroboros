namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusRankRankingRewardParam
    {
        public int schedule_id;
        public int rank_begin;
        public int rank_end;
        public string reward_id;

        public JSON_VersusRankRankingRewardParam()
        {
            base..ctor();
            return;
        }
    }
}

