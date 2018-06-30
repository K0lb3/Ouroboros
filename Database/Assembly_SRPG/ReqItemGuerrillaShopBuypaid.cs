namespace SRPG
{
    using System;

    public class ReqItemGuerrillaShopBuypaid : WebAPI
    {
        public unsafe ReqItemGuerrillaShopBuypaid(int id, int buynum, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "shop/guerrilla/buy";
            base.body = base.body + "\"id\":" + &id.ToString() + ",";
            base.body = base.body + "\"buynum\":" + &buynum.ToString();
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

