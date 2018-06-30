namespace SRPG
{
    using System;

    public class ReqItemShop : WebAPI
    {
        public ReqItemShop(string iname, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "shop";
            base.body = WebAPI.GetRequestString("\"iname\":\"" + iname + "\"");
            base.callback = response;
            return;
        }
    }
}

