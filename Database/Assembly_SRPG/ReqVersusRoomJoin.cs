namespace SRPG
{
    using System;

    public class ReqVersusRoomJoin : WebAPI
    {
        public ReqVersusRoomJoin(int roomID, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/friendmatch/join";
            base.body = string.Empty;
            base.body = base.body + "\"roomid\":" + ((int) roomID);
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }

        public class Quest
        {
            public string iname;

            public Quest()
            {
                base..ctor();
                return;
            }
        }

        public class Response
        {
            public string app_id;
            public string token;
            public ReqVersusRoomJoin.Quest quest;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

