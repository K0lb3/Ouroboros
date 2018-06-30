namespace SRPG
{
    using System;

    public class ReqGachaPending : WebAPI
    {
        public ReqGachaPending(Network.ResponseCallback _response)
        {
            base..ctor();
            base.name = "gacha/pending";
            base.body = WebAPI.GetRequestString(null);
            base.callback = _response;
            return;
        }
    }
}

