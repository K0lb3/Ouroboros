namespace SRPG
{
    using System;

    public class ReqFriendApprove : WebAPI
    {
        public ReqFriendApprove(string fuid, Network.ResponseCallback response)
        {
            base..ctor();
            fuid = WebAPI.EscapeString(fuid);
            base.name = "friend/approve";
            base.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
            base.callback = response;
            return;
        }
    }
}

