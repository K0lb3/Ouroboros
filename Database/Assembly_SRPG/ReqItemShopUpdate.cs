namespace SRPG
{
    using System;

    public class ReqItemShopUpdate : WebAPI
    {
        public ReqItemShopUpdate(string iname, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "shop/update";
            base.body = "\"iname\":\"" + iname + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

