namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    public class ReqMPRoomJoin : WebAPI
    {
        public ReqMPRoomJoin(int roomID, Network.ResponseCallback response, bool LockRoom)
        {
            object[] objArray1;
            string str;
            base..ctor();
            base.name = "btl/room/join";
            base.body = string.Empty;
            str = base.body;
            objArray1 = new object[] { str, "\"roomid\":", (int) roomID, "," };
            base.body = string.Concat(objArray1);
            base.body = base.body + "\"pwd\":";
            base.body = base.body + ((LockRoom == null) ? "\"0\"" : "\"1\"");
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
            public ReqMPRoomJoin.Quest quest;

            public Response()
            {
                base..ctor();
                return;
            }
        }
    }
}

