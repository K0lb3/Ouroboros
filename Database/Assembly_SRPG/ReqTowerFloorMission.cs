namespace SRPG
{
    using System;
    using System.Text;

    public class ReqTowerFloorMission : WebAPI
    {
        public ReqTowerFloorMission(string tower_iname, string floor_iname, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "tower/floor/mission";
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
            public int[] missions;
            public int[] missions_val;

            public Json_Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

