namespace SRPG
{
    using System;

    public class ReqMultiTwRoomMake : WebAPI
    {
        public ReqMultiTwRoomMake(string iname, string comment, string passCode, int floor, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/multi/tower/make";
            base.body = string.Empty;
            base.body = base.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
            base.body = base.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
            base.body = base.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
            base.body = base.body + ",\"req_at\":" + ((long) Network.GetServerTime());
            base.body = base.body + ",\"floor\":" + ((int) floor);
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public class Response
        {
            public int roomid;
            public string app_id;
            public string token;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

