namespace SRPG
{
    using System;
    using System.Text;

    public class ReqGetSupport : WebAPI
    {
        public ReqGetSupport(Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "support";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

