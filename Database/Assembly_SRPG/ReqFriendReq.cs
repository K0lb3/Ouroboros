namespace SRPG
{
    using System;

    public class ReqFriendReq : WebAPI
    {
        public ReqFriendReq(string fuid, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "friend/req";
            base.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
            base.callback = response;
            return;
        }
    }
}

