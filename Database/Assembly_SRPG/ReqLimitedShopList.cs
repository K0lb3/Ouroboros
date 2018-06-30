namespace SRPG
{
    using System;

    public class ReqLimitedShopList : WebAPI
    {
        public ReqLimitedShopList(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "shop/limited/shoplist";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

