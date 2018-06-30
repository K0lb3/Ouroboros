namespace SRPG
{
    using System;

    public class ReqBtlComResume : WebAPI
    {
        public ReqBtlComResume(long btlid, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/com/resume";
            base.body = "\"btlid\":" + ((long) btlid);
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

