namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTowerFloorReset : WebAPI
    {
        public ReqTowerFloorReset(string tower_iname, string floor_iname, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "tower/floor/reset";
            builder.Append(WebAPI.KeyValueToString("tower_iname", tower_iname));
            builder.Append(",");
            builder.Append(WebAPI.KeyValueToString("floor_iname", floor_iname));
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        public class JSON_CoinParam
        {
            public int free;
            public int paid;
            public int com;

            public JSON_CoinParam()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Json_Response
        {
            public ReqTowerFloorReset.JSON_CoinParam coin;

            public Json_Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

