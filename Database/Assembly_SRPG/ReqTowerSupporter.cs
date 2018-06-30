namespace SRPG
{
    using System;

    public class ReqTowerSupporter : WebAPI
    {
        public ReqTowerSupporter(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "tower/supportlist";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

