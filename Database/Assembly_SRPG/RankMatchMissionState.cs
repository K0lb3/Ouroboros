namespace SRPG
{
    using System;

    public class RankMatchMissionState
    {
        private string mIName;
        private int mProgress;
        private DateTime mRewardedAt;
        private bool mIsRewarded;

        public RankMatchMissionState()
        {
            base..ctor();
            return;
        }

        public void Deserialize(string iname, int prog, string rewarded_at)
        {
            this.mIName = iname;
            this.mProgress = prog;
            if (string.IsNullOrEmpty(rewarded_at) != null)
            {
                goto Label_002C;
            }
            this.mRewardedAt = DateTime.Parse(rewarded_at);
            this.mIsRewarded = 1;
        Label_002C:
            return;
        }

        public void Increment()
        {
            this.mProgress += 1;
            return;
        }

        public void Rewarded()
        {
            this.mRewardedAt = TimeManager.ServerTime;
            return;
        }

        public void SetProgress(int prog)
        {
            this.mProgress = prog;
            return;
        }

        public string IName
        {
            get
            {
                return this.mIName;
            }
        }

        public int Progress
        {
            get
            {
                return this.mProgress;
            }
        }

        public DateTime RewardedAt
        {
            get
            {
                return this.mRewardedAt;
            }
        }

        public bool IsRewarded
        {
            get
            {
                return this.mIsRewarded;
            }
        }
    }
}

