namespace SRPG
{
    using System;

    public class ReqMultiTwRoomUpdate : WebAPI
    {
        public ReqMultiTwRoomUpdate(int roomID, string comment, string passCode, string iname, int floor, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/multi/tower/update";
            base.body = string.Empty;
            base.body = base.body + "\"roomid\":" + ((int) roomID);
            base.body = base.body + ",\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
            base.body = base.body + ",\"floor\":" + ((int) floor);
            base.body = base.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
            base.body = base.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

