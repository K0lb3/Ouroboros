namespace SRPG
{
    using System;

    public class ReqGetCoinNum : WebAPI
    {
        public ReqGetCoinNum(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "shop/event/getcoinnum";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

