namespace SRPG
{
    using System;

    public class ReqJobRankup : WebAPI
    {
        public ReqJobRankup(long iid_job, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "unit/job/equip/lvup/";
            base.body = WebAPI.GetRequestString("\"iid\":" + ((long) iid_job));
            base.callback = response;
            return;
        }
    }
}

