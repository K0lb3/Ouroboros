namespace SRPG
{
    using System;
    using System.Text;

    public class ReqBtlComRaid : WebAPI
    {
        public unsafe ReqBtlComRaid(string iname, int ticket, Network.ResponseCallback response, int partyIndex)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "btl/com/raid2";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iname\":\"");
            builder.Append(iname);
            builder.Append("\",");
            if (partyIndex < 0)
            {
                goto Label_0060;
            }
            builder.Append("\"partyid\":");
            builder.Append(partyIndex);
            builder.Append(",");
        Label_0060:
            builder.Append("\"req_at\":");
            builder.Append(Network.GetServerTime());
            builder.Append(",");
            builder.Append("\"ticket\":");
            builder.Append(&ticket.ToString());
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

