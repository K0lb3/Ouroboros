namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTowerFloorRanking : WebAPI
    {
        public ReqTowerFloorRanking(string tower_iname, string floor_iname, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "tower/floor/ranking";
            builder.Append(WebAPI.KeyValueToString("tower_iname", tower_iname));
            builder.Append(",");
            builder.Append(WebAPI.KeyValueToString("floor_iname", floor_iname));
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        [Serializable]
        public class Json_Response
        {
            public ReqTowerRank.JSON_TowerRankParam[] speed;
            public ReqTowerRank.JSON_TowerRankParam[] technical;
            public ReqTowerFloorRanking.Json_Score score;

            public Json_Response()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Json_Score
        {
            public int turn_num;
            public int died_num;
            public int retire_num;
            public int recovery_num;
            public int reset_num;
            public int lose_num;
            public int challenge_num;

            public Json_Score()
            {
                base..ctor();
                return;
            }
        }
    }
}

