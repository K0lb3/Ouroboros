namespace SRPG
{
    using System;

    public class ReqTowerBtlComResume : WebAPI
    {
        public ReqTowerBtlComResume(long btlid, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "tower/btl/resume";
            base.body = "\"btlid\":" + ((long) btlid);
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

