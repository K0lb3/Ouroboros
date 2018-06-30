namespace SRPG
{
    using System;

    public class ReqSendAlterData : WebAPI
    {
        public ReqSendAlterData(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "master/log";
            base.body = WebAPI.GetRequestString(null);
            base.callback = response;
            return;
        }
    }
}

