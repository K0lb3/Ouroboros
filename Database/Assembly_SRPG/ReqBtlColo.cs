namespace SRPG
{
    using System;
    using System.Text;

    public class ReqBtlColo : WebAPI
    {
        public ReqBtlColo(Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "btl/colo";
            builder = WebAPI.GetStringBuilder();
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

