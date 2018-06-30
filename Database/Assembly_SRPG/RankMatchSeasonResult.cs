namespace SRPG
{
    using System;

    public class RankMatchSeasonResult
    {
        private int mScheduleId;
        private int mScore;
        private int mRank;
        private RankMatchClass mClass;

        public RankMatchSeasonResult()
        {
            base..ctor();
            return;
        }

        public void Deserialize(ReqRankMatchReward.Response res)
        {
            this.mScheduleId = res.schedule_id;
            this.mScore = res.score;
            this.mRank = res.rank;
            this.mClass = res.type;
            return;
        }

        public int ScheduleId
        {
            get
            {
                return this.mScheduleId;
            }
        }

        public int Score
        {
            get
            {
                return this.mScore;
            }
        }

        public int Rank
        {
            get
            {
                return this.mRank;
            }
        }

        public RankMatchClass Class
        {
            get
            {
                return this.mClass;
            }
        }
    }
}

