namespace SRPG
{
    using System;

    public class VersusRankRankingRewardParam
    {
        private int mScheduleId;
        private int mRankBegin;
        private int mRankEnd;
        private string mRewardId;

        public VersusRankRankingRewardParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusRankRankingRewardParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.mScheduleId = json.schedule_id;
            this.mRankBegin = json.rank_begin;
            this.mRankEnd = json.rank_end;
            this.mRewardId = json.reward_id;
            return 1;
        }

        public int ScheduleId
        {
            get
            {
                return this.mScheduleId;
            }
        }

        public int RankBegin
        {
            get
            {
                return this.mRankBegin;
            }
        }

        public int RankEnd
        {
            get
            {
                return this.mRankEnd;
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

