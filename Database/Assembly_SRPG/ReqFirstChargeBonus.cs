namespace SRPG
{
    using System;

    public class ReqFirstChargeBonus : WebAPI
    {
        public ReqFirstChargeBonus(Network.ResponseCallback _response)
        {
            base..ctor();
            base.name = "charge/bonus/exec";
            base.body = WebAPI.GetRequestString(null);
            base.callback = _response;
            return;
        }
    }
}

