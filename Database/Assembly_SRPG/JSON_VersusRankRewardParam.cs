namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_VersusRankRewardParam
    {
        public string reward_id;
        public JSON_VersusRankRewardRewardParam[] rewards;

        public JSON_VersusRankRewardParam()
        {
            base..ctor();
            return;
        }
    }
}

