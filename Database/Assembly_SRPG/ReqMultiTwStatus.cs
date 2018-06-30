namespace SRPG
{
    using System;

    public class ReqMultiTwStatus : WebAPI
    {
        public ReqMultiTwStatus(string tower_id, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/multi/tower/status";
            base.body = string.Empty;
            base.body = base.body + "\"tower_id\":\"" + JsonEscape.Escape(tower_id) + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        [Serializable]
        public class FloorParam
        {
            public int floor;
            public int clear_count;

            public FloorParam()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Response
        {
            public ReqMultiTwStatus.FloorParam[] floors;
            public string appid;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

