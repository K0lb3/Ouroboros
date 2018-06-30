namespace SRPG
{
    using System;
    using System.Text;

    public class ReqBtlComOpen : WebAPI
    {
        public ReqBtlComOpen(string iname, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "btl/com/open";
            builder.Append("\"areaid\":\"");
            builder.Append(iname);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

