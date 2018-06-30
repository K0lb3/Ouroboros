namespace SRPG
{
    using System;

    public class ReqMPRoomMake : WebAPI
    {
        public ReqMPRoomMake(string iname, string comment, string passCode, bool isPrivate, bool limit, int unitlv, bool clear, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/room/make";
            base.body = string.Empty;
            base.body = base.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
            base.body = base.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
            base.body = base.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
            base.body = base.body + ",\"private\":" + ((int) ((isPrivate == null) ? 0 : 1));
            base.body = base.body + ",\"req_at\":" + ((long) Network.GetServerTime());
            base.body = base.body + ",\"limit\":" + ((int) ((limit == null) ? 0 : 1));
            base.body = base.body + ",\"unitlv\":" + ((int) unitlv);
            base.body = base.body + ",\"clear\":" + ((int) ((clear == null) ? 0 : 1));
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

