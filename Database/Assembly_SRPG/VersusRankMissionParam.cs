namespace SRPG
{
    using System;

    public class VersusRankMissionParam
    {
        private string mIName;
        private string mName;
        private string mExpire;
        private RankMatchMissionType mType;
        private string mSVal;
        private int mIVal;
        private string mRewardId;

        public VersusRankMissionParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusRankMissionParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.mIName = json.iname;
            this.mName = json.name;
            this.mExpire = json.expr;
            this.mType = json.type;
            this.mSVal = json.sval;
            this.mIVal = json.ival;
            this.mRewardId = json.reward_id;
            return 1;
        }

        public string IName
        {
            get
            {
                return this.mIName;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
        }

        public string Expire
        {
            get
            {
                return this.mExpire;
            }
        }

        public RankMatchMissionType Type
        {
            get
            {
                return this.mType;
            }
        }

        public string SVal
        {
            get
            {
                return this.mSVal;
            }
        }

        public int IVal
        {
            get
            {
                return this.mIVal;
            }
        }

        public string RewardId
        {
            get
            {
                return this.mRewardId;
            }
        }
    }
}

