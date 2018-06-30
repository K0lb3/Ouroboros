namespace SRPG
{
    using System;

    public class ReqFriendList : WebAPI
    {
        public ReqFriendList(bool is_follow, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "friend";
            base.body = WebAPI.GetRequestString(null);
            if (is_follow == null)
            {
                goto Label_003E;
            }
            base.body = WebAPI.GetRequestString("\"is_follower\":" + ((int) 1));
        Label_003E:
            base.callback = response;
            return;
        }
    }
}

