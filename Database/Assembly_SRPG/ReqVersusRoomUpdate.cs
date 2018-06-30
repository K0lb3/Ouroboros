namespace SRPG
{
    using System;

    public class ReqVersusRoomUpdate : WebAPI
    {
        public ReqVersusRoomUpdate(int roomID, string comment, string iname, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/friendmatch/update";
            base.body = string.Empty;
            base.body = base.body + "\"roomid\":" + ((int) roomID);
            base.body = base.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
            base.body = base.body + ",\"quest\":\"" + iname + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

