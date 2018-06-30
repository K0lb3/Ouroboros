namespace SRPG
{
    using System;
    using UnityEngine.Experimental.Networking;

    public class ReqVersusAudience : WebAPI
    {
        public ReqVersusAudience(string appid, string version, string roomid, Network.ResponseCallback response, DownloadHandler handler)
        {
            base..ctor();
            base.name = "photon/watching/view";
            base.body = string.Empty;
            base.body = base.body + "\"appid\":\"" + JsonEscape.Escape(appid) + "\",";
            base.body = base.body + "\"appversion\":\"" + JsonEscape.Escape(version) + "\",";
            base.body = base.body + "\"roomname\":\"" + JsonEscape.Escape(roomid) + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            base.dlHandler = handler;
            base.reqtype = 1;
            return;
        }
    }
}

