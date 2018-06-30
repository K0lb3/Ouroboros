namespace SRPG
{
    using System;

    public class ReqSupporter : WebAPI
    {
        public ReqSupporter(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/com/supportlist";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

