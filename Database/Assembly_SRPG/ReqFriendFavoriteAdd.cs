namespace SRPG
{
    using System;

    public class ReqFriendFavoriteAdd : WebAPI
    {
        public ReqFriendFavoriteAdd(string fuid, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "friend/favorite/add";
            base.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
            base.callback = response;
            return;
        }
    }
}

