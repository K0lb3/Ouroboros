namespace SRPG
{
    using System;

    public class ReqFriendFavoriteRemove : WebAPI
    {
        public ReqFriendFavoriteRemove(string fuid, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "friend/favorite/remove";
            base.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
            base.callback = response;
            return;
        }
    }
}

