namespace SRPG
{
    using System;

    public class ReqGacha : WebAPI
    {
        public ReqGacha(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "gacha";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

