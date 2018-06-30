namespace SRPG
{
    using System;

    public class ReqItemEventShopUpdate : WebAPI
    {
        public ReqItemEventShopUpdate(string iname, string costiname, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "shop/update";
            base.body = "\"iname\":\"" + iname + "\",";
            base.body = base.body + "\"costiname\":\"" + costiname + "\"";
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

