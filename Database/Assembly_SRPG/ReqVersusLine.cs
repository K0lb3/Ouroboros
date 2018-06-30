namespace SRPG
{
    using System;

    public class ReqVersusLine : WebAPI
    {
        public ReqVersusLine(string roomname, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/friendmatch/line/recruitment";
            base.body = string.Empty;
            base.body = base.body + "\"token\":\"" + JsonEscape.Escape(roomname) + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

