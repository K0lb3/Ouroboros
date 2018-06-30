namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class VersusRankRewardParam
    {
        private string mRewardId;
        private List<VersusRankReward> mRewardList;

        public VersusRankRewardParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusRankRewardParam json)
        {
            int num;
            VersusRankReward reward;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.mRewardId = json.reward_id;
            this.mRewardList = new List<VersusRankReward>();
            num = 0;
            goto Label_004F;
        Label_0026:
            reward = new VersusRankReward();
            if (reward.Deserialize(json.rewards[num]) == null)
            {
                goto Label_004B;
            }
            this.mRewardList.Add(reward);
        Label_004B:
            num += 1;
        Label_004F:
            if (num < ((int) json.rewards.Length))
            {
                goto Label_0026;
            }
            return 1;
        }

        public string RewardId
        {
            get
            {
                return this.mRewardId;
            }
        }

        public List<VersusRankReward> RewardList
        {
            get
            {
                return this.mRewardList;
            }
        }
    }
}

