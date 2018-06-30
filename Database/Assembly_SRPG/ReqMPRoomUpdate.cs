namespace SRPG
{
    using System;

    public class ReqMPRoomUpdate : WebAPI
    {
        public ReqMPRoomUpdate(int roomID, string comment, string passCode, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/room/update";
            base.body = string.Empty;
            base.body = base.body + "\"roomid\":" + ((int) roomID);
            base.body = base.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
            base.body = base.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

