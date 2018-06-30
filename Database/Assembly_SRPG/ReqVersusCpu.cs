namespace SRPG
{
    using System;
    using System.Text;

    public class ReqVersusCpu : WebAPI
    {
        public ReqVersusCpu(string iname, int deck_id, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "vs/com/req";
            builder.Append("\"iname\":\"");
            builder.Append(JsonEscape.Escape(iname));
            builder.Append("\",");
            builder.Append("\"deck_id\":");
            builder.Append(deck_id);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

