namespace SRPG
{
    using System;

    public class VersusRankReward
    {
        private RewardType mType;
        private string mIName;
        private int mNum;

        public VersusRankReward()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusRankRewardRewardParam json)
        {
            this.mType = json.item_type;
            this.mIName = json.item_iname;
            this.mNum = json.item_num;
            return 1;
        }

        public RewardType Type
        {
            get
            {
                return this.mType;
            }
        }

        public string IName
        {
            get
            {
                return this.mIName;
            }
        }

        public int Num
        {
            get
            {
                return this.mNum;
            }
        }
    }
}

