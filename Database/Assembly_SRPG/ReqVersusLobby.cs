namespace SRPG
{
    using System;

    public class ReqVersusLobby : WebAPI
    {
        public ReqVersusLobby(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/lobby";
            base.body = WebAPI.GetRequestString(string.Empty);
            base.callback = response;
            return;
        }

        public class Response
        {
            public int rankmatch_schedule_id;
            public int rankmatch_ranking_status;
            public ReqRankMatchStatus.EnableTimeSchedule rankmatch_enabletime;
            public int draft_schedule_id;
            public int draft_type;

            public Response()
            {
                base..ctor();
                return;
            }

            public ReqRankMatchStatus.RankingStatus RankMatchRankingStatus
            {
                get
                {
                    return this.rankmatch_ranking_status;
                }
            }
        }
    }
}

