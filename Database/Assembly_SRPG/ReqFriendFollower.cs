namespace SRPG
{
    using System;

    public class ReqFriendFollower : WebAPI
    {
        public ReqFriendFollower(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "friend/follower";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

