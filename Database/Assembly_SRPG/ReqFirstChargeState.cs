namespace SRPG
{
    using System;

    public class ReqFirstChargeState : WebAPI
    {
        public ReqFirstChargeState(Network.ResponseCallback _response)
        {
            base..ctor();
            base.name = "charge/bonus/state";
            base.body = WebAPI.GetRequestString(null);
            base.callback = _response;
            return;
        }
    }
}

