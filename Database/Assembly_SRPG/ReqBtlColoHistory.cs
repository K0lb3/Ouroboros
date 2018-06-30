namespace SRPG
{
    using System;
    using System.Text;

    public class ReqBtlColoHistory : WebAPI
    {
        public ReqBtlColoHistory(Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "btl/colo/history/";
            builder = WebAPI.GetStringBuilder();
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

