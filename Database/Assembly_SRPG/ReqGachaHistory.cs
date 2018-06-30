namespace SRPG
{
    using System;

    public class ReqGachaHistory : WebAPI
    {
        public ReqGachaHistory(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "gacha/history";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

