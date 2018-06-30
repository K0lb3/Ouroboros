namespace SRPG
{
    using System;

    public class ReqFriendFind : WebAPI
    {
        public ReqFriendFind(string fuid, Network.ResponseCallback response)
        {
            base..ctor();
            fuid = WebAPI.EscapeString(fuid);
            base.name = "friend/find";
            base.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
            base.callback = response;
            return;
        }
    }
}

