namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTowerRank : WebAPI
    {
        public ReqTowerRank(string qid, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "tower/ranking";
            builder.Append("\"qid\":\"");
            builder.Append(qid);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        public class JSON_TowerRankParam
        {
            public string fuid;
            public string name;
            public int lv;
            public int rank;
            public int score;
            public string uid;
            public Json_Unit unit;
            public string selected_award;

            public JSON_TowerRankParam()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_TowerRankResponse
        {
            public ReqTowerRank.JSON_TowerRankParam[] speed;
            public ReqTowerRank.JSON_TowerRankParam[] technical;
            public JSON_ReqTowerResuponse.Json_RankStatus rank;

            public JSON_TowerRankResponse()
            {
                base..ctor();
                return;
            }
        }
    }
}

