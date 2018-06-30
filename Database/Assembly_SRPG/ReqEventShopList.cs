namespace SRPG
{
    using System;

    public class ReqEventShopList : WebAPI
    {
        public ReqEventShopList(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "shop/event/shoplist";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

