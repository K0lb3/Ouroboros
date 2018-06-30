namespace SRPG
{
    using System;

    public class ReqItemShopBuypaid : WebAPI
    {
        public unsafe ReqItemShopBuypaid(string iname, int id, int buynum, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "shop/buy";
            base.body = "\"iname\":\"" + iname + "\",";
            base.body = base.body + "\"id\":" + &id.ToString() + ",";
            base.body = base.body + "\"buynum\":" + &buynum.ToString();
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

