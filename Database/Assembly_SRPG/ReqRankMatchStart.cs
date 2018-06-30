namespace SRPG
{
    using System;

    public class ReqRankMatchStart : WebAPI
    {
        public ReqRankMatchStart(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/rankmatch/start";
            base.body = WebAPI.GetRequestString(string.Empty);
            base.callback = response;
            return;
        }

        [Serializable]
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

        [Serializable]
        public class Response
        {
            public string app_id;
            public int schedule_id;
            public int rank;
            public int score;
            public int type;
            public int bp;
            public ReqRankMatchStart.EnableTimeSchedule enabletime;
            public string[] enemies;
            public ReqRankMatchStart.StreakWin streakwin;
            public int wincnt;
            public int losecnt;

            public Response()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class StreakWin
        {
            public int num;
            public int best;

            public StreakWin()
            {
                base..ctor();
                return;
            }
        }
    }
}

