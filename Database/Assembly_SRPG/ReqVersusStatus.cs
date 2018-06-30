namespace SRPG
{
    using System;

    public class ReqVersusStatus : WebAPI
    {
        public ReqVersusStatus(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/status";
            base.body = string.Empty;
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public class EnableTimeSchedule
        {
            public long expired;
            public long next;
            public int draft_type;
            public long schedule_id;
            public long draft_id;
            public string iname;

            public EnableTimeSchedule()
            {
                base..ctor();
                return;
            }
        }

        public class Response
        {
            public int floor;
            public int key;
            public int wincnt;
            public int is_season_gift;
            public int is_give_season_gift;
            public long end_at;
            public string vstower_id;
            public string tower_iname;
            public string last_enemyuid;
            public int daycnt;
            public ReqVersusStatus.StreakStatus[] streakwins;
            public int is_firstwin;
            public ReqVersusStatus.EnableTimeSchedule enabletime;

            public Response()
            {
                base..ctor();
                return;
            }
        }

        public class StreakStatus
        {
            public int schedule_id;
            public int num;
            public int best;

            public StreakStatus()
            {
                base..ctor();
                return;
            }
        }
    }
}

