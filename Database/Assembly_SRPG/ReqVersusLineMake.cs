namespace SRPG
{
    using System;

    public class ReqVersusLineMake : WebAPI
    {
        public ReqVersusLineMake(string roomname, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "vs/friendmatch/line/make";
            base.body = string.Empty;
            base.body = base.body + "\"token\":\"" + JsonEscape.Escape(roomname) + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

