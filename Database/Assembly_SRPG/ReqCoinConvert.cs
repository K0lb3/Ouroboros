namespace SRPG
{
    using System;

    public class ReqCoinConvert : WebAPI
    {
        public ReqCoinConvert(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "shop/event/coinconvert";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

