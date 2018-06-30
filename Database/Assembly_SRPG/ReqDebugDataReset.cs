namespace SRPG
{
    using System;

    public class ReqDebugDataReset : WebAPI
    {
        public ReqDebugDataReset(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "debug/reset";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

