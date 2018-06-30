namespace SRPG
{
    using System;

    public class ReqBuyGold : WebAPI
    {
        public unsafe ReqBuyGold(int coin, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "shop/gold/buy";
            base.body = WebAPI.GetRequestString("\"coin\":" + &coin.ToString());
            base.callback = response;
            return;
        }
    }
}

