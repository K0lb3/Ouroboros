namespace SRPG
{
    using System;

    public class ReqRankMatchStatus : WebAPI
    {
        public ReqRankMatchStatus(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/rankmatch/status";
            base.body = WebAPI.GetRequestString(string.Empty);
            base.callback = response;
            return;
        }

        public class EnableTimeSchedule
        {
            public long expired;
            public long next;
            public string iname;

            public EnableTimeSchedule()
            {
                base..ctor();
                return;
            }
        }

        public enum RankingStatus
        {
            Normal,
            Aggregating,
            Rewarding
        }

        public class Response
        {
            public int schedule_id;
            public int ranking_status;
            public ReqRankMatchStatus.EnableTimeSchedule enabletime;

            public Response()
            {
                base..ctor();
                return;
            }

            public SRPG.ReqRankMatchStatus.RankingStatus RankingStatus
            {
                get
                {
                    return this.ranking_status;
                }
            }
        }
    }
}

