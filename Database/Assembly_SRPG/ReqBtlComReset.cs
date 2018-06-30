namespace SRPG
{
    using System;
    using System.Text;

    public class ReqBtlComReset : WebAPI
    {
        public ReqBtlComReset(string iname, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "btl/com/reset";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iname\":\"");
            builder.Append(iname);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

