namespace SRPG
{
    using System;
    using System.Text;

    public class ReqSetSupport : WebAPI
    {
        public ReqSetSupport(long id, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "support/set";
            builder.Append("\"uiid\":");
            builder.Append(id);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

