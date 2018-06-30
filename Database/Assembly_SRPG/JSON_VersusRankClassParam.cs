namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusRankClassParam
    {
        public int schedule_id;
        public int type;
        public int up_pt;
        public int down_pt;
        public int down_losing_streak;
        public string reward_id;
        public int win_pt_max;
        public int win_pt_min;
        public int lose_pt_max;
        public int lose_pt_min;

        public JSON_VersusRankClassParam()
        {
            base..ctor();
            return;
        }
    }
}

